using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MobileMart.DB.Model;

namespace MobileMart.Repository
{
    public class StateRepository : IStateRepository
    {
        private MobileMartEntities _context;
        public IEnumerable<state> Get()
        {
            _context = new MobileMartEntities();
            return _context.states.ToList();
        }

        public state GetStateByID(int? stateID)
        {
            _context = new MobileMartEntities();
            return _context.states.FirstOrDefault(s=> s.id == stateID);
        }
    }
}
