using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using HotelWebApp.Models;
using System.Diagnostics;
using System.Data.SqlClient;

namespace HotelWebApp.Controllers
{
    public class AccountController : Controller
    {
        //private IConfiguration configuration;
        public ILoginService MyLogin;

        public List<Registration> users = null;
        public AccountController(ILoginService _log)
        {
            MyLogin = _log;
        }
        //public AccountController()
        //{
           
        //}
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetMyUsers()
        {
            IList<Registration> Registrations = null;
            if (!ModelState.IsValid)
                return BadRequest("NOt a valid model");
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("https://localhost:44349/api/");
                var responseTask = client.GetAsync("Account");   //sends request and ui becomes responsive
                responseTask.Wait();                                    //for Asynchronous call

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Registration[]>();
                    readTask.Wait();
                    Registrations = readTask.Result.ToList();
                    
                }
                else
                {
                    Registrations = Enumerable.Empty<Registration>().ToList();
                    ModelState.AddModelError(String.Empty, "Server Error.Please Connect to server");
                }
            }
            return View(Registrations);
        }
        [HttpPost]
        public ActionResult AddUser(Registration r)
        {
            
            var recordsAdded = 0;

            //if (!ModelState.IsValid)
            //return BadRequest("Not a valid model");
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44349/api/");
                var postTask = client.PostAsJsonAsync<Registration>("Account", r);   //sends request and ui becomes responsive
                postTask.Wait();                                    //for Asynchronous call

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<int>();
                    readTask.Wait();
                    recordsAdded = readTask.Result;
                  
                }
                else
                {
                    return View("Create");
                }
            }
            return RedirectToAction("GetMyUsers");
        }
        [HttpGet]
        public ActionResult UserDetails(int id)
        {

            Registration r = null;
            //var recordsAdded = 0;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44349/api/");
                var responseTask = client.GetAsync($"Account/?uid={id}");   //sends request and ui becomes responsive
                responseTask.Wait();                                    //for Asynchronous call

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Registration>();
                    readTask.Wait();
                    r = readTask.Result;
                }
                else
                {
                    // return View("Edit");
                }
            }

            return View(r);
        }
        [HttpGet]
        public ActionResult DeleteUser(int id)
        {
            Registration rd = null;
            var recordsAdded = 0;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44349/api/");
                var responseTask = client.GetAsync($"Account/?uid={id}");   //sends request and ui becomes responsive
                responseTask.Wait();                                    //for Asynchronous call

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Registration>();
                    readTask.Wait();
                    rd = readTask.Result;
                    TempData["Registration_Id"] = rd.Registration_ID;
                   
                }
                else
                {
                    // return View("Edit");
                }
            }
            return View(rd);
        }
        [HttpPost]
        public ActionResult DeleteUser()
        {

            int rd = 0;
            var recordsAdded = 0;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44349/api/");
                var responseTask = client.DeleteAsync($"Account/?uid={TempData["Registration_Id"]}");   //sends request and ui becomes responsive
                responseTask.Wait();                                    //for Asynchronous call

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<int>();
                    readTask.Wait();
                    rd = readTask.Result;
                    
                }
                else
                {
                    // return View("Edit");
                }
            }

            return RedirectToAction("GetMyUsers");
        }

        [HttpGet]
        public ActionResult EditUser(int id)
        {

            Registration grd = null;
            var recordsAdded = 0;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44349/api/");
                var responseTask = client.GetAsync($"Account/?uid={id}");   //sends request and ui becomes responsive
                responseTask.Wait();                                    //for Asynchronous call

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Registration>();
                    readTask.Wait();
                    grd = readTask.Result;
                  
                }
                else
                {
                    // return View("Edit");
                }
            }

            return View(grd);
        }
        [HttpPost]
        public ActionResult EditUser(Registration grd)
        {
            var recordsAdded = 0;
            //if (!ModelState.IsValid)
            // return BadRequest("Not a valid model");
            using (var client = new HttpClient())
            {//POST api/Account?gId={gId}
                client.BaseAddress = new Uri("https://localhost:44349/api/");
                var postTask = client.PostAsJsonAsync("Account?gId=" + grd.Registration_ID, grd);   //sends request and ui becomes responsive
                postTask.Wait();                                    //for Asynchronous call

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<int>();
                    readTask.Wait();
                    recordsAdded = readTask.Result;
                }
                else
                {
                    return View("EditUser", grd);
                }
            }

            return RedirectToAction("GetMyUsers");
        }
        
        [HttpGet]
        public ActionResult Login(string ReturnUrl = "/")
        {
            LoginModel objLoginModel = new LoginModel();
            objLoginModel.ReturnUrl = ReturnUrl;
            return View(objLoginModel);
        }
       
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel objLoginModel)
        {
            List<Claim> myClaims = MyLogin.LogMeIn(objLoginModel);
            
            try
            {
                if (myClaims != null)
                {
                    var identity = new ClaimsIdentity(myClaims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
                    {
                        IsPersistent = objLoginModel.RememberLogin
                    });
                    //HttpContent
                    //HttpContext.Session.SetString("UserID", Registration.UserID);
                    //HttpContext.Session.SetString("UserType", Registration.Usertype);

                    return LocalRedirect(objLoginModel.ReturnUrl);
                }
                else
                {
                    ViewBag.Message = "Invalid Credential";
                    
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return View(objLoginModel);

        }
       
       public async Task<IActionResult> LogOut()
       {
           await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
           return LocalRedirect("/");
       }
      
    }
}
