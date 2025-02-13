using CustomersMasterDetail.DTO;
using CustomersMasterDetail.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace CustomersMasterDetail.Controllers
{
    public class CustomerController : Controller
    {
        private string baseURL = "https://localhost:7283/";

        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Index()
        {
            List<Customer> lstCustomer = new List<Customer>();
            using (var _httpclient = new HttpClient())
            {
                //HttpContext.Session.GetString("APIToken")
                _httpclient.BaseAddress = new Uri(baseURL + "api/Customer/");
                _httpclient.DefaultRequestHeaders.Accept.Clear();
                _httpclient.DefaultRequestHeaders.Accept.Add
                    (new MediaTypeWithQualityHeaderValue("application/json"));
                _httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("APIToken"));


                HttpResponseMessage getData = await _httpclient.GetAsync("GetCustomers");
                if (getData.IsSuccessStatusCode)
                {
                    string result = getData.Content.ReadAsStringAsync().Result;
                    lstCustomer = JsonConvert.DeserializeObject<List<Customer>>(result);
                }
                else
                {
                    return View("ErrorPage");
                }
            }
            return View(lstCustomer);
        }
        public async Task<IActionResult> Details(int Id)
        {
            CustomerDTO Customer = new CustomerDTO();
            using (var _httpclient = new HttpClient())
            {
                _httpclient.BaseAddress = new Uri(baseURL + "api/Customer/");
                _httpclient.DefaultRequestHeaders.Accept.Clear();
                _httpclient.DefaultRequestHeaders.Accept.Add
                    (new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await _httpclient.GetAsync($"GetCustomerById?Id={Id}");
                if (getData.IsSuccessStatusCode)
                {
                    string result = getData.Content.ReadAsStringAsync().Result;
                    Customer = JsonConvert.DeserializeObject<CustomerDTO>(result);
                }
                else
                {
                    return View("ErrorPage");
                }
            }
            return View(Customer);
        }
       
        public async Task<IActionResult> Delete(int Id)
        {

            CustomerDTO Customer = new CustomerDTO();
            using (var _httpclient = new HttpClient())
            {
                _httpclient.BaseAddress = new Uri(baseURL + "api/Customer/");
                _httpclient.DefaultRequestHeaders.Accept.Clear();
                _httpclient.DefaultRequestHeaders.Accept.Add
                    (new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await _httpclient.GetAsync($"GetCustomerById?Id={Id}");
                if (getData.IsSuccessStatusCode)
                {
                    string result = getData.Content.ReadAsStringAsync().Result;
                    Customer = JsonConvert.DeserializeObject<CustomerDTO>(result);
                }
                else
                {
                    return View("ErrorPage");
                }
            }
            return View(Customer);

        }
        public async Task<IActionResult> Create()
        {
            CustomerDTO customer = new CustomerDTO();
            customer.customerAddresses = new List<CustomerAddressDTO>();
           
                var newAddress = new CustomerAddressDTO();
            newAddress.Id = 1;
               
            customer.customerAddresses.Add(newAddress);
           
            return View(customer);
        }
        public async Task<IActionResult> CreateCustomer(CustomerDTO customer)
        {
            using (var _httpclient = new HttpClient())
            {
                _httpclient.BaseAddress = new Uri(baseURL + "api/Customer/");
                _httpclient.DefaultRequestHeaders.Accept.Clear();
                _httpclient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await _httpclient.PostAsJsonAsync("", customer);

                if (getData.IsSuccessStatusCode)
                {                    
                   return RedirectToAction("Index");
                }            
                        
                else
                {
                    return View("ErrorPage"); 
                }
                
            }
        }
        public async Task<IActionResult> UpdateCustomer(CustomerDTO customer)
        {
            using (var _httpclient = new HttpClient())
            {
                _httpclient.BaseAddress = new Uri(baseURL + "api/Customer/");
                _httpclient.DefaultRequestHeaders.Accept.Clear();
                _httpclient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await _httpclient.PutAsJsonAsync("UpdateCustomer", customer);

                if (getData.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("ErrorPage");
                }

            }
        }

        public async Task<IActionResult> DeleteCustomer(int Id)
        {
            using (var _httpclient = new HttpClient())
            {
                _httpclient.BaseAddress = new Uri(baseURL + "api/Customer/");
                _httpclient.DefaultRequestHeaders.Accept.Clear();
                _httpclient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await _httpclient.PutAsync($"DeleteCustomerById?Id={Id}", null);

                if (getData.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("ErrorPage");
                }

            }
        }

    }
}
