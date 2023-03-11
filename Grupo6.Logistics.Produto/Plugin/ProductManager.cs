using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grupo6.Logistics.SharedClass;
using Grupo6.Logistics.SharedClass.Arquitetura;
using Grupo6.Logistics.Produto.Model;
using Grupo6.Logistics.Produto.Repository;
using Microsoft.Xrm.Sdk;

namespace Grupo6.Logistics.Produto.Plugin
{
    public class ProductManager : PluginCore
    {
        public IPluginExecutionContext Context { get; set; }
        public IOrganizationServiceFactory ServiceFactory { get; set; }
        public IOrganizationService Service { get; set; }
        public override void ExecutePlugin(IServiceProvider serviceProvider)
        {
            Context = serviceProvider.GetService(typeof(IPluginExecutionContext)) as IPluginExecutionContext;
            ServiceFactory = serviceProvider.GetService(typeof(IOrganizationServiceFactory)) as IOrganizationServiceFactory;
            Service = ServiceFactory.CreateOrganizationService(Context.UserId);
            ITracingService tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            Entity product = (Entity)this.Context.InputParameters["Target"];
           
            IOrganizationService ambiente2 = PluginConnection.GetService();
           
            ProductModel productModel = new ProductModel();

            ProductRepository productRepository = new ProductRepository(ambiente2, tracingService, Service);

            //Campos Obrigtórios
            productModel.Nome = product.GetAttributeValue<string>("name");
            productModel.ProdutoId = product.GetAttributeValue<string>("productnumber");
            productModel.GrupoUnidade = product.GetAttributeValue<EntityReference>("defaultuomscheduleid").Id;
            productModel.UnidadePadrao = product.GetAttributeValue<EntityReference>("defaultuomid").Id;
            productModel.SuporteDecimais = product.GetAttributeValue<int>("quantitydecimal");

            //Campos não obrigatórios, validação caso nulo

            if (product.Contains("validfromdate") && product["validfromdate"] != null)
                productModel.ValidoAPartir = product.GetAttributeValue<DateTime>("validfromdate");

            if(product.Contains("validtodate") && product["validtodate"] != null)
                productModel.ValidoAte = product.GetAttributeValue<DateTime>("validtodate");           

            if (product.Contains("currentcost") && product["currentcost"] != null)
                productModel.CustoAtual = product.GetAttributeValue<Money>("currentcost").Value;

            if (product.Contains("price") && product["price"] != null)
                productModel.PrecoDeLista = product.GetAttributeValue<Money>("price").Value;

            productRepository.Create(productModel);
        }
    }
    
}
