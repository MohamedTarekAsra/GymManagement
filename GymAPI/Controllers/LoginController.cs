using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using GymAPI.BLL;
using GymAPI.Classes;
using GymAPI.DTOs;
using GymAPI.Enums;
using GymAPI.Models;

namespace GymAPI.Controllers
{
    public class LoginController : ApiController
    {
        
        [HttpPost]
        [Route("api/Login")]
        public IHttpActionResult Login([FromBody]LoginDto loginDto)
        {
            var Content = new APIContent<AuthonticationResponse>();
            try
            {
                var result = cUsers.Login(loginDto.UserName, loginDto.Password);
                // check user status
                // if user active generate token and return success
                if (result.status == (int)UserStatus.Active)
                {
                    Content.Status = (int)HttpStatusCode.OK;
                    Content.Message = ResultStatus.Success.ToString();
                    Content.response = result.response;
                }
                // invalid username or password
                if (result.status == (int)UserStatus.NotFound)
                {
                    Content.Status = (int)HttpStatusCode.Unauthorized;
                    Content.Message = ResultStatus.InvalidUsernameOrPassword.ToString();
                }
                // account deleted
                if (result.status == (int)UserStatus.Deleted)
                {
                    Content.Status = (int)HttpStatusCode.Unauthorized;
                    Content.Message = ResultStatus.Deleted.ToString();
                }
                // user is inactive
                if (result.status == (int)UserStatus.Inactive)
                {
                    Content.Status = (int)HttpStatusCode.Unauthorized;
                    Content.Message = ResultStatus.Inactive.ToString();
                }
                // exception
                if (result.status == (int)ResultStatus.Exception)
                {
                    Content.Status = (int)HttpStatusCode.InternalServerError;
                    Content.Message = ResultStatus.Exception.ToString();
                }
                return Ok(Content);
            }
            catch (Exception ex)
            {
                // if internal server error heppened
                Content.Status = (int)HttpStatusCode.InternalServerError;
                Content.Message = ex.Message;
                cGeneral.ExceptionLog(-1, ex);
            }
            return Ok(Content);
        }
    }
}