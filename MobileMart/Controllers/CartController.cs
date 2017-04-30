﻿
using MobileMart.BL;
using MobileMart.DB.ViewModel;
using MobileMart.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MobileMart.Utility;
using MobileMart.Repository;
using MobileMart.DB.Model;

namespace MobileMart.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
        {
            return View((List<CartSessionViewModel>)Session["Cart"]);
        }

        [HttpPost]
        public JsonResult AddItemInCart(int productID, int quantity = 1)
        {
            CartBL ajaxBL = new CartBL();
            CartSessionViewModel cartSessionVM = new CartSessionViewModel();
            List<CartSessionViewModel> list1 = new List<CartSessionViewModel>();
            if (Session["Cart"] == null)
            {
                cartSessionVM.ProductID = productID;
                cartSessionVM.Quantity = quantity;
                cartSessionVM.ProductDetail = ajaxBL.GetProductByID(productID);
                list1.Add(cartSessionVM);
                Session["Cart"] = list1 as List<CartSessionViewModel>;
            }
            else
            {
                var list2 = GetCartItems();
                if (list2.Exists(s => s.ProductID == productID))
                {
                    list2.Where(s => s.ProductID == productID).Select(w => w.Quantity++).ToList();
                    Session["Cart"] = list2 as List<CartSessionViewModel>;
                }
                else
                {
                    cartSessionVM.ProductID = productID;
                    cartSessionVM.Quantity = quantity;
                    cartSessionVM.ProductDetail = ajaxBL.GetProductByID(productID);
                    list2.Add(cartSessionVM);
                    Session["Cart"] = list2 as List<CartSessionViewModel>;
                }
            }
            return Json(Session["Cart"] as List<CartSessionViewModel>, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteItemFromCart(int productID)
        {
            var list2 = GetCartItems();
            var product = list2.Where(s => s.ProductID == productID).FirstOrDefault();
            list2.Remove(product);
            Session["Cart"] = list2 as List<CartSessionViewModel>;

            return Json(Session["Cart"] as List<CartSessionViewModel>, JsonRequestBehavior.AllowGet);
        }

        public void ClearCart()
        {
            Session.Remove("Cart");
        }

        private List<CartSessionViewModel> GetCartItems()
        {
            return (List<CartSessionViewModel>)Session["Cart"];
        }

        [Authorize(Roles ="Customer")]
        public ActionResult Checkout()
        {
            var cart = (List<CartSessionViewModel>)Session["Cart"]; 
            if (cart.Any())
            {
                // Flat rate shipping
                int shipping = 200;

                // Flat rate tax 10%
                var taxRate = 0.1M;

                var subtotal = cart.Sum(x => x.ProductDetail.Price * x.Quantity);
                var tax = Convert.ToInt32((subtotal + shipping) * taxRate);
                var total = subtotal + shipping + tax;
                // Create an Order object to store info about the shopping cart
                OrderBL orderDetailBL = new OrderBL();
                AddOrderViewModel orderObj = new AddOrderViewModel()
                {
                    CustomerID = User.Identity.GetCustomerID(),
                    PaymentID = 1,
                    CreatedOn = DateTime.Now,
                    Tax = tax,
                    Total = total,
                    Shipping = shipping,
                    SubTotal = subtotal,
                };
               
                // Get PayPal API Context using configuration from web.config
                var apiContext = GetApiContext();

                // Create a new payment object
                var payment = new PayPal.Api.Payment
                {
                    intent = "sale",
                    payer = new Payer
                    {
                        payment_method = "paypal"
                    },
                    transactions = new List<Transaction>
                    {
                        new Transaction
                        {
                            description = $"BeerPal Brewery Shopping Cart Purchase",
                            amount = new Amount
                            {
                                currency = "USD",
                                total = (orderObj.Total/100M).ToString(), // PayPal expects string amounts, eg. "20.00",
                                details = new Details()
                                {
                                    subtotal = (orderObj.SubTotal/100M).ToString(),
                                    shipping = (orderObj.Shipping/100M).ToString(),
                                    tax = (orderObj.Tax/100M).ToString()
                                }
                            },
                            item_list = new ItemList()
                            {
                                items = cart.Select(x=> new Item()
                                {
                                    description = x.ProductDetail.ProductName,
                                    currency = "USD",
                                    quantity = x.Quantity.ToString(),
                                    price = (x.ProductDetail.Price/100M).ToString(),
                                }).ToList()
                                
                            }
                        }
                    },
                    redirect_urls = new RedirectUrls
                    {
                        return_url = Url.Action("Return", "Cart", null, Request.Url.Scheme),
                        cancel_url = Url.Action("CheckoutCancel", "Cart", null, Request.Url.Scheme)
                    }
                };

                // Send the payment to PayPal
                var createdPayment = payment.Create(apiContext);

                // Save a reference to the paypal payment
                orderObj.PayPalReference = createdPayment.id;
                
                // Find the Approval URL to send our user to
                var approvalUrl =
                    createdPayment.links.FirstOrDefault(
                        x => x.rel.Equals("approval_url", StringComparison.OrdinalIgnoreCase));

                var orderID = orderDetailBL.AddOrderReturnID(orderObj);
                //Inserting data in OrderDetail Entity
                AddOrderDetailViewModel orderDetailVM = new AddOrderDetailViewModel();
                foreach (var item in cart)
                {
                    orderDetailVM.OrderID = orderID;
                    orderDetailVM.UnitPrice = (item.ProductDetail.Price) * item.Quantity;
                    orderDetailVM.Quantity = item.Quantity;
                    orderDetailVM.ProductID = item.ProductID;
                    orderDetailBL.AddOrderDetail(orderDetailVM);
                }

                // Send the user to PayPal to approve the payment
                return Redirect(approvalUrl.href);
            }

            return RedirectToAction("Cart");
        }

        public ActionResult Return(string payerId, string paymentId)
        {
            // Fetch the existing order
            OrderBL orderDetailBL = new OrderBL();
            var order = orderDetailBL.GetOrderByPayPalReference(paymentId);
            var orderID = order.OrderID;
            var orderNR = new OrderNotificationRepository();
            var orderNE = new OrderNotification();
            orderNE.OrderID = orderID;
            orderNE.Description = "New Placed Order..";
            orderNE.IsSeen = false;
            orderNE.Timestamp = DateTime.Now;
            orderNE.URL = "/Notification/OrderDetail?orderID=" + orderID;
            orderNR.Insert(orderNE);
            // Get PayPal API Context using configuration from web.config
            var apiContext = GetApiContext();

            // Set the payer for the payment
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };

            // Identify the payment to execute
            var payment = new PayPal.Api.Payment()
            {
                id = paymentId
            };
            // Execute the Payment
            var executedPayment = payment.Execute(apiContext, paymentExecution);
            //ClearCart();
            ClearCart();
            return RedirectToAction("Thankyou");
        }

        public ActionResult Thankyou()
        {
            return View();
        }

        
        public ActionResult CartErrors(string message)
        {
            ViewBag.ErrorMessage = message;
            return View();
        }
        
        public ActionResult CheckoutCancel()
        {
            return View();
        }

        private APIContext GetApiContext()
        {
            // Authenticate with PayPal
            var config = ConfigManager.Instance.GetProperties();
            var accessToken = new OAuthTokenCredential(config).GetAccessToken();
            var apiContext = new APIContext(accessToken);
            return apiContext;
        }

        
    }
}