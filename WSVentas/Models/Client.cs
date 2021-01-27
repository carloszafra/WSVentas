using System;
using System.Collections.Generic;

#nullable disable

namespace WSVentas.Models
{
    public partial class Client
    {
        public Client()
        {
            Sales = new HashSet<Sale>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Ci { get; set; }
        public string Number { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string City { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }
    }
}
