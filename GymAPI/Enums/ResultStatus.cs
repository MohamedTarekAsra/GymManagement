using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GymAPI.Enums
{
    public enum ResultStatus
    {
        Success = 1,
        Failed = -1,
        Exception = -2,
        InvalidUsernameOrPassword = -3,
        //
        InternalServerError = 500,
        Unauthorized = 401
    }
}