using System;
using WebAppTratamentoExceptions.Utils;

namespace WebAppTratamentoExceptions.Models
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string NomeCliente { get; set; }
        public DateTime? DataNascimento { get; set; }

        public void Validar()
        {
            if (IdCliente <= 0)
                throw new ExceptionWarning("ID do Cliente é obrigatório");

            if (string.IsNullOrEmpty(this.NomeCliente))
                throw new ExceptionWarning("Nome do Cliente é obrigatório");

            if (this.DataNascimento.HasValue)
            {
                if (this.DataNascimento.Value > DateTime.Now.Date)
                    throw new ExceptionWarning("Data de Nascimento não pode ser maior que a data atual do sistema");
            }
        }
    }
}
