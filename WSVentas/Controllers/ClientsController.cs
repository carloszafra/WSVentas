using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSVentas.Models;
using WSVentas.Models.Responses;
using WSVentas.Models.Requests;
using Microsoft.AspNetCore.Authorization;

namespace WSVentas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClientsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            Response oResponse = new Response();
            try
            {
                
                using (Sales_DBContext db = new Sales_DBContext())
                {
                    var list = db.Clients.OrderBy(d => d.Id).ToList();
                    oResponse.Data = list;
                    oResponse.Success = 1;
                    
                }
            }
            catch (Exception ex )
            {
                oResponse.Message = ex.Message;
            }

            return Ok(oResponse);
        }

        [HttpPost]
        public IActionResult Add(ClientRequest req)
        {
            Response oResponse = new Response();
            try
            {

                using (Sales_DBContext db = new Sales_DBContext())
                {
                    Client oClient = new Client();
                    oClient.Name = req.Name;
                    oClient.Ci = req.Ci;
                    oClient.Number = req.Number;
                    oClient.Address = req.Address;
                    oClient.Email = req.Email;
                    oClient.City = req.City;
                    db.Clients.Add(oClient);
                    db.SaveChanges();
                    oResponse.Success = 1;
                }
            }
            catch(Exception ex)
            {
                oResponse.Message = ex.Message;
            }

            return Ok(oResponse);
        }

        [HttpPut]
        public IActionResult Update(ClientRequest req)
        {
            Response oResponse = new Response();
            try
            {
                using(Sales_DBContext db = new Sales_DBContext())
                {
                    Client oClient = db.Clients.Find(req.Id);
                    oClient.Name = req.Name;
                    oClient.Ci = req.Ci;
                    oClient.Number = req.Number;
                    oClient.Address = req.Address;
                    oClient.Email = req.Email;
                    oClient.City = req.City;
                    db.Entry(oClient).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();
                    oResponse.Success = 1;
                }
            }
            catch (Exception ex)
            {
                oResponse.Message = ex.Message;
            }

            return Ok(oResponse);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Response oResponse = new Response();
            try
            {
               using(Sales_DBContext db = new Sales_DBContext())
                {
                    Client oClient = db.Clients.Find(id);
                    db.Remove(oClient);
                    db.SaveChanges();
                    oResponse.Success = 1;
                }
            }
            catch (Exception ex)
            {
                oResponse.Message = ex.Message;
            }

            return Ok(oResponse);
        }
    }
}
