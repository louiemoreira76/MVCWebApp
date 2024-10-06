using System.ComponentModel.DataAnnotations;
using MVCWebApp.DTO;

namespace MVCWebApp.DTOs
{
    public class FornecedorDTO
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O CNPJ é obrigatório")]
        [StringLength(14, ErrorMessage = "O CNPJ deve ter 14 caracteres")]
        public string Cnpj { get; set; }

        [Required(ErrorMessage = "O Segmento é obrigatório")]
        public Segmento Segmento { get; set; }

        [RegularExpression(@"^\d{5}-?\d{3}$", ErrorMessage = "O CEP deve estar no formato 00000-000")]
        public string Cep { get; set; }
        public string Endereco { get; set; }

        public string ImageUrl { get; set; }

    }
}
