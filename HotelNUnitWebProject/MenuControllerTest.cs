using HotelManagementProject.Controllers;
using System.Collections.Generic;
using HotelManagementProject.Models;
using System.Net.Http;
using System.Net;
using System.Reflection;
using System.Collections;
using System.Linq;

namespace HotelNUnitWebProject
{
    public class Tests
    {
        public IList<Menu> menuExpected, menuActual;
        public Menu m1,m2;
        public HttpClient _client;
        public HttpResponseMessage _response;
        public const string ServiceBaseUrl= "https://localhost:44349/api/";

        [SetUp]
        public void Setup()
        {
            _client = new HttpClient { BaseAddress = new Uri(ServiceBaseUrl) };

            menuExpected = new List<Menu>
            {
                new Menu{Menu_Type="Veg",Price=100,Quantity=1},
                new Menu{Menu_Type="Non Veg",Price=250,Quantity=3}
            };

        }
        [TearDown]//Automatically executed aftr each testcase
        public void DisposeTest()
        {
            if (_response != null)
                _response.Dispose();
            if (_client != null)
                _client.Dispose();
        }

        [Test]
        public void GetAllMenusTest()
        {
            var responseTask = _client.GetAsync("Menu");
            responseTask.Wait();

            _response = responseTask.Result;

            if(_response.IsSuccessStatusCode)
            {
                var readTask = _response.Content.ReadAsAsync<Menu[]>();
                readTask.Wait();

                menuActual = readTask.Result;
            }
            Assert.IsNotNull(menuActual);
        }
        /*
        [Test]
        public void GetMenuByIDTest()
        {
            var responseTask = _client.GetAsync("Menu/?mid=1");
            responseTask.Wait();

            _response = responseTask.Result;

            if (_response.IsSuccessStatusCode)
            {
                var readTask = _response.Content.ReadAsAsync<Menu[]>();
                readTask.Wait();

                menuActual = readTask.Result;
            }
            Assert.IsNotNull(menuActual);
        }
        */
    }
}