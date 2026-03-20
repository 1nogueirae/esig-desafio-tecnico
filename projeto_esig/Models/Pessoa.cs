using System;

namespace projeto_esig.Models

{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cidade { get; set; }
        public string Email { get; set; }
        public string Cep { get; set; }
        public string Endereco { get; set; }
        public string Pais { get; set; }
        public string Usuario { get; set; }
        public string Telefone { get; set; }
        public DateTime DataNascimento { get; set; }
        public int CargoId { get; set; }
    }
}