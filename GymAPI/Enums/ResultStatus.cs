using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GymAPI.Enums
{
    public enum ResultStatus
    {
        Success = 200,
        Unauthorized = 401,
        //
        Inactive = 0,
        Deleted = -1,
        InvalidUsernameOrPassword = -2,
        Exception = -999

    }
}