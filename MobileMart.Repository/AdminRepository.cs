using MobileMart.DB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileMart.Repository
{
    public class AdminRepository:IAdminRepository
    {
        private MobileMartEntities _context;
        public Admin Get(string aspID)
        {
             _context = new MobileMartEntities();
            return _context.Admins.FirstOrDefault(s => s.AspNetID == aspID);
        }
        public void Edit(Admin entity)
        {
            _context = new MobileMartEntities();
            var admin = _context.Admins.FirstOrDefault(s => s.AspNetID == entity.AspNetID);
            admin.Name = entity.Name;
            admin.Mobile = entity.Mobile;
            admin.ProfilePhoto = entity.ProfilePhoto;
            _context.SaveChanges();
        }       
    }
}
