using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupo6.Logistics.Produto.Model
{
    public class ProductModel
    {
        public string Nome { get; set; }
        public string ProdutoId { get; set; }
        public Guid Primario { get; set; }
        public DateTime ValidoAPartir { get; set; }
        public DateTime ValidoAte { get; set; }
        public string Descricao { get; set; }
        public Guid GrupoUnidade { get; set; }
        public Guid UnidadePadrao { get; set; }
        public Guid ListaPrecoPadrao { get; set; }
        public int SuporteDecimais { get; set; }
        public Guid Assunto { get; set; }
        public decimal CustoAtual { get; set; }
        public decimal CustoPadrao { get; set; }
        public decimal PrecoDeLista { get; set; }
    }
}
