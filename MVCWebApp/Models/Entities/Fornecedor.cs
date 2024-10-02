namespace MVCWebApp.Models.Entities
{
    public class Fornecedor
    {
        //Endentificador Unico
        public Guid Id {  get; set; }

        public string Name { get; set; }

        public string Cnpj { get; set; }

        public byte Segmento { get; set; }

        public int Cep { get; set; }

        public string Endereco { get; set; }

        public string Image { get; set; }

    }
}
