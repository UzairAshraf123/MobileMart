using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileMart.DB.ViewModel
{
    public class CartSessionViewModel
    {
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public ProductDetailViewModel ProductDetail { get; set; } 
    }
    public class CartDisplayViewModel
    {
        public IEnumerable<CartSessionViewModel> Cart { get; set; }

        [Required(ErrorMessage = "Your must provide a PhoneNumber..")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^((\03-?)|0)?[0-9]{10}$", ErrorMessage = "Not a valid Phone number")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "Address is required..")]
        public string Address { get; set; }
    }
}
