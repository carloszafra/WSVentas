using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSVentas.Models.Requests
{
    public class ClientRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Ci { get; set; }
        public string Number { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string City { get; set; }

    }
}
