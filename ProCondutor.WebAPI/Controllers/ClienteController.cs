using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProCondutor.WebAPI.ArquivoConfiguracao;
using ProCondutor.WebAPI.Data.Factory;
using ProCondutor.WebAPI.Data.Interface;
using ProCondutor.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProCondutor.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {

        private readonly IDbCliente _dbCliente;


        public ClienteController(Configuracao configuracao)
        {
            _dbCliente = FactoryClienteDB.GetDBCliente(configuracao);
        }

        [HttpGet]
        public IActionResult Get()
        {


            return Ok(_dbCliente.Clientes());
        }

        [HttpGet("{Id}")]
        public IActionResult GetPorId(int Id)
        {
            var cliente = _dbCliente.Clientes().FirstOrDefault(x => x.Id == Id);

            if (cliente is null) return BadRequest("Cliente não encontrado");

            return Ok(cliente);

        }


        [HttpGet("BuscaCliente")]
        public IActionResult GetQueryStringPorIdENome(int id, string nome)
        {
            var cliente = _dbCliente.Clientes().FirstOrDefault(x => x.Id == id && x.Nome == nome);

            if (cliente is null) return BadRequest("Cliente não encontrado");

            return Ok(cliente);
        }


        [HttpPost]
        public IActionResult Post(Cliente cliente)
        {

            var resposta = _dbCliente.Insert(cliente);

            if (!resposta) return BadRequest("Erro: Não foi possivel salvar o cliente");

            return Ok(resposta);
        }


        [HttpPut]
        [Route("{Id}")]
        public IActionResult Put(int Id, [FromBody] Cliente cliente)
        {

            var clienteEncontrado = _dbCliente.Clientes().FirstOrDefault(x => x.Id == cliente.Id);

            if (clienteEncontrado is null) return BadRequest("Erro ao buscar o cliente");

            var reposta = _dbCliente.Update(cliente);


            return Ok(cliente);
        }


        [HttpDelete]
        [Route("{Id}")]
        public IActionResult Delete(int Id)
        {

            var clienteEncontrado = _dbCliente.Clientes().FirstOrDefault(x => x.Id == Id);

            if (clienteEncontrado is null) return BadRequest("Erro ao buscar o cliente");

            var reposta = _dbCliente.Delete(Id);


            return Ok();
        }


    }
}
