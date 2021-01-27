using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSVentas.Models.Responses
{
    public class Response
    {
        public int Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public Response()
        {
            this.Success = 0;  //constructor 
        }
    }
}
