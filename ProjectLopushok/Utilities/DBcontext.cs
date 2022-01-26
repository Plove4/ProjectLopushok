using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectLopushok.Entities;

namespace ProjectLopushok.Utilities
{
    internal class DBcontext
    {
        private static LopushokEntities _context { get; set; }

        public static LopushokEntities Context
        {
            get
            {
                if (_context == null)
                    _context = new LopushokEntities();
                return _context;
            }
        }
    }
}
