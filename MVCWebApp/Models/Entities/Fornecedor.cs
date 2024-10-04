using MVCWebApp.DTO;

namespace MVCWebApp.Models.Entities
{
    public class Fornecedor
    {
        //Endentificador Unico
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }

        public string Cnpj { get; set; }

        public Segmento Segmento { get; set; }

        public string Cep { get; set; }

        public string Endereco { get; set; }

        public string Image { get; set; }
    }
}
