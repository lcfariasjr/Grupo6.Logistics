using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using Grupo6.Logistics.Arquitetura;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace Grupo6.Logistics.ContaCNPJ
{
    public class ContaManager : PluginCore
    {
        public IOrganizationService Service { get; set; }
        public override void ExecutePlugin(IServiceProvider serviceProvider)
        {
            Context = serviceProvider.GetService(typeof(IPluginExecutionContext)) as IPluginExecutionContext;
            ServiceFactory = serviceProvider.GetService(typeof(IOrganizationServiceFactory)) as IOrganizationServiceFactory;
            Service = ServiceFactory.CreateOrganizationService(Context.UserId);

            Entity account = (Entity)this.Context.InputParameters["Target"];
            string cnpj = account.GetAttributeValue<string>("gp6_cnpj");

            if (cnpj != null)
            {
                RetrieveCpf(cnpj);
            }
        }
        private void RetrieveCpf(string cnpj)
        {
            QueryExpression query = new QueryExpression("account");
            query.Criteria.AddCondition("gp6_cnpj", ConditionOperator.Equal, cnpj);
            EntityCollection contacts = this.Service.RetrieveMultiple(query);

            if (contacts.Entities.Count() > 0)
                throw new InvalidPluginExecutionException("Já existe esse CNPJ cadastrado.");

        }
    }
}
