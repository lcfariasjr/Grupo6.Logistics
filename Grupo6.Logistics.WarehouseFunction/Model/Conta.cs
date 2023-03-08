using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupo6.Logistics.WarehouseFunction.Model
{
    public class Conta
    {
        public Entity Account = new Entity("account");
        public string LogicalName { get; set; }
        public string CNPJ { get; set; }
        public string Telefone { get; set; }
        public string Fax { get; set; }
        public string Site { get; set; }
        public string CNPJ_Conta { get; set; }
        public string SimboloAcao { get; set; }
        public string DataUltVisita { get; set; }
        public Endereco Endereco { get; set; }
        public string CPF_Contato { get; set; }
        public string Descricao { get; set; }
        
        public Conta 

    }
}
