using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hospital.SharedModel.Model;

namespace Hospital.SharedModel.Service
{
    public interface IJWTTokenGenerator
    {
        string GenerateToken(User user, IList<string> roles);
    }
}
