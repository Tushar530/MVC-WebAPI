
using ModelFirst_Student.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace ModelFirst_Student.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            IEnumerable<Student> list = null;

            using (var client = new HttpClient())
            {
                // Url of Webapi project
                client.BaseAddress = new Uri("http://localhost:62346/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Students");  // PersonDetails is the WebApi controller name
                // wait for task to complete
                responseTask.Wait();
                // retrieve the result
                var result = responseTask.Result;
                // check the status code for success
                if (result.IsSuccessStatusCode)
                {
                    // read the result
                    var readTask = result.Content.ReadAsAsync<IList<Student>>();
                    readTask.Wait();
                    // fill the list vairable created above with the returned result
                    list = readTask.Result;
                }
                else //web api sent error response 
                {
                    list = Enumerable.Empty<Student>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(list);
          }
 
           public ActionResult Create()
           {
            return View();
           }

        [HttpPost]
        public ActionResult Create(Student obj)
        {
            using (var client = new HttpClient())
            {
                // Url of Webapi project
                client.BaseAddress = new Uri("http://localhost:62346/api/");
                //HTTP POST
                var postTask = client.PostAsJsonAsync<Student>("Students", obj);
                // wait for task to complete
                postTask.Wait();
                // retrieve the result
                var result = postTask.Result;
                // check the status code for success
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            // Add model error
            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            // send the view back with model error
            return View(obj);
        }

        public ActionResult Edit(int id)
        {
            // variable to hold the person details retrieved from WebApi
           Student stud = null;

            using (var client = new HttpClient())
            {
                // Url of Webapi project
                client.BaseAddress = new Uri("http://localhost:62346/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Students/" + id.ToString());
                // wait for task to complete
                responseTask.Wait();
                // retrieve the result
                var result = responseTask.Result;
                // check the status code for success
                if (result.IsSuccessStatusCode)
                {
                    // read the result
                    var readTask = result.Content.ReadAsAsync<Student>();
                    readTask.Wait();
                    // fill the person vairable created above with the returned result
                    stud = readTask.Result;
                }
            }
            return View(stud);
        }

        [HttpPost]
        public ActionResult Edit(Student obj)
        {
            //    //using (var client = new HttpClient())
            //    //{
            //    //    // Url of Webapi project
            //    //    client.BaseAddress = new Uri("http://localhost:62346/api/");
            //    //    //HTTP POST
            //    //    var putTask = client.PutAsJsonAsync<Student>("Students", obj);
            //    //    // wait for task to complete
            //    //    putTask.Wait();
            //    //    // retrieve the result
            //    //    var result = putTask.Result;
            //    //    // check the status code for success
            //    //    if (result.IsSuccessStatusCode)
            //    //    {
            //    //        // Return to Index
            //    //        return RedirectToAction("Index");
            //    //    }




            //}
            //    // Add model error
            //    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            //    // send the view back with model error 

            //    return View(obj);

          try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:62346/api/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.PutAsJsonAsync("students/" + obj.RollNo, obj).Result;
               
                return RedirectToAction("Index");
            }
            catch
            {
                return View(obj);
            }

        }

        public ActionResult Details(int id)
        {
            // variable to hold the person details retrieved from WebApi
           Student stud = null;

            using (var client = new HttpClient())
            {
                // Url of Webapi project
                client.BaseAddress = new Uri("http://localhost:62346/api/");
                //HTTP GET
                var responseTask = client.GetAsync("Students?id=" + id.ToString());
                // wait for task to complete
                responseTask.Wait();
                // retrieve the result
                var result = responseTask.Result;
                // check the status code for success
                if (result.IsSuccessStatusCode)
                {
                    // read the result
                    var readTask = result.Content.ReadAsAsync<Student>();
                    readTask.Wait();
                    // fill the person vairable created above with the returned result
                   stud = readTask.Result;
                }
            }
            return View(stud);
        }

        public ActionResult Delete(int id)
        {
            // variable to hold the person details retrieved from WebApi
          //Student stud = null;

            using (var client = new HttpClient())
            {
                // Url of Webapi project
                client.BaseAddress = new Uri("http://localhost:62346/api/");
                //HTTP Delete
                var responseTask = client.DeleteAsync("Students/" + id.ToString());
                // wait for task to complete
                responseTask.Wait();
                // retrieve the result
                var deleteTask = responseTask.Result;
                // check the status code for success
                if (deleteTask.IsSuccessStatusCode)
                {

                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

    }
}