using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using HotelWebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace HotelWebApp.Controllers
{
    //[AllowAnonymous]
    // [Authorize(Roles ="Admin")]//allows admin to access the all methods 
    public class MenuController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }
      //  [AllowAnonymous]//it allow all to use the methods
        public ActionResult GetMyMenus()
        {
            IList<Menu> Menus = null;
            if (!ModelState.IsValid)
                return BadRequest("NOt a valid model");
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("https://localhost:44349/api/");
                var responseTask = client.GetAsync("Menu");   //sends request and ui becomes responsive
                responseTask.Wait();                                    //for Asynchronous call

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Menu[]>();
                    readTask.Wait();
                    Menus = readTask.Result.ToList();
                    
                }
                else
                {
                    Menus =   Enumerable.Empty<Menu>().ToList();
                    ModelState.AddModelError(String.Empty, "Server Error.Please Connect to server");
                }
            }
            return View(Menus);
        }
       // [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AddMenu(Menu mm)
        {
            
            var recordsAdded = 0;

            //if (!ModelState.IsValid)
            //return BadRequest("Not a valid model");
            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44349/api/");
                var postTask = client.PostAsJsonAsync<Menu>("Menu", mm);   //sends request and ui becomes responsive
                postTask.Wait();                                    //for Asynchronous call

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<int>();
                    readTask.Wait();
                    recordsAdded = readTask.Result;
                    // Console.WriteLine("{0} records of grade added", recordsAdded);
                }
                else
                {
                    return View("Create");
                }
            }
            return RedirectToAction("GetMyMenus");
        }
        [HttpGet]
        public ActionResult MenuDetails(int id)
        {

            Menu r = null;
            var recordsAdded = 0;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44349/api/");
                var responseTask = client.GetAsync($"Menu/?mid={id}");   //sends request and ui becomes responsive
                responseTask.Wait();                                    //for Asynchronous call

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Menu>();
                    readTask.Wait();
                    r = readTask.Result;
                    //Console.WriteLine("{0} records of grade updated", recordsAdded);
                }
                else
                {
                    // return View("Edit");
                }
            }

            return View(r);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult DeleteMenu(int id)
        {
            Menu rd = null;
            var recordsAdded = 0;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44349/api/");
                var responseTask = client.GetAsync($"Menu/?mid={id}");   //sends request and ui becomes responsive
                responseTask.Wait();                                    //for Asynchronous call

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Menu>();
                    readTask.Wait();
                    rd = readTask.Result;
                    TempData["Menu_Id"] = rd.Menu_ID;
                }
                else
                {
                    // return View("Edit");
                }
            }
            return View(rd);
        }
        [HttpPost]
        public ActionResult DeleteMenu()
        {

            int rd = 0;
            var recordsAdded = 0;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44349/api/");
                var responseTask = client.DeleteAsync($"Menu/?mid={TempData["Menu_Id"]}");   //sends request and ui becomes responsive
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

            return RedirectToAction("GetMyMenus");
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult EditMenu(int id)
        {

            Menu grd = null;
            var recordsAdded = 0;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44349/api/");
                var responseTask = client.GetAsync($"Menu/?mid={id}");   //sends request and ui becomes responsive
                responseTask.Wait();                                    //for Asynchronous call

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Menu>();
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
        public ActionResult EditMenu(Menu grd)
        {
            var recordsAdded = 0;
            //if (!ModelState.IsValid)
            // return BadRequest("Not a valid model");
            using (var client = new HttpClient())
            {//POST api/Account?gId={gId}
                client.BaseAddress = new Uri("https://localhost:44349/api/");
                var postTask = client.PostAsJsonAsync("Menu?mId=" + grd.Menu_ID, grd);   //sends request and ui becomes responsive
                postTask.Wait();                                    //for Asynchronous call

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<int>();
                    readTask.Wait();
                    recordsAdded = readTask.Result;
                    //Console.WriteLine("{0} records of grade updated", recordsAdded);
                }
                else
                {
                    return View("EditMenu", grd);
                }
            }

            return RedirectToAction("GetMyMenus");
        }


    }
}
