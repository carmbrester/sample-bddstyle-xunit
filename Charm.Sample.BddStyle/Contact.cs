using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charm.Sample.BddStyle
{
    public class Contact
    {
        private int _id = 0;
        public int Id { get { return _id; } }
        public int _Id { set { _id = value; } }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
    }
}
