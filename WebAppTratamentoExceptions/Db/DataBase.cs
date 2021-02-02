using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppTratamentoExceptions.Models;

namespace WebAppTratamentoExceptions.Db
{
    public class DataBase
    {
        public DataBase()
        {
            Clientes = new List<Cliente>
            {
                new Cliente { IdCliente = 1, NomeCliente = "Diogo Rodrigo" },
                new Cliente { IdCliente = 2, NomeCliente = "Gelson Gilmar" },
                new Cliente { IdCliente = 3, NomeCliente = "Dilmar Douglas" }
            };
        }

        public IList<Cliente> Clientes { get; set; }
    }
}
