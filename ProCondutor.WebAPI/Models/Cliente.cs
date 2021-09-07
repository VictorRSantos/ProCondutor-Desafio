using System;

namespace ProCondutor.WebAPI.Models
{
    public class Cliente
    {
        public Cliente() { }

        public Cliente(int id, string nome, DateTime dataNascimento, string observacao, bool ativo)
        {
            Id = id;
            Nome = nome;
            DataNascimento = dataNascimento;
            Observacao = observacao;
            Ativo = ativo;
        }
  
        public int Id { get; set; }

        public string Nome { get; set; }

        public DateTime DataNascimento { get; set; }

        public string Observacao { get; set; }

        public bool Ativo { get; set; }
    }
}
