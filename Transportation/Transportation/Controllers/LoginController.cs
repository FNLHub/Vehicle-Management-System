using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Transportation.Models;
using Newtonsoft.Json;


namespace Transportation.Controllers
{
    public class LoginController : Controller
    {
        public static string userToken = "user_Token";

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
                public string Email;
                public string EmployeeId;
                public string OfficePhone;
                public string[][] AdGroups;
            }
        }

        internal static AuthorizeObject GetAuthorize(HttpCookie token)
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
                    return Auth;
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e);
                }

                return null;
            }
                return null;
            
        }

        private void CreateUserCookie(Token token)
        {
            HttpCookie tokenCookie = new HttpCookie(userToken);
            tokenCookie.Values["access_token"] = token.access_token;
            tokenCookie.Values["token_type"] = token.token_type;
            tokenCookie.Expires.AddMinutes(token.expires_in);
            tokenCookie.HttpOnly = true;
            HttpContext.Response.SetCookie(tokenCookie);
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
            

            // usually this will be injected via DI. but creating this manually now for brevity
            //if (authenticationResult.IsSuccess)
            //{
            //}

            //ModelState.AddModelError("", authenticationResult.ErrorMessage);
            return RedirectToAction("../Transportation/TransportationRequest");
        }
    }
}