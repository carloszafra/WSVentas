using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSVentas.Models;
using WSVentas.Models.Requests;
using WSVentas.Models.Responses;

namespace WSVentas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SalesController : ControllerBase
    {
        public IActionResult Add(SaleRequest req)
        {
            Response response = new Response();

            try
            {
                using(Sales_DBContext db = new Sales_DBContext())
                {
                    using(var transaction = db.Database.BeginTransaction())
                    {

                        try
                        {
                            Sale sale = new Sale();
                            sale.Total = req.Concepts.Sum(d => d.Amount * d.UnitPrice);
                            sale.Date = DateTime.Now;
                            sale.IdCliente = req.IdClient;
                            db.Add(sale);
                            db.SaveChanges();

                            foreach (var concept in req.Concepts)
                            {
                                Concept concept1 = new Concept
                                {
                                    Amount = concept.Amount,
                                    IdProduct = concept.IdProduct,
                                    UnitPrice = concept.UnitPrice,
                                    Imports = concept.Imports,
                                    IdSale = sale.Id
                                };
                                db.Add(concept1);
                                db.SaveChanges();
                            }

                            transaction.Commit();
                            response.Success = 1;

                        }
                        catch(Exception ex)
                        {
                            transaction.Rollback();
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                response.Message = ex.Message;
            }

            return Ok(response);
        }
    }
}
