using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSVentas.Models.Requests
{
    public class SaleRequest
    {
        public decimal Total { get; set; }

        public int IdClient { get; set; }
        public List<ConceptRequest> Concepts { get; set; }

        public SaleRequest()
        {
            this.Concepts = new List<ConceptRequest>();
        }
    }

    public class ConceptRequest
    {
        public int Amount { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Imports { get; set; }
        public int IdProduct { get; set; }


    }
}
