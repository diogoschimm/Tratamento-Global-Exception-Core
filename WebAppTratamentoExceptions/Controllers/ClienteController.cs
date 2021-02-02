using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebAppTratamentoExceptions.Db;
using WebAppTratamentoExceptions.Models;
using WebAppTratamentoExceptions.Utils;

namespace WebAppTratamentoExceptions.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly DataBase _db;

        public ClienteController(DataBase db)
        {
            this._db = db;
        }

        [HttpGet]
        public IEnumerable<Cliente> Get()
        {
            return this._db.Clientes;
        }

        [HttpGet("{id}")]
        public Cliente Get(int id)
        {
            var cliente = GetById(id);
            if (cliente == null)
                throw new ExceptionNotFound($"Cliente de Id {id} não localizado no sistema");

            return cliente;
        }

        [HttpPost]
        public void Post([FromBody] Cliente value)
        {
            value.Validar();

            var clienteResult = GetById(value.IdCliente);
            if (clienteResult != null)
                throw new ExceptionWarning("Cliente já existe com esse código no sistema");

            this._db.Clientes.Add(value);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Cliente value)
        {
            if (value.IdCliente != id)
                throw new ExceptionNotFound("Informe o ID Cliente igual ao ID da Url");

            value.Validar();

            var cliente = GetById(id);
            if (cliente == null)
                throw new ExceptionNotFound($"Cliente de Id {id} não localizado no sistema");

            cliente.NomeCliente = value.NomeCliente;
            cliente.DataNascimento = value.DataNascimento;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var cliente = GetById(id);
            if (cliente == null)
                throw new ExceptionNotFound($"Cliente de Id {id} não localizado no sistema");

            this._db.Clientes.Remove(cliente);
        }

        private Cliente GetById(int id) =>
            this._db.Clientes.FirstOrDefault(c => c.IdCliente == id);
    }
}
