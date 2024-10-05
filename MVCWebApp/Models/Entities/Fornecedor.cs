using System.ComponentModel.DataAnnotations;
using MVCWebApp.DTO;

namespace MVCWebApp.Models.Entities
{
    public class Fornecedor
    {
        public Guid Id { get; set; } 
        public string Cep { get; set; }
        public string Cnpj { get; set; }
        public string Endereco { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public Segmento Segmento { get; set; }
    }
}
