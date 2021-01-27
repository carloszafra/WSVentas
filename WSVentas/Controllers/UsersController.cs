using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSVentas.Models.Requests;
using WSVentas.Services;
using WSVentas.Models.Responses;

namespace WSVentas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private Iuser _userService;

        public UsersController(Iuser userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public IActionResult Auth([FromBody] AuthRequest req)
        {
            Response response = new Response();
            var userResponse = _userService.Auth(req);

            if(userResponse == null)
            {
                response.Success = 0;
                response.Message = "User or password incorrects";
                return BadRequest(response);
            }

            response.Success = 1;
            response.Data = userResponse;
                
            return Ok(response);
        }
    }
}
