using System.ComponentModel.DataAnnotations;

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
        public byte Segmento { get; set; }

        [Required(ErrorMessage = "O CEP é obrigatório")]
        [StringLength(8, ErrorMessage = "O CEP deve ter 8 caracteres")]
        public int Cep { get; set; }
        public string Endereco { get; set; }
        public string Image { get; set; }
        public bool Subscribed { get; set; } // Propriedade para captura da inscrição
    }
}
