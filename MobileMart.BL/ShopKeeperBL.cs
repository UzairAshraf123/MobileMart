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
        ICompanyRepository companyrepo = new CompanyRepository();
        ICategoryRepository categoryrepo = new CategoryRepository();
        IProductRepository productrepo = new ProductRepository();
        IColorRepository colorrepo = new ColorRepository();
        public void AddCompany(AddCompanyViewModel viewModel)
        {
            Company entity = new Company();
            entity.CompanyName = viewModel.CompanyName;
            entity.CompanyLogo = viewModel.CompanyLogo;
            companyrepo.insert(entity);

        }
        public void AddCategory(AddCategoryViewModel viewmodel)
        {
            var fileName = Path.GetFileNameWithoutExtension(viewmodel.CategoryImage.FileName);
            fileName += DateTime.Now.Ticks + Path.GetExtension(viewmodel.CategoryImage.FileName);
            var basePath = "~/Content/ShopOwner/Category/Images/";
            var path = Path.Combine(HttpContext.Current.Server.MapPath(basePath), fileName);
            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Content/Category/Product/Images/"));

            Category entity = new Category();
            entity.CategoryName = viewmodel.CategoryName;
            entity.CategoryImage = basePath + fileName;
            categoryrepo.insert(entity);

        }
        public IEnumerable<Company> GetCompany()
        {
            return companyrepo.GetCompany(); 
        }
        public IEnumerable<Category> GetCategory()
        {
            return categoryrepo.GetCategory();
        }
        public IEnumerable<Color> GetColor()
        {
            return colorrepo.GetColors();
        }
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
                viewmodel.ProductImage = item.ProductImage1;
                viewmodel.ProductDetail = item.ProductDetails;
                viewmodellist.Add(viewmodel);
            }
            return viewmodellist;
        }
        public void AddProduct(AddProductViewModel viewmodel)
        {
            var fileName = Path.GetFileNameWithoutExtension(viewmodel.ProductImage.FileName);
            fileName += DateTime.Now.Ticks + Path.GetExtension(viewmodel.ProductImage.FileName);
            var basePath = "~/Content/ShopOwner/Product/Images/";
            var path = Path.Combine(HttpContext.Current.Server.MapPath(basePath), fileName);
            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Content/ShopOwner/Product/Images/"));
            viewmodel.ProductImage.SaveAs(path);

            Product entity = new Product();
            entity.CategoryID = viewmodel.Category;
            entity.CompanyID = viewmodel.Company;
            entity.ColorID = viewmodel.Color;
            entity.ProductName = viewmodel.ProductName;
            entity.ProductImage1 = basePath + fileName;
            entity.ProductDetails = viewmodel.ProductDetail;
            productrepo.insert(entity);
        }
        public string DeleteProduct(int id)
        {
            productrepo.delete(id);
            return "product delete";
        }
        public AddProductViewModel  UpdteProduct(int id)
        {
            var product = productrepo.GetProduct().Where(s => s.ProductID == id).FirstOrDefault();
            AddProductViewModel viewmodel = new AddProductViewModel();
            viewmodel.id = product.ProductID;
            viewmodel.ProductName = product.ProductName;
            viewmodel.ProductImagePath = product.ProductImage1;
            viewmodel.ProductDetail = product.ProductDetails;
            return (viewmodel);
        }
        public void UpdateProduct(AddProductViewModel viewmodel)
        {
            Product entity = new Product();
            if(viewmodel.ProductImagePath!=null)
            {
                var fileName = Path.GetFileNameWithoutExtension(viewmodel.ProductImage.FileName);
                fileName += DateTime.Now.Ticks + Path.GetExtension(viewmodel.ProductImage.FileName);
                var basePath = "~/Content/ShopOwner/Product/Images/";
                var path = Path.Combine(HttpContext.Current.Server.MapPath(basePath), fileName);
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Content/ShopOwner/Product/Images/"));
                viewmodel.ProductImage.SaveAs(path);
            }
            else
            {
                entity.ProductImage1 = viewmodel.ProductImagePath;
            }
            entity.ProductID = viewmodel.id;
            entity.CategoryID = viewmodel.Category;
            entity.CompanyID = viewmodel.Company;
            entity.ColorID = viewmodel.Color;
            entity.ProductName = viewmodel.ProductName;
            entity.ProductDetails = viewmodel.ProductDetail;
            productrepo.update(entity);
        }
    }
}
