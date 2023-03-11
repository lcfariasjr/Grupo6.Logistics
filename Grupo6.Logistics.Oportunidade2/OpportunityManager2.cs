using Grupo6.Logistcs.Arquitetura;
using Grupo6.Logistics.Arquitetura;
using Grupo6.Logistics.Oportunidade2.Controller;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupo6.Logistics.Oportunidade2
{
    public class OpportunityManager2 : PluginCore
    {
        public override void ExecutePlugin(IServiceProvider serviceProvider)
        {
            Entity opportuniy2 = new Entity("opportunity");
            Entity opportuniy = Context.InputParameters["Target"] as Entity;

            IOrganizationService ambient2 = PluginConnection.GetService();

            OppController2 oppControllerPrincipal = new OppController2(Service);
            OppController2 oppControllersecundario = new OppController2(ambient2);

            string nomeUsuario = oppControllerPrincipal.GetNameByUserId(((EntityReference)opportuniy.Attributes["ownerid"]).Id);
            Guid userId = oppControllersecundario.GetUserIdByName(nomeUsuario);

            string codMoeda = oppControllerPrincipal.GetCodeSymbolById(((EntityReference)opportuniy.Attributes["transactioncurrencyid"]).Id);
            Guid moedaId = oppControllersecundario.GetCurrencyIdByName(codMoeda);
            if (codMoeda != null)
                opportuniy2.Attributes["transactioncurrencyid"] = new EntityReference("transactioncurrency", moedaId);


            if (opportuniy.Contains("parentcontactid"))
            {
                string cpfcontato = oppControllerPrincipal.GetCPFContacById(((EntityReference)opportuniy.Attributes["parentcontactid"]).Id);
                Guid contactid = oppControllersecundario.GetContactIdByCpf(cpfcontato);
                if (cpfcontato != null)
                    opportuniy2.Attributes["parentcontactid"] = new EntityReference("contact", contactid);
            }
            if (opportuniy.Contains("parentaccountid"))
            {
                string cnpj = oppControllerPrincipal.GetCnpjAccountById(((EntityReference)opportuniy.Attributes["parentaccountid"]).Id);
                Guid accounId = oppControllersecundario.GetAccountIdByCnpj(cnpj);
                if (cnpj != null)
                    opportuniy2.Attributes["parentaccountid"] = new EntityReference("account", accounId);
            }

            if (opportuniy.Contains("purchasetimeframe"))
                opportuniy2.Attributes["purchasetimeframe"] = opportuniy.Attributes["purchasetimeframe"];

            if (opportuniy.Contains("budgetamount"))
                opportuniy2.Attributes["budgetamount"] = opportuniy.Attributes["budgetamount"];

            if (opportuniy.Contains("purchaseprocess"))
                opportuniy2.Attributes["purchaseprocess"] = opportuniy.Attributes["purchaseprocess"];

            if (opportuniy.Contains("msdyn_forecastcategory"))
                opportuniy2.Attributes["msdyn_forecastcategory"] = opportuniy.Attributes["msdyn_forecastcategory"];

            if (opportuniy.Contains("description"))
                opportuniy2.Attributes["description"] = opportuniy.Attributes["description"];

            if (opportuniy.Contains("currentsituation"))
                opportuniy2.Attributes["currentsituation"] = opportuniy.Attributes["currentsituation"];

            if (opportuniy.Contains("customerneed"))
                opportuniy2.Attributes["customerneed"] = opportuniy.Attributes["customerneed"];

            if (opportuniy.Contains("proposedsolution"))
                opportuniy2.Attributes["proposedsolution"] = opportuniy.Attributes["proposedsolution"];

            if (opportuniy.Contains("isrevenuesystemcalculated"))
                opportuniy2.Attributes["isrevenuesystemcalculated"] = opportuniy.Attributes["isrevenuesystemcalculated"];

            if (opportuniy.Contains("pricelevelid"))
            {

                string cpfcontato = oppControllerPrincipal.GetNameListPriceById(((EntityReference)opportuniy.Attributes["pricelevelid"]).Id);
                Guid contactid = oppControllersecundario.GetListPriceIdByName(cpfcontato);
                if (contactid != null)
                    opportuniy2.Attributes["pricelevelid"] = new EntityReference("contact", contactid);

            }

            if (opportuniy.Contains("totallineitemamount"))
                opportuniy2.Attributes["totallineitemamount"] = opportuniy.Attributes["totallineitemamount"];

            if (opportuniy.Contains("totallineitemamount"))
                opportuniy2.Attributes["totallineitemamount"] = opportuniy.Attributes["totallineitemamount"];

            if (opportuniy.Contains("discountpercentage"))
                opportuniy2.Attributes["discountpercentage"] = opportuniy.Attributes["discountpercentage"];

            if (opportuniy.Contains("discountamount"))
                opportuniy2.Attributes["discountamount"] = opportuniy.Attributes["discountamount"];

            if (opportuniy.Contains("totalamountlessfreight"))
                opportuniy2.Attributes["totalamountlessfreight"] = opportuniy.Attributes["totalamountlessfreight"];

            if (opportuniy.Contains("freightamount"))
                opportuniy2.Attributes["freightamount"] = opportuniy.Attributes["freightamount"];

            if (opportuniy.Contains("totaltax"))
                opportuniy2.Attributes["totaltax"] = opportuniy.Attributes["totaltax"];

            if (opportuniy.Contains("totalamount"))
                opportuniy2.Attributes["totalamount"] = opportuniy.Attributes["totalamount"];


            opportuniy2.Attributes["gp6_integracao"] = true;
            opportuniy2.Attributes["gp6_codopp"] = opportuniy.Attributes["gp6_codopp"];
            opportuniy2.Attributes["name"] = opportuniy.Attributes["name"];
            opportuniy2.Attributes["ownerid"] = new EntityReference("systemuser", userId);


            ambient2.Create(opportuniy2);

        }
    }
}
