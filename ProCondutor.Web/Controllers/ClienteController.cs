using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ProCondutor.Domain.Helper;
using ProCondutor.Domain.Models;
using ProCondutor.Web.Configuracao;
using ProCondutor.Web.Models;

namespace ProCondutor.Web.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ClienteViewModel model = new ClienteViewModel();
        private readonly Uri uri;
        private readonly UrlAPI _configuration;


        public ClienteController(UrlAPI configuration)
        {
            _configuration = configuration;
            uri = new Uri($"{_configuration.URL}");
        }
        public async Task<IActionResult> Index()
        {

           
            using (var httpClient = new HttpClient())
            {

                using (var response = await httpClient.GetAsync($"{uri.AbsoluteUri}/api/Cliente"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        model.Clientes = JsonConvert.DeserializeObject<List<Cliente>>(apiResponse);

                    }
                    else
                    {
                        model.Clientes = new List<Cliente>();
                    }

                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Salvar(Cliente cliente)
        {

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(cliente), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync($"{uri.AbsoluteUri}/api/Cliente", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{uri.AbsoluteUri}/api/Cliente/{id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    model.Cliente = JsonConvert.DeserializeObject<Cliente>(apiResponse);

                }
            }
            return View("Editar", model);
        }

        [HttpPost]     
        public async Task<IActionResult> Editar(Cliente cliente)
        {

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(cliente), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PutAsync($"{uri.AbsoluteUri}/api/Cliente/{cliente.Id}", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                }
            }
            return RedirectToAction("Index");

         
        }
              
      
        public async Task<IActionResult> Excluir(int Id)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.DeleteAsync($"{uri.AbsoluteUri}/api/Cliente/{Id}"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return RedirectToAction("Index");
        }


    }

}
