using CustomersMasterDetail.DTO;
using CustomersMasterDetail.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http.Headers;
using static System.Net.Mime.MediaTypeNames;

namespace CustomersMasterDetail.Controllers
{
    public class ApplicantController : Controller
    {
        private string baseURL = "https://localhost:7283/";
        private readonly IWebHostEnvironment _webHost;

        public ApplicantController(IWebHostEnvironment webHost)
        {
            _webHost = webHost;
        }
        public async Task<IActionResult> Index()
        {
            List<Applicant> lstApplicant = new List<Applicant>();
            using (var _httpclient = new HttpClient())
            {
                _httpclient.BaseAddress = new Uri(baseURL + "api/Applicant/");
                _httpclient.DefaultRequestHeaders.Accept.Clear();
                _httpclient.DefaultRequestHeaders.Accept.Add
                    (new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await _httpclient.GetAsync("");
                if (getData.IsSuccessStatusCode)
                {
                    string result = getData.Content.ReadAsStringAsync().Result;
                    lstApplicant = JsonConvert.DeserializeObject<List<Applicant>>(result);
                }
                else
                {
                    return View("ErrorPage");
                }
            }
            return View(lstApplicant);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ApplicantDTO applicant = new ApplicantDTO();
            applicant.ExperienceDetail = new List<ExperienceDTO>();
            applicant.SoftwareExperience = new List<SoftwareExperienceDTO>();
            var newExperience = new ExperienceDTO();
            newExperience.ExperienceId = 1;

            var newSoftwareExperience =new SoftwareExperienceDTO();
            newSoftwareExperience.Id = 1;

            applicant.ExperienceDetail.Add(newExperience);
            applicant.SoftwareExperience.Add(newSoftwareExperience);
            ViewBag.Gender = GetGender();
            ViewBag.Rating = GetRating();

            List<Software> selSoftware = new List<Software>(); 

            using (var _httpclient = new HttpClient())
            {
                _httpclient.BaseAddress = new Uri(baseURL + "api/Software/");
                _httpclient.DefaultRequestHeaders.Accept.Clear();
                _httpclient.DefaultRequestHeaders.Accept.Add
                    (new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await _httpclient.GetAsync("");
                if (getData.IsSuccessStatusCode)
                {
                    string result = getData.Content.ReadAsStringAsync().Result;
                    selSoftware = JsonConvert.DeserializeObject<List<Software>>(result);
                }
                else
                {
                    View("ErrorPage");
                }
            }

            List<SelectListItem> getSoft = new List<SelectListItem>();
            foreach (var entry in selSoftware)
            {
                getSoft.Add(new SelectListItem { Text = entry.Name, Value = entry.id.ToString() });
            }
            ViewBag.Software = getSoft;
            return View(applicant);
        }
        [HttpPost]
        public async Task<IActionResult> CreateApplicant(ApplicantDTO applicant)
        {
            using (var _httpclient = new HttpClient())
            {
                _httpclient.BaseAddress = new Uri(baseURL + "api/Applicant/");
                _httpclient.DefaultRequestHeaders.Accept.Clear();
                _httpclient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                string uniqueFileName = GetUploadedFileName(applicant);
                applicant.PhotoUrl = uniqueFileName;
                HttpResponseMessage getData = await _httpclient.PostAsJsonAsync("", applicant);

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
                
        public async Task<IActionResult> Update(int Id)
        {

            ApplicantDTO Applicant = new ApplicantDTO();
           
            ViewBag.Gender = GetGender();
            ViewBag.Rating = GetRating();
            //ViewBag.Software = GetSoftware();
            using (var _httpclient = new HttpClient())
            {
                _httpclient.BaseAddress = new Uri(baseURL + "api/Applicant/");
                _httpclient.DefaultRequestHeaders.Accept.Clear();
                _httpclient.DefaultRequestHeaders.Accept.Add
                    (new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await _httpclient.GetAsync($"GetApplicantById?Id={Id}");
                if (getData.IsSuccessStatusCode)
                {
                    string result = getData.Content.ReadAsStringAsync().Result;
                    Applicant = JsonConvert.DeserializeObject<ApplicantDTO>(result);
                }
                else
                {
                    return View("ErrorPage");
                }
            }

            List<Software> selSoftware = new List<Software>();

            using (var _httpclient = new HttpClient())
            {
                _httpclient.BaseAddress = new Uri(baseURL + "api/Software/");
                _httpclient.DefaultRequestHeaders.Accept.Clear();
                _httpclient.DefaultRequestHeaders.Accept.Add
                    (new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await _httpclient.GetAsync("");
                if (getData.IsSuccessStatusCode)
                {
                    string result = getData.Content.ReadAsStringAsync().Result;
                    selSoftware = JsonConvert.DeserializeObject<List<Software>>(result);
                }
                else
                {
                    View("ErrorPage");
                }
            }

            List<SelectListItem> getSoft = new List<SelectListItem>();
            foreach (var entry in selSoftware)
            {
                getSoft.Add(new SelectListItem { Text = entry.Name, Value = entry.id.ToString() });
            }
            ViewBag.Software = getSoft;

            return View(Applicant);          
            
        }
               
        public async Task<IActionResult> UpdateApplicant(ApplicantDTO applicant)
        {
            string uniqueFileName = GetUploadedFileName(applicant);
            applicant.PhotoUrl = uniqueFileName;
            using (var _httpclient = new HttpClient())
            {
                _httpclient.BaseAddress = new Uri(baseURL + "api/Applicant/");
                _httpclient.DefaultRequestHeaders.Accept.Clear();
                _httpclient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await _httpclient.PutAsJsonAsync("UpdateApplicant", applicant);

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
        public async Task<IActionResult> Detail(int Id)
        {
            ApplicantDTO Applicant = new ApplicantDTO();
            using (var _httpclient = new HttpClient())
            {
                _httpclient.BaseAddress = new Uri(baseURL + "api/Applicant/");
                _httpclient.DefaultRequestHeaders.Accept.Clear();
                _httpclient.DefaultRequestHeaders.Accept.Add
                    (new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await _httpclient.GetAsync($"GetApplicantById?Id={Id}");
                if (getData.IsSuccessStatusCode)
                {
                    string result = getData.Content.ReadAsStringAsync().Result;
                    Applicant = JsonConvert.DeserializeObject<ApplicantDTO>(result);
                }
                else
                {
                    return View("ErrorPage");
                }
            }
            return View(Applicant);
        }

        public async Task<IActionResult> Delete(int Id)
        {           
            ApplicantDTO Applicant = new ApplicantDTO();

            ViewBag.Gender = GetGender();
            ViewBag.Rating = GetRating();
            //ViewBag.Software = GetSoftware();
            using (var _httpclient = new HttpClient())
            {
                _httpclient.BaseAddress = new Uri(baseURL + "api/Applicant/");
                _httpclient.DefaultRequestHeaders.Accept.Clear();
                _httpclient.DefaultRequestHeaders.Accept.Add
                    (new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await _httpclient.GetAsync($"GetApplicantById?Id={Id}");
                if (getData.IsSuccessStatusCode)
                {
                    string result = getData.Content.ReadAsStringAsync().Result;
                    Applicant = JsonConvert.DeserializeObject<ApplicantDTO>(result);
                }
                else
                {
                    return View("ErrorPage");
                }
            }

            List<Software> selSoftware = new List<Software>();

            using (var _httpclient = new HttpClient())
            {
                _httpclient.BaseAddress = new Uri(baseURL + "api/Software/");
                _httpclient.DefaultRequestHeaders.Accept.Clear();
                _httpclient.DefaultRequestHeaders.Accept.Add
                    (new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await _httpclient.GetAsync("");
                if (getData.IsSuccessStatusCode)
                {
                    string result = getData.Content.ReadAsStringAsync().Result;
                    selSoftware = JsonConvert.DeserializeObject<List<Software>>(result);
                }
                else
                {
                    View("ErrorPage");
                }
            }

            List<SelectListItem> getSoft = new List<SelectListItem>();
            foreach (var entry in selSoftware)
            {
                getSoft.Add(new SelectListItem { Text = entry.Name, Value = entry.id.ToString() });
            }
            ViewBag.Software = getSoft;

            return View(Applicant);
        }
        public async Task<IActionResult> DeleteApplicant(int Id)
        {
            using (var _httpclient = new HttpClient())
            {
                _httpclient.BaseAddress = new Uri(baseURL + "api/Applicant/");
                _httpclient.DefaultRequestHeaders.Accept.Clear();
                _httpclient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData = await _httpclient.PutAsync($"DeleteApplicantById?Id={Id}", null);

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

        private List<SelectListItem> GetGender()
        { 

            List<SelectListItem> selGender = new List<SelectListItem>();
            var selItem = new SelectListItem()
            {
                Value = "",
                Text = "Select Gender"
            };
            selGender.Insert(0, selItem);
            selItem = new SelectListItem()
            {
                Value = "Male",
                Text = "Male"
            };
            selGender.Add( selItem);
            selItem = new SelectListItem()
            {
                Value = "Female",
                Text = "Female"
            };
            selGender.Add(selItem);
            return selGender; 
        
        }

        private List<SelectListItem> GetSoftware()
        {

            List<SelectListItem> selSoftware = new List<SelectListItem>();
            var selItem = new SelectListItem()
            {
                Value = "0",
                Text = "Select Software"
            };
            selSoftware.Insert(0, selItem);
            selItem = new SelectListItem()
            {
                Value = "1",
                Text = "Microsoft"
            };
            selSoftware.Add(selItem);
            selItem = new SelectListItem()
            {
                Value = "2",
                Text = "Linux"
            };
            selSoftware.Add(selItem);
            return selSoftware;

        }
        private async Task<List<Software>> GetSoftware1()
        {

            List<Software> selSoftware = new List<Software>();
            //selSoftware.Add(new Software { id = 3, Name = "VS Code" });
            // return selSoftware;


            using (var _httpclient = new HttpClient())
            {
                _httpclient.BaseAddress = new Uri(baseURL + "api/Software/");
                _httpclient.DefaultRequestHeaders.Accept.Clear();
                _httpclient.DefaultRequestHeaders.Accept.Add
                    (new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage getData =await _httpclient.GetAsync("");
                if (getData.IsSuccessStatusCode)
                {
                    string result = getData.Content.ReadAsStringAsync().Result;
                    selSoftware = JsonConvert.DeserializeObject<List<Software>>(result);
                }
                else
                {
                    
                }

                return selSoftware;

            }
        }
        private List<SelectListItem> GetRating()
        {

            List<SelectListItem> selRating = new List<SelectListItem>();
            var selItem = new SelectListItem()
            {
                Value = "0",
                Text = "Select Rating"
            };
            selRating.Insert(0, selItem);
            
            for(int i=1; i<11;i++)
            {
                selItem = new SelectListItem()
                {
                    Value = i.ToString(),
                    Text = i.ToString()
                };
                selRating.Add(selItem);
            }
            return selRating;

        }
        private string GetUploadedFileName(ApplicantDTO applicant)
        {
            string uniqueFileName = null;
            if (applicant.ProfilePhoto != null)
            {
                string uploadsFolder = Path.Combine(_webHost.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + applicant.ProfilePhoto.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                     applicant.ProfilePhoto.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

    }
}
