using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grupo6.Logistcs.Arquitetura;
using Grupo6.Logistics.Arquitetura;
using Grupo6.Logistics.Produto.Model;
using Grupo6.Logistics.Produto.Repository;
using Microsoft.Xrm.Sdk;

namespace Grupo6.Logistics.Produto.Plugin
{
    public class ProductManager : PluginCore
    {
        public override void ExecutePlugin(IServiceProvider serviceProvider)
        {
            ITracingService tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            Entity product = (Entity)this.Context.InputParameters["Target"];
            tracingService.Trace("antes da conexao");
            IOrganizationService ambiente2 = PluginConnection.GetService();
            tracingService.Trace("depois da conexao");
            ProductModel productModel = new ProductModel();


            ProductRepository productRepository = new ProductRepository(ambiente2);
            productModel.Nome = product.GetAttributeValue<string>("name");
            productModel.ProdutoId = product.GetAttributeValue<string>("productnumber");
            //productModel.Primario = product.GetAttributeValue<Guid>("parentproductid");
            //productModel.ValidoAPartir = product.GetAttributeValue<DateTime>("validfromdate");
            //productModel.ValidoAte = product.GetAttributeValue<DateTime>("validtodate");
            //productModel.Descricao = product.GetAttributeValue<string>("description");
            productModel.GrupoUnidade = product.GetAttributeValue<EntityReference>("defaultuomscheduleid").Id;
            productModel.UnidadePadrao = product.GetAttributeValue<EntityReference>("defaultuomid").Id;
            //productModel.ListaPrecoPadrao = product.GetAttributeValue<EntityReference>("pricelevelid").Id;
            tracingService.Trace("antes que quantitydecimal");
            productModel.SuporteDecimais = product.GetAttributeValue<int>("quantitydecimal");
            tracingService.Trace("depois que quantitydecimal");
            //productModel.Assunto = product.GetAttributeValue<Guid>("subjectid");
            //productModel.CustoAtual = product.GetAttributeValue<Money>("currentcost").Value;
            //productModel.CustoPadrao = product.GetAttributeValue<Money>("standardcost").Value;
            //productModel.PrecoDeLista = product.GetAttributeValue<Money>("price").Value;

            productRepository.Create(productModel);
        }
    }
}
