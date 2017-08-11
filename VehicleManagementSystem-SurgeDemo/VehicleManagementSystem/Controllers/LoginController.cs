using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using VehicleManagementSystem.Models;
using Newtonsoft.Json;
using TransportationDB;

namespace VehicleManagementSystem.Controllers
{
    public class LoginController : Controller
    {
        public static string userToken = "user_Token";
        FacilitiesDBEntities transportationContext = new FacilitiesDBEntities();

        public static string FirstName = null;

        private Token GetToken(string userName, string password)
        {
            // Variables
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://reports.cos.edu/API/Authentication/token");

            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.Headers.Add("cache-control", "no-cache");
            using (StreamWriter writer = new StreamWriter(req.GetRequestStream(), Encoding.ASCII))
            {
                // Variables
                string txt = "username=" + userName + "&password=" + password + "&grant_type=password";

                writer.Write(txt);
                writer.Flush();
                writer.Close();
            }

            try
            {
                // Variables
                WebResponse webRes = req.GetResponse();
                Stream resStream = webRes.GetResponseStream();
                StreamReader reader = new StreamReader(resStream);
                string res = reader.ReadToEnd();
                Token token = Newtonsoft.Json.JsonConvert.DeserializeObject<Token>(res);

                return token;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }

            return null;
        }

        internal class Token
        {
            public string access_token;
            public string token_type;
            public int expires_in;
        }

        public class AuthorizeObject
        {
            public AuthUserInfo userInfo;

            public class AuthUserInfo
            {
                public string DisplayName;
                public string FirstName;
                public string LastName;
                public string Email;
                public string EmployeeId;
                public string OfficePhone;
                public string[][] AdGroups;
            }
        }

        internal static AuthorizeObject GetAuthorize(HttpCookie token)
        {
            try
            {
                if (token.Values["access_token"] != null)
                {
                    // Variables
                    HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://reports.cos.edu/API/Authentication/api/auth/authorize/Json");

                    req.Method = "GET";
                    req.ContentType = "application/x-www-form-urlencoded";
                    req.Headers.Add("cache-control", "no-cache");
                    req.Headers.Add("authorization", token.Values["token_type"] + " " + token.Values["access_token"]);

                    try
                    {
                        // Variables
                        WebResponse webRes = req.GetResponse();
                        Stream resStream = webRes.GetResponseStream();
                        StreamReader reader = new StreamReader(resStream);
                        string res = reader.ReadToEnd();
                        AuthorizeObject Auth = JsonConvert.DeserializeObject<AuthorizeObject>(res);

                        //Set for View
                        FirstName = Auth.userInfo.FirstName;

                        return Auth;
                    }
                    catch (Exception e)
                    {
                        System.Diagnostics.Debug.WriteLine(e);
                    }

                    return null;
                }
            }
            catch
            {
                return null;
            }
            return null;

        }

        private void CreateUserCookie(Token token)
        {
            try
            {
                HttpCookie tokenCookie = new HttpCookie(userToken);
                tokenCookie.Values["access_token"] = token.access_token;
                tokenCookie.Values["token_type"] = token.token_type;
                tokenCookie.Expires.AddMinutes(token.expires_in);
                tokenCookie.HttpOnly = true;
                HttpContext.Response.SetCookie(tokenCookie);
                GetAuthorize(tokenCookie);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Index()
        {
            ViewBag.Title = "Login 2 Test";
            var loginViewModel = new LoginViewModel
            {

            };
            return View(loginViewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Index(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Create and save login token
            CreateUserCookie(GetToken(model.UserName, model.Password));

            var userAuth = GetAuthorize(Request.Cookies[userToken]);

            // Checks to verify username and password are correct else throws error
            if (userAuth != null)
            {
                // Checks to see if user is already in the user table, If not redirect to create account page, If True redirect to new Request Page
                if (transportationContext.Users.Where(u => u.BannerId == userAuth.userInfo.EmployeeId.Substring(1)).Select(u => u.BannerId).FirstOrDefault() == null)
                {
                    return RedirectToAction("../User/EditUserInfo");
                }
                //return RedirectToAction("../User/EditUserInfo");
                return RedirectToAction("../Home/Index");
            }
            else {
                //If incorreect password or username
                ModelState.AddModelError("", "Incorrect Username or Password");
                return View();

            }
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Response.Cookies[LoginController.userToken].Expires = DateTime.Now.AddDays(-1);
            FirstName = null;
            return RedirectToAction("Index","Home");
        }
    }
}