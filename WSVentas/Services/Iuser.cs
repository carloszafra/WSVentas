using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSVentas.Models.Responses;
using WSVentas.Models.Requests;

namespace WSVentas.Services
{
    public interface Iuser
    {
        UserResponse Auth(AuthRequest model);
    }
}
