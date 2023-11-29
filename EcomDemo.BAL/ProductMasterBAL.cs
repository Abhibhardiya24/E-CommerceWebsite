using EcomDemo.DAL;
using EcomDemo.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace EcomDemo.BAL
{
    public class ProductMasterBAL
    {
        public AddEditProductModel GetProduct(AddEditProductModel aepm)
        {
            ProductMasterDAL pmd = new ProductMasterDAL();
            aepm = pmd.GetProduct(aepm);
            return aepm;
        }

        public AddEditProductModel CreateProduct(AddEditProductModel aepm)
        {
            ProductMasterDAL pmd = new ProductMasterDAL();
            aepm = pmd.CreateProduct(aepm); 
            return aepm;
        }

        public AddEditProductModel EditProduct(int ID)
        {
            AddEditProductModel aepm = new AddEditProductModel();
            ProductMasterDAL pmd = new ProductMasterDAL();
            aepm = pmd.EditProduct(ID);
            return aepm;
        }

        public AddEditProductModel EditProduct(AddEditProductModel aepm)
        {
            ProductMasterDAL pmd = new ProductMasterDAL();
            aepm = pmd.EditProduct(aepm);
            return aepm;
        }

        public AddEditProductModel DeleteProduct(int ID)
        {
            AddEditProductModel aepm = new AddEditProductModel();
            ProductMasterDAL pmd = new ProductMasterDAL();
            aepm = pmd.DeleteProduct(ID);
            return aepm;
        }

        public AddEditProductModel AddPhoto(AddEditProductModel aepm)
        {
           // AddEditProductModel aepm = new AddEditProductModel();
            ProductMasterDAL pmd = new ProductMasterDAL();
            aepm= pmd.AddPhoto(aepm);
            return aepm;
        }
        
        public AddEditProductModel GetProductPhoto(int ID)
        {
            AddEditProductModel aepm = new AddEditProductModel();
            ProductMasterDAL pmd = new ProductMasterDAL();
            aepm = pmd.GetProductPhoto(ID);
            return aepm;
        }

        public AddEditProductModel DeleteProductDetailPhoto(int PdID)
        {
            AddEditProductModel aepm = new AddEditProductModel();
            ProductMasterDAL pmd = new ProductMasterDAL();
            aepm = pmd.DeleteProductDetailPhoto(PdID);
            return aepm;
        }
    }
}
