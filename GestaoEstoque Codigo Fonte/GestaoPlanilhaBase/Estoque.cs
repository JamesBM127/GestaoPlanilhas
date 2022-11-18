using System.ComponentModel.DataAnnotations;

namespace GestaoPlanilhaBase
{
    public class Estoque
    {
        [Key]
        public int Id { get; set; }
        public string Produto { get; set; }
        public int Quantidade { get; set; }
        public double ValorUnitario { get; set; }
        public double ValorTotal { get; set; }
        public string Value;

        public Estoque()
        {
            Value = ValorTotal.ToString("C");
        }
    }
}
