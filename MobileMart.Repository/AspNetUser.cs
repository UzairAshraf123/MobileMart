using MobileMart.DB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileMart.Repository
{
    public class AspNetUser : IAspNetUser
    {
        private MobileMartEntities _context;
        public void DeleteUser(string userID)
        {
            _context = new MobileMartEntities();
            var user = _context.AspNetUsers.Where(s => s.Id == userID).FirstOrDefault();
            _context.AspNetUsers.Remove(user);
            _context.SaveChanges();
        }
    }
}
