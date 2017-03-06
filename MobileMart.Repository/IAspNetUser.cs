using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileMart.Repository
{
    public interface IAspNetUser
    {
         void DeleteUser(string userID);
    }
}
