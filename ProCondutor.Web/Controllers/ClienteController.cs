using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification.Abstractions;
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
        private readonly INotyfService _notfy;

        public ClienteController(UrlAPI configuration, INotyfService notyf)
        {
            _configuration = configuration;
            _notfy = notyf;
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
                    NotfyResposta(response.IsSuccessStatusCode);


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
                    NotfyResposta(response.IsSuccessStatusCode);

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
                    NotfyResposta(response.IsSuccessStatusCode);
                }
            }

            return RedirectToAction("Index");
        }

        private void NotfyResposta(bool isSuccessStatusCode)
        {
            if (isSuccessStatusCode)
            {
                _notfy.Success("Sucesso");
            }
            else
            {
                _notfy.Error("Verificar string de conexão do banco de dados");
            }

        }
    }

}
