using MobileMart.DB.Model;
using MobileMart.DB.ViewModel;
using MobileMart.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileMart.BL
{
    public class OrderBL
    {
        public void AddOrderDetail(AddOrderDetailViewModel viewModel)
        {
            IOrderDetailRepository repo = new OrderDetailRepository();
            OrderDetail entity = new OrderDetail();
            entity.OrderID = viewModel.OrderID;
            entity.ProductID = viewModel.ProductID;
            entity.Quantity = viewModel.Quantity;
            entity.UnitPrice = viewModel.UnitPrice;
            repo.Insert(entity);
        }
        public Order GetOrderByPayPalReference(string PayPalReference)
        {
            IOrderRepository repo = new OrderRepository();
            return repo.Get().Where(s => s.PayPalReference == PayPalReference).FirstOrDefault();
        }
        public void AddOrder(AddOrderViewModel viewModel)
        {
            IOrderRepository orderRepo = new OrderRepository();
            var entity = new Order();
            entity.CustomerID = viewModel.CustomerID;
            entity.PaymentID = viewModel.PaymentID ;
            entity.CreatedOn = viewModel.CreatedOn ;
            entity.Tax = viewModel.Tax;
            entity.SubTotal = viewModel.SubTotal;
            entity.Total = viewModel.Total;
            entity.Shipping = viewModel.Shipping;
            entity.PayPalReference = viewModel.PayPalReference;
            orderRepo.Insert(entity);
        }
        public int AddOrderReturnID(AddOrderViewModel viewModel)
        {
            IOrderRepository orderRepo = new OrderRepository();
            var entity = new Order();
            entity.CustomerID = viewModel.CustomerID;
            entity.PaymentID = viewModel.PaymentID;
            entity.CreatedOn = viewModel.CreatedOn;
            entity.Tax = viewModel.Tax;
            entity.SubTotal = viewModel.SubTotal;
            entity.Total = viewModel.Total;
            entity.Shipping = viewModel.Shipping;
            entity.PayPalReference = viewModel.PayPalReference;
            return orderRepo.InsertAndReturnID(entity);
        }
    }
}
