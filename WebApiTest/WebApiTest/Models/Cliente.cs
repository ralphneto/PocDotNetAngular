namespace WebApiTest.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string cpf_cnpj { get; set; } = string.Empty;

        public string email { get; set; } = string.Empty;

        public string telefone { get; set; } = string.Empty;

        public string status { get; set; } = string.Empty;

        public float valor_total { get; set; }

        public float valor_atraso  { get; set; }

        public int dias_atraso { get; set; }

        public string analista { get; set; } = string.Empty;
    }
}
