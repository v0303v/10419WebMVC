using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebAppMVCInterface10419.Models;

namespace WebAppMVCInterface10419.Controllers
{
    public class ProductController : Controller
    {
        static HttpClient client = new HttpClient();
        
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            //Hosted web API REST Service base url
            string Baseurl = "http://10419-webapi.us-east-1.elasticbeanstalk.com/";
            List<Product> ProdInfo = new List<Product>();
            
            //Passing service base url
            client.BaseAddress = new Uri(Baseurl);
            client.DefaultRequestHeaders.Clear();
            
            //Define request data format
            client.DefaultRequestHeaders.Accept.Add(new
            MediaTypeWithQualityHeaderValue("application/json"));

            //Sending request to find web api REST service resource using HttpClient
            HttpResponseMessage Res = await client.GetAsync("api/Product");

            //Checking the response is successful or not which is sent HttpClient
            if (Res.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api
                var PrResponse = Res.Content.ReadAsStringAsync().Result;

                //Deserializing the response recieved from web api and storing the Product list
                ProdInfo = JsonConvert.DeserializeObject<List<Product>>(PrResponse);
            }
            //returning the Product list to view
            return View(ProdInfo);
            
        }

        [HttpGet]
        public async Task<Product> Details(int Id)
        {
           
            string Baseurl = "http://10419-webapi.us-east-1.elasticbeanstalk.com/";
            Product product = new Product();

            //Passing service base url
            client.BaseAddress = new Uri(Baseurl);
            client.DefaultRequestHeaders.Clear();

            //Define request data format
            client.DefaultRequestHeaders.Accept.Add(new
            MediaTypeWithQualityHeaderValue("application/json"));

            //Sending request to find web api REST service resource using HttpClient
            HttpResponseMessage Res = await client.GetAsync("api/Product/" + Id);

            //Checking the response is successful or not which is sent HttpClient
            if (Res.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api
                var PrResponse = Res.Content.ReadAsStringAsync().Result;

                //Deserializing the response recieved from web api and storing the Product list
                product = JsonConvert.DeserializeObject<Product>(PrResponse);
            }
  
            return product;

        }

        [HttpPost]
        public async Task<Product> CreateEdit(Product product)
        {
            string Baseurl = "http://10419-webapi.us-east-1.elasticbeanstalk.com/";
            Product producti = new Product();

            //Passing service base url
            client.BaseAddress = new Uri(Baseurl);
            client.DefaultRequestHeaders.Clear();

            string data = JsonConvert.SerializeObject(product);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            //Define request data format
            client.DefaultRequestHeaders.Accept.Add(new
            MediaTypeWithQualityHeaderValue("application/json"));

            //Sending request to find web api REST service resource using HttpClient
            HttpResponseMessage Res = await client.PostAsync("api/Product/", content);

            //Checking the response is successful or not which is sent HttpClient
            if (Res.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api
                var PrResponse = Res.Content.ReadAsStringAsync().Result;

                //Deserializing the response recieved from web api and storing the Product list
                product = JsonConvert.DeserializeObject<Product>(PrResponse);
            }

            return product;
        }

        [HttpDelete]
        public async Task<string> Delete(int Id)
        {
            string msg = "";
            string Baseurl = "http://10419-webapi.us-east-1.elasticbeanstalk.com/";
            Product product = new Product();

            //Passing service base url
            client.BaseAddress = new Uri(Baseurl);
            client.DefaultRequestHeaders.Clear();

            //Define request data format
            client.DefaultRequestHeaders.Accept.Add(new
            MediaTypeWithQualityHeaderValue("application/json"));

            //Sending request to find web api REST service resource using HttpClient
            HttpResponseMessage Res = await client.DeleteAsync("api/Product/" + Id);

            msg = await Res.Content.ReadAsStringAsync();
            
            return msg;
        }
    }
}
