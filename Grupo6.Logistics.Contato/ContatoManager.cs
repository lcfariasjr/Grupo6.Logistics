using System;
using System.Collections.Generic;
using System.Linq;
using Grupo6.Logistics.Arquitetura;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;

namespace Grupo6.Logistics.Contato
{
    public class ContatoManager : PluginCore
    {
        public IOrganizationService Service { get; set; }
        public override void ExecutePlugin(IServiceProvider serviceProvider)
        {
            Context = serviceProvider.GetService(typeof(IPluginExecutionContext)) as IPluginExecutionContext;
            ServiceFactory = serviceProvider.GetService(typeof(IOrganizationServiceFactory)) as IOrganizationServiceFactory;
            Service = ServiceFactory.CreateOrganizationService(Context.UserId);

            Entity contact = (Entity)this.Context.InputParameters["Target"];
            string cpf = contact.GetAttributeValue<string>("gp6_cpf");

            if(cpf != null)
            {
                RetrieveCpf(cpf);
            }
        }    
        private void RetrieveCpf(string cpf)
        {
            QueryExpression query = new QueryExpression("contact");
            query.Criteria.AddCondition("gp6_cpf", ConditionOperator.Equal, cpf);
            EntityCollection contacts = this.Service.RetrieveMultiple(query);

            if (contacts.Entities.Count() > 0)
                throw new InvalidPluginExecutionException ("Já existe esse cpf cadastrado.") ;
           
        }
    }
}
