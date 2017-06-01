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
        ISupplierRepository supplierRepo = new SupplierRepository();
        ICompanyRepository companyRepo = new CompanyRepository();
        ICategoryRepository categoryRepo = new CategoryRepository();
        IProductRepository productRepo = new ProductRepository();
        //Add Company
        public string AddCompany(AddCompanyViewModel viewmodel)
        {
            var fileName1 = Path.GetFileNameWithoutExtension(viewmodel.CompanyLogo.FileName);
            fileName1 += DateTime.Now.Ticks + Path.GetExtension(viewmodel.CompanyLogo.FileName);
            var basePath = "~/Content/Campanies/" + viewmodel.CompanyName + "/Logo/";
            var path1 = Path.Combine(HttpContext.Current.Server.MapPath(basePath), fileName1);
            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Content/Campanies/" + viewmodel.CompanyName + "/Logo/"));
            viewmodel.CompanyLogo.SaveAs(path1);
           

            Company entity = new Company();
            entity.CompanyName = viewmodel.CompanyName;
            entity.CompanyLogo = basePath + fileName1;
            companyRepo.insert(entity);
            return "Company has been added successfully..";
        }
        //Add Supplier
        public int AddSupplier(AddSupplierViewModel viewmodel)
        {
            Supplier entity = new Supplier();
            entity.SupplierName = viewmodel.SupplierName;
            entity.SupplierContact = viewmodel.SupplierContact;
            entity.SupplierAddress = viewmodel.SupplierAddress;
            entity.CNIC = viewmodel.CNIC;
            entity.CreatedOn = viewmodel.CreatedON;
            entity.ShopID = viewmodel.ShopID;
            var supplierID = supplierRepo.InsertAndGetID(entity);

            var supplierNR = new SupplierNotificationRepository();
            var supplierNE = new SupplierNotification();
            supplierNE.SupplierID = supplierID;
            supplierNE.Description = "New Supplier...";
            supplierNE.IsSeen = false;
            supplierNE.URL = "/ShopOwner/DisplaySupplier/" + supplierID;
            supplierNE.Timestamp = DateTime.Now;
            supplierNR.Insert(supplierNE);
            return supplierID;
        }
        //GetCompany
        public IEnumerable<Company> GetCompany()
        {
            return companyRepo.GetCompany(); 
        }
        //GetCategory
        public IEnumerable<Category> GetCategory()
        {
            return categoryRepo.GetCategory().Where(s=> s.ParentCategory == null);
        }
        //GetSupplier
        public IEnumerable<DisplaySupplierViewModel> GetSupplier(int shopID)
        {
            var supplier= supplierRepo.GetSupplierByID(shopID);
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
        //GetProducts
        public IEnumerable<DisplayProductViewModel> GetProduct(int shopID)
        {
            var products= productRepo.GetProduct(shopID);
            List<DisplayProductViewModel> viewmodellist = new List<DisplayProductViewModel>();
            foreach(var item in products)
            {
                DisplayProductViewModel viewmodel = new DisplayProductViewModel();
                viewmodel.ProductID = item.ProductID;
                viewmodel.Category = categoryRepo.GetCategory().FirstOrDefault(s => s.CategoryID == item.CategoryID).CategoryName;
                viewmodel.Company = companyRepo.GetCompany().FirstOrDefault(s => s.CompanyID == item.CompanyID).CompanyName;
                viewmodel.ProductName = item.ProductName;
                viewmodel.ProductImage1 = item.ProductImage1;
                viewmodel.ProductImage2 = item.ProductImage2;
                viewmodel.ProductImage3 = item.ProductImage3;
                viewmodel.ProductImage4 = item.ProductImage4;
                viewmodel.ProductDetail = item.ProductDetails;
                viewmodel.Color = item.ProductColor;
                viewmodel.CreatedOn = item.CreatedOn;
                viewmodel.IMEI = item.IMEI;
                viewmodel.price = item.Price;
                viewmodel.New = item.IsOld;
                viewmodel.IsActive = item.IsActive;
                viewmodel.IsFeatured = item.IsFeature;
                viewmodel.Quantity = item.Quantity;
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


            var basePath = "~/Content/Shops/" + viewmodel.ShopID + "/Product/" + viewmodel.ProductID + "/Images/";
            var path1 = Path.Combine(HttpContext.Current.Server.MapPath(basePath), fileName1);
            var path2 = Path.Combine(HttpContext.Current.Server.MapPath(basePath), fileName2);
            var path3 = Path.Combine(HttpContext.Current.Server.MapPath(basePath), fileName3);
            var path4 = Path.Combine(HttpContext.Current.Server.MapPath(basePath), fileName4);

            Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Content/Shops/" + viewmodel.ShopID + "/Product/" + viewmodel.ProductID + "/Images/"));

            viewmodel.ProductImage1.SaveAs(path1);
            viewmodel.ProductImage2.SaveAs(path2);
            viewmodel.ProductImage3.SaveAs(path3);
            viewmodel.ProductImage4.SaveAs(path4);

            Product entity = new Product();
            entity.CategoryID = viewmodel.CategoryID;
            entity.CompanyID = viewmodel.CompanyID;
            entity.SupplierID = viewmodel.SupplierID;
            entity.ProductColor = viewmodel.Color;
            entity.ProductName = viewmodel.ProductName;
            entity.ProductImage1 = basePath + fileName1;
            entity.ProductImage2 = basePath + fileName2;
            entity.ProductImage3 = basePath + fileName3;
            entity.ProductImage4 = basePath + fileName4;
            entity.ProductDetails = viewmodel.ProductDetail;
            entity.Price = viewmodel.Price;
            entity.IMEI = viewmodel.IMEI;
            entity.CreatedOn = viewmodel.CreatedOn;
            entity.Quantity = viewmodel.Quantity;
            entity.IsOld = viewmodel.IsOld;
            entity.SubCategoryID = viewmodel.SubCategoryID;
            entity.IsActive = true;
            var productID = productRepo.InsertAndGetID(entity);

            var productNE = new ProductNotification();
            var productNR = new ProductNotificationRepository();

            productNE.ProductID = productID;
            productNE.Timestamp = DateTime.Now;
            productNE.Description = "New Product added.";
            productNE.URL = "/Notification/ProductDetail?productID="+productID;
            productNE.IsSeen = false;

            productNR.Insert(productNE);
        }
        //DeleteProducts
        public string DeleteProduct(int id)
        {
            productRepo.delete(id);
            return "product deleted";
        }
        //DeleteSupplier
        public string DeleteSupplier(int? id)
        {
            supplierRepo.delete(id);
            return "Supplier delete";
        }
        //Show Product List For Update
        public EditSupplierViewModel UpdteSupplierlist(int id)
        {
            var Supplier = supplierRepo.GetSupplier().Where(s => s.SupplierID == id).FirstOrDefault();
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
            supplierRepo.update(entity);
        }
        //Show Product List For Update
        public EditProductViewModel UpdteProductlist(int id, int shopID)
        {
            var product = productRepo.GetProduct(shopID).Where(s => s.ProductID == id).FirstOrDefault();
            EditProductViewModel viewmodel = new EditProductViewModel();
            viewmodel.ProductID = product.ProductID;
            viewmodel.SupplierID = product.SupplierID;
            viewmodel.SupplierID = product.SupplierID;
            viewmodel.ProductName = product.ProductName;
            viewmodel.ProductImagePath1 = product.ProductImage1;
            viewmodel.ProductImagePath2 = product.ProductImage2;
            viewmodel.ProductImagePath3 = product.ProductImage3;
            viewmodel.ProductImagePath4 = product.ProductImage4;
            viewmodel.ProductDetail = product.ProductDetails;
            viewmodel.Price = product.Price;
            viewmodel.IMEI = product.IMEI;
            viewmodel.Quantity = product.Quantity;
            viewmodel.IsOld = product.IsOld;
            viewmodel.CategoryID = product.CategoryID;
            viewmodel.CompanyID = product.CompanyID;
            viewmodel.CreatedOn = product.CreatedOn;
            viewmodel.Color = product.ProductColor;
            viewmodel.SubCategoryID = product.SubCategoryID;
            return (viewmodel);
        }
        //UpdateProduct
        public void UpdateProduct(EditProductViewModel viewmodel)
        {
            Product entity = new Product();
            if (viewmodel.ProductImage1 != null && viewmodel.ProductImage2 != null && viewmodel.ProductImage3 != null && viewmodel.ProductImage4 != null)
            {
                var fileName1 = Path.GetFileNameWithoutExtension(viewmodel.ProductImage1.FileName);
                var fileName2 = Path.GetFileNameWithoutExtension(viewmodel.ProductImage2.FileName);
                var fileName3 = Path.GetFileNameWithoutExtension(viewmodel.ProductImage3.FileName);
                var fileName4 = Path.GetFileNameWithoutExtension(viewmodel.ProductImage4.FileName);
                fileName1 += DateTime.Now.Ticks + Path.GetExtension(viewmodel.ProductImage1.FileName);
                fileName2 += DateTime.Now.Ticks + Path.GetExtension(viewmodel.ProductImage2.FileName);
                fileName3 += DateTime.Now.Ticks + Path.GetExtension(viewmodel.ProductImage3.FileName);
                fileName4 += DateTime.Now.Ticks + Path.GetExtension(viewmodel.ProductImage4.FileName);

                var basePath = "~/Content/Shops/" + viewmodel.ShopID + "Product/" + viewmodel.ProductID + "Images/";
                var path1 = Path.Combine(HttpContext.Current.Server.MapPath(basePath), fileName1);
                var path2 = Path.Combine(HttpContext.Current.Server.MapPath(basePath), fileName2);
                var path3 = Path.Combine(HttpContext.Current.Server.MapPath(basePath), fileName3);
                var path4 = Path.Combine(HttpContext.Current.Server.MapPath(basePath), fileName4);

                Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Content/Shops/" + viewmodel.ShopID + "Product/" + viewmodel.ProductID + "Images/"));

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
            entity.ProductID = viewmodel.ProductID;
            entity.SupplierID = viewmodel.SupplierID;
            entity.CategoryID = viewmodel.CategoryID;
            entity.CompanyID = viewmodel.CompanyID;
            entity.ProductColor = viewmodel.Color;
            entity.ProductName = viewmodel.ProductName;
            entity.ProductDetails = viewmodel.ProductDetail;
            entity.IMEI = viewmodel.IMEI;
            entity.CreatedOn = viewmodel.CreatedOn;
            entity.Price = viewmodel.Price;
            entity.IsOld = viewmodel.IsOld;
            entity.Quantity = viewmodel.Quantity;
            productRepo.update(entity);
        }

        public int GetShopByAspID(string aspID)
        {
            IOwnerRepository ownerRepo = new OwnerRepository();
            int ownerID = ownerRepo.GetOwnerIDByUserID(aspID);

            IShopRepository shopRepo = new ShopRepository();
            int shopID = shopRepo.GetShopIDByOwnerID(ownerID);
            return shopID;
        }
        //IsactiveProducts
        public bool IsActive(int id, int shopID)
        {
            var isactive = productRepo.GetProduct(shopID).FirstOrDefault(s => s.ProductID == id).IsActive;
            return (isactive);
        }
        //IsFeatureProducts
        public bool? IsFeature(int id, int shopID)
        {
            var isFeature = productRepo.GetProduct(shopID).FirstOrDefault(s => s.ProductID == id).IsFeature;
            return (isFeature);
        }
        public bool ChangeProductStateTo(int productID, bool IsActive)
        {
            var entity = productRepo.Get().FirstOrDefault(s => s.ProductID == productID);
            entity.IsActive = IsActive;
            entity.ProductID = productID;
            return productRepo.ChangeActiveStatus(entity);
        }
        public bool? ChangeFeatureState(int productID, bool? IsFeature)
        {
            var entity = productRepo.Get().FirstOrDefault(s => s.ProductID == productID);
            entity.IsFeature = IsFeature;
            entity.ProductID = productID;
            return productRepo.ChangeFeatureStatus(entity);
        }
        public IEnumerable<Supplier> GetSuppliersByShopID(int shopID)
        {
            return supplierRepo.GetSupplierByID(shopID);
        }

        public IEnumerable<DisplayOrderViewModel> GetOrdersByShopID(int? shopID)
        {
            var orderRepo = new OrderRepository();
            var customerRepo = new CustomerRepository();
            IEnumerable<DisplayOrderViewModel> orderList = orderRepo.GetByShopID(shopID).Select(s => new DisplayOrderViewModel
            {
                OrderID = s.OrderID,
                CustomerName = customerRepo.GetCustomerByID(s.CustomerID).FirstName,
                CreatedOn = s.CreatedOn,
                Tax = s.Tax,
                Total = s.Total,
                Shipping = s.Shipping,
                SubTotal = s.SubTotal,
                PayPalReference = s.PayPalReference
            });
            return orderList;
        }

        public IEnumerable<DisplayOrderViewModel> GetOrdersByRange(int? shopID, DateTime from, DateTime to)
        {
            var orderRepo = new OrderRepository();
            var customerRepo = new CustomerRepository();
            IEnumerable<DisplayOrderViewModel> orderList = orderRepo.GetByShopID(shopID).Where(w=> w.CreatedOn >= from && w.CreatedOn <= to).Select(s => new DisplayOrderViewModel
            {
                OrderID = s.OrderID,
                CustomerName = customerRepo.GetCustomerByID(s.CustomerID).FirstName,
                CreatedOn = s.CreatedOn,
                Tax = s.Tax,
                Total = s.Total,
                Shipping = s.Shipping,
                SubTotal = s.SubTotal,
                PayPalReference = s.PayPalReference
            });
            return orderList;
        }
        //GetProducts
        public IEnumerable<DisplayProductViewModel> GetProductsByRange(int? shopID, DateTime from, DateTime to)
        {
            var products = productRepo.GetProduct(shopID).Where(s => s.CreatedOn >= from && s.CreatedOn <= to);
            IEnumerable<DisplayProductViewModel> productList = products.Select(w => new DisplayProductViewModel
            {
                CreatedOn = w.CreatedOn,
                Category = w.Category.CategoryName,
                Color = w.ProductColor,
                Company = w.Company.CompanyName,
                IMEI = w.IMEI,
                IsActive = w.IsActive,
                New = w.IsOld , 
                price = w.Price,
                ProductDetail =w.ProductDetails , 
                ProductID = w.ProductID, 
                ProductImage1 = w.ProductImage1 ,
                ProductImage2 = w.ProductImage2,
                ProductImage3 = w.ProductImage3, 
                ProductImage4 = w.ProductImage4, 
                ProductName = w.ProductName,
            });
            return productList;
            //foreach (var item in products)
            //{
            //    DisplayProductViewModel viewmodel = new DisplayProductViewModel();
            //    viewmodel.ProductID = item.ProductID;
            //    viewmodel.Category = categoryRepo.GetCategory().FirstOrDefault(s => s.CategoryID == item.CategoryID).CategoryName;
            //    viewmodel.Company = companyRepo.GetCompany().FirstOrDefault(s => s.CompanyID == item.CompanyID).CompanyName;
            //    viewmodel.ProductName = item.ProductName;
            //    viewmodel.ProductImage1 = item.ProductImage1;
            //    viewmodel.ProductImage2 = item.ProductImage2;
            //    viewmodel.ProductImage3 = item.ProductImage3;
            //    viewmodel.ProductImage4 = item.ProductImage4;
            //    viewmodel.ProductDetail = item.ProductDetails;
            //    viewmodel.Color = item.ProductColor;
            //    viewmodel.CreatedOn = item.CreatedOn;
            //    viewmodel.IMEI = item.IMEI;
            //    viewmodel.price = item.Price;
            //    viewmodel.New = item.IsOld;
            //    viewmodel.IsActive = item.IsActive;
            //    viewmodellist.Add(viewmodel);
            //}
            //return viewmodellist;
        }

        public string GetOwnerPicture(int shopID)
        {
            return new OwnerRepository().GetOwnerByShopID(shopID).OwnerPicture;
        }

        public DisplayShopViewModel GetShopAndOwnerByShopID(int? shopID)
        {
            var owner = new OwnerRepository().GetOwnerByShopID(shopID);
            var shop = new ShopRepository().GetShopByID(shopID);
            return new DisplayShopViewModel()
            {
                UserID = owner.AspNetUserID,
                OwnerCreatedOn = owner.CreatedOn,
                OwnerID = owner.OwnerID,
                OwnerName = owner.OwnerName,
                OwnerProfilePath = owner.OwnerPicture,
                Contact = owner.OwnerContact,
                ShopID = shop.ShopID,
                ShopAddress = shop.ShopAddress,
                ShopCreatedOn = shop.CreatedOn,
                ShopLogo = shop.ShopLogo,
                ShopName = shop.ShopName
            };
        }
    }
}
