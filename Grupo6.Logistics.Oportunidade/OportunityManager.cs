using Grupo6.Logistcs.Conta.SharedClass.Arquitetura;
using Grupo6.Logistics.Arquitetura;
using Grupo6.Logistics.Oportunidade.Controller;
using Grupo6.Logistics.SharedClass.Arquitetura;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Grupo6.Logistics.Oportunidade
{
    public class OportunityManager : PluginCore
    {
        public override void ExecutePlugin(IServiceProvider serviceProvider)
        {
            try
            {

                if (Context.MessageName.ToLower() == "create")
                {
                   
                    OppController oppController = new OppController(Service);

                    oppController.GetCod();


                    ConexaoSegundario secundario = new ConexaoSegundario();
                    OppController oppController2 = new OppController(secundario.GetService());

                    oppController2.GetCod();

                    string[] codAmbiente1 = oppController.CodOpp.Split('-');
                    string[] codAmbiente2 = oppController2.CodOpp.Split('_');

                    if (int.Parse(codAmbiente1[1]) > int.Parse(codAmbiente2[1]))
                    {
                        oppController.InsereCod(Context);
                    }
                    else
                    {
                        oppController2.InsereCod(Context);
                    }



                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }
    }
}
