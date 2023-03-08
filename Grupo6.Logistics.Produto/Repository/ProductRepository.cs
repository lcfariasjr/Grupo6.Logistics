using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grupo6.Logistics.Produto.Model;
using Microsoft.Xrm.Sdk;

namespace Grupo6.Logistics.Produto.Repository
{
    public class ProductRepository
    {
        private IOrganizationService Service { get; set; }
        private string logicalname { get; set; }
        public ProductRepository(IOrganizationService service)
        {
            this.Service = service;
            this.logicalname = "product";
        }

        public Guid Create(ProductModel productModel)
        {
            Entity product = new Entity(this.logicalname);
            product["name"] = productModel.Nome;
            product["productnumber"] = productModel.ProdutoId;
            //product["parentproductid"] = new EntityReference("parentproductid",productModel.Primario);
            //["validfromdate"] = productModel.ValidoAPartir;
            //product["validtodate"] = productModel.ValidoAte;
            //product["description"] = productModel.Descricao;
            product["defaultuomscheduleid"] = new EntityReference("uomschedule", Guid.Parse("2d053e2a-0772-473b-9986-eeeaad9bf44d"));
            product["defaultuomid"] = new EntityReference("uom", Guid.Parse("08ebd844-778e-4099-91cb-66bbfe356554"));
            //product["pricelevelid"] = new EntityReference("pricelevel", Guid.Parse("08ebd844-778e-4099-91cb-66bbfe356554"));
            product["quantitydecimal"] = productModel.SuporteDecimais;
            //product["subjectid"] = new EntityReference("subjectid", productModel.Assunto); 
            //product["currentcost"] = new Money(productModel.CustoAtual);
            //product["standardcost"] = new Money(productModel.CustoPadrao);
            //product["price"] = new Money(productModel.PrecoDeLista);

            Guid productId = Service.Create(product);
            return productId;
        }
    }
}
