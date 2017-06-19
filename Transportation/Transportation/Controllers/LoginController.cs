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


namespace Transportation.Controllers
{
    public class LoginController : Controller
    {
        // Variables

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

        internal static string GetAuthorize(HttpCookie token)
        {
            if (token.Values[0] != null)
            {
                // Variables
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://reports.cos.edu/API/Authentication/api/auth/authorize/");

                req.Method = "GET";
                req.ContentType = "application/x-www-form-urlencoded";
                req.Headers.Add("cache-control", "no-cache");
                req.Headers.Add("authorization", token.Values[1] + " " + token.Values[0]);

                try
                {
                    // Variables
                    WebResponse webRes = req.GetResponse();
                    Stream resStream = webRes.GetResponseStream();
                    StreamReader reader = new StreamReader(resStream);
                    string res = reader.ReadToEnd();

                    return res;
                }
                catch (Exception e)
                {
                    System.Diagnostics.Debug.WriteLine(e);
                }

                return "";
            }
                return "";
            
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
            Token token = GetToken(model.UserName, model.Password);
            HttpCookie tokenCookie = new HttpCookie("user_Token");
            tokenCookie.Values["access_token"] = token.access_token;
            tokenCookie.Values["token_type"] = token.token_type;
            tokenCookie.Values["expires_in"] = token.expires_in.ToString();
            tokenCookie.HttpOnly = true;
            tokenCookie.Expires.AddHours(2.0);
            HttpContext.Response.SetCookie(tokenCookie);

            // usually this will be injected via DI. but creating this manually now for brevity
            //if (authenticationResult.IsSuccess)
            //{
            //}

            //ModelState.AddModelError("", authenticationResult.ErrorMessage);
            return RedirectToAction("../Transportation/TransportationRequest");
        }
    }
}