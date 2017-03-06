using MobileMart.DB.Model;
using MobileMart.DB.ViewModel;
using MobileMart.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
namespace MobileMart.BL
{
    public class ShopKeeperBL
    {
        ISupplierRepository supplierrepo = new SupplierRepository();
        ICompanyRepository companyrepo = new CompanyRepository();
        ICategoryRepository categoryrepo = new CategoryRepository();
        IProductRepository productrepo = new ProductRepository();
        IColorRepository colorrepo = new ColorRepository();
        //Add Company
        public void AddCompany(AddCompanyViewModel viewModel)
        {
            Company entity = new Company();
            entity.CompanyName = viewModel.CompanyName;
            entity.CompanyLogo = viewModel.CompanyLogo;
            companyrepo.insert(entity);

        }
        //Add Supplier
        public void AddSupplier(AddSupplierViewModel viewmodel)
        {
            Supplier entity = new Supplier();
            entity.SupplierName = viewmodel.SupplierName;
            entity.SupplierContact = viewmodel.SupplierContact;
            entity.SupplierAddress = viewmodel.SupplierAddress;
            supplierrepo.insert(entity);

        }
        //GetCompany
        public IEnumerable<Company> GetCompany()
        {
            return companyrepo.GetCompany(); 
        }
        //GetCategory
        public IEnumerable<Category> GetCategory()
        {
            return categoryrepo.GetCategory();
        }
        //GetSupplier
        public IEnumerable<DisplaySupplierViewModel> GetSupplier()
        {
            var supplier= supplierrepo.GetSupplier();
            List<DisplaySupplierViewModel> viewmodellist = new List<DisplaySupplierViewModel>();
            foreach(var item in supplier)
            {
                DisplaySupplierViewModel viewmodel = new DisplaySupplierViewModel();
                viewmodel.SupplierID = item.SupplierID;
                viewmodel.SupplierName = item.SupplierName;
                viewmodel.Supplierno = item.SupplierContact;
                viewmodel.SupplierAddress = item.SupplierAddress;
                viewmodellist.Add(viewmodel);
            }
            return viewmodellist;
        }
        //GetColors
        public IEnumerable<Color> GetColor()
        {
            return colorrepo.GetColors();
        }
        //GetProducts
        public IEnumerable<DisplayProductViewModel> GetProduct()
        {
            var products= productrepo.GetProduct();
            List<DisplayProductViewModel> viewmodellist = new List<DisplayProductViewModel>();
            foreach(var item in products)
            {
                DisplayProductViewModel viewmodel = new DisplayProductViewModel();
                viewmodel.ProductID = item.ProductID;
                viewmodel.Category = categoryrepo.GetCategory().FirstOrDefault(s => s.CategoryID == item.CategoryID).CategoryName;
                viewmodel.Company = companyrepo.GetCompany().FirstOrDefault(s => s.CompanyID == item.CompanyID).CompanyName;
                viewmodel.Color = colorrepo.GetColors().FirstOrDefault(s => s.ColorID == item.ColorID).ColorName;
                viewmodel.ProductName = item.ProductName;
                viewmodel.ProductImage1 = item.ProductImage1;
                viewmodel.ProductImage2 = item.ProductImage2;
                viewmodel.ProductImage3 = item.ProductImage3;
                viewmodel.ProductImage4 = item.ProductImage4;
                viewmodel.ProductDetail = item.ProductDetails;
                viewmodel.CreatedOn = item.CreatedOn;
                viewmodel.IMEI = item.IMEI;
                viewmodellist.Add(viewmodel);
            }
            return viewmodellist;
        }
        //AddProducts
        public void AddProduct(AddProductViewModel viewmodel)
        {
            var fileName1 = Path.GetFileNameWithoutExtension(viewmodel.ProductImage1.FileName);
            var fileName2 = Path.GetFileNameWithoutExtension(viewmodel.ProductImage2.FileName);
            var fileName3 = Path.GetFileNameWithoutExtension(viewmodel.ProductImage3.FileName);
            var fileName4 = Path.GetFileNameWithoutExtension(viewmodel.ProductImage4.FileName);
            fileName1 += DateTime.Now.Ticks + Path.GetExtension(viewmodel.ProductImage1.FileName);
            fileName2 += DateTime.Now.Ticks + Path.GetExtension(viewmodel.ProductImage2.FileName);
            fileName3 += DateTime.Now.Ticks + Path.GetExtension(viewmodel.ProductImage3.FileName);
            fileName4 += DateTime.Now.Ticks + Path.GetExtension(viewmodel.ProductImage4.FileName);
            var basePath = "~/Content/ShopOwner/Product/Images/";
            var path1 = Path.Combine(HttpContext.Current.Server.MapPath(basePath), fileName1);
            var path2 = Path.Combine(HttpContext.Current.Server.MapPath(basePath), fileName2);
            var path3 = Path.Combine(HttpContext.Current.Server.MapPath(basePath), fileName3);
            var path4 = Path.Combine(HttpContext.Current.Server.MapPath(basePath), fileName4);
            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Content/ShopOwner/Product/Images/"));
            viewmodel.ProductImage1.SaveAs(path1);
            viewmodel.ProductImage2.SaveAs(path2);
            viewmodel.ProductImage3.SaveAs(path3);
            viewmodel.ProductImage4.SaveAs(path4);

            Product entity = new Product();
            entity.CategoryID = viewmodel.Category;
            entity.CompanyID = viewmodel.Company;
            entity.ColorID = viewmodel.Color;
            entity.ProductName = viewmodel.ProductName;
            entity.ProductImage1 = basePath + fileName1;
            entity.ProductImage2 = basePath + fileName2;
            entity.ProductImage3 = basePath + fileName3;
            entity.ProductImage4 = basePath + fileName4;
            entity.ProductDetails = viewmodel.ProductDetail;
            entity.Price = viewmodel.Price;
            entity.IMEI = viewmodel.IMEI;
            entity.CreatedOn = viewmodel.CreatedOn;
            productrepo.insert(entity);
        }
        //DeleteProducts
        public string DeleteProduct(int id)
        {
            productrepo.delete(id);
            return "product delete";
        }
        //DeleteSupplier
        public string DeleteSupplier(int id)
        {
            supplierrepo.delete(id);
            return "Supplier delete";
        }
        //Show Product List For Update
        public EditSupplierViewModel UpdteSupplierlist(int id)
        {
            var Supplier = supplierrepo.GetSupplier().Where(s => s.SupplierID == id).FirstOrDefault();
            EditSupplierViewModel viewmodel = new EditSupplierViewModel();
            viewmodel.SupplierID = Supplier.SupplierID;
            viewmodel.SupplierName = Supplier.SupplierName;
            viewmodel.SupplierContact = Supplier.SupplierContact;
            viewmodel.SupplierAddress = Supplier.SupplierAddress;
            return (viewmodel);
        }
        //UpdateSupplier
        public void UpdateSupplier(EditSupplierViewModel viewmodel)
        {
            Supplier entity = new Supplier();
            entity.SupplierID = viewmodel.SupplierID;
            entity.SupplierName = viewmodel.SupplierName;
            entity.SupplierContact = viewmodel.SupplierContact;
            entity.SupplierAddress = viewmodel.SupplierAddress;
            supplierrepo.update(entity);
        }
        //Show Product List For Update
        public AddProductViewModel  UpdteProductlist(int id)
        {
            var product = productrepo.GetProduct().Where(s => s.ProductID == id).FirstOrDefault();
            AddProductViewModel viewmodel = new AddProductViewModel();
            viewmodel.id = product.ProductID;
            viewmodel.ProductName = product.ProductName;
            viewmodel.ProductImagePath1 = product.ProductImage1;
            viewmodel.ProductImagePath2 = product.ProductImage2;
            viewmodel.ProductImagePath3 = product.ProductImage3;
            viewmodel.ProductImagePath4 = product.ProductImage4;
            viewmodel.ProductDetail = product.ProductDetails;
            viewmodel.Price = product.Price;
            viewmodel.IMEI = product.IMEI;
            return (viewmodel);
        }
        //UpdateProduct
        public void UpdateProduct(AddProductViewModel viewmodel)
        {
            Product entity = new Product();
            if(viewmodel.ProductImage1!=null && viewmodel.ProductImage2 != null && viewmodel.ProductImage3 != null && viewmodel.ProductImage4 != null)
            {
                var fileName1 = Path.GetFileNameWithoutExtension(viewmodel.ProductImage1.FileName);
                var fileName2 = Path.GetFileNameWithoutExtension(viewmodel.ProductImage2.FileName);
                var fileName3 = Path.GetFileNameWithoutExtension(viewmodel.ProductImage3.FileName);
                var fileName4 = Path.GetFileNameWithoutExtension(viewmodel.ProductImage4.FileName);
                fileName1 += DateTime.Now.Ticks + Path.GetExtension(viewmodel.ProductImage1.FileName);
                fileName2 += DateTime.Now.Ticks + Path.GetExtension(viewmodel.ProductImage2.FileName);
                fileName3 += DateTime.Now.Ticks + Path.GetExtension(viewmodel.ProductImage3.FileName);
                fileName4 += DateTime.Now.Ticks + Path.GetExtension(viewmodel.ProductImage4.FileName);
                var basePath = "~/Content/ShopOwner/Product/Images/";
                var path1 = Path.Combine(HttpContext.Current.Server.MapPath(basePath), fileName1);
                var path2 = Path.Combine(HttpContext.Current.Server.MapPath(basePath), fileName2);
                var path3 = Path.Combine(HttpContext.Current.Server.MapPath(basePath), fileName3);
                var path4 = Path.Combine(HttpContext.Current.Server.MapPath(basePath), fileName4);
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Content/ShopOwner/Product/Images/"));
                viewmodel.ProductImage1.SaveAs(path1);
                viewmodel.ProductImage2.SaveAs(path2);
                viewmodel.ProductImage3.SaveAs(path3);
                viewmodel.ProductImage4.SaveAs(path4);

                entity.ProductImage1 = basePath + fileName1;
                entity.ProductImage2 = basePath + fileName2;
                entity.ProductImage3 = basePath + fileName3;
                entity.ProductImage4 = basePath + fileName4;
            }
            else
            {
                entity.ProductImage1 = viewmodel.ProductImagePath1;
                entity.ProductImage2 = viewmodel.ProductImagePath2;
                entity.ProductImage3 = viewmodel.ProductImagePath3;
                entity.ProductImage4 = viewmodel.ProductImagePath4;
            }
            entity.ProductID = viewmodel.id;
            entity.CategoryID = viewmodel.Category;
            entity.CompanyID = viewmodel.Company;
            entity.ColorID = viewmodel.Color;
            entity.ProductName = viewmodel.ProductName;
            entity.ProductDetails = viewmodel.ProductDetail;
            entity.IMEI = viewmodel.IMEI;
            entity.CreatedOn = viewmodel.CreatedOn;
            entity.Price = viewmodel.Price;
            productrepo.update(entity);
        }
    }
}
