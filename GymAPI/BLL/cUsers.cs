using GymAPI.Classes;
using GymAPI.DTOs;
using GymAPI.Enums;
using GymAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;

namespace GymAPI.BLL
{
    public class cUsers
    {
        public static (int status, AuthonticationResponse response) Login(string userName, string password)
        {
            try
            {
                AuthonticationResponse response;
                using (var db = new GymDbContext())
                {
                    var decPassword = APICrypto.encrypt(password);
                    var userDetails = db.Users.FirstOrDefault(user => user.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase) 
                                                           && user.Password.Equals(decPassword));
                    if (userDetails != null)
                    {
                        if (userDetails.Status == (int)UserStatus.Active)
                        {
                            int tokenExpiration = int.Parse(ConfigurationManager.AppSettings["TokenExpiration"]);
                            int refreshTokenExpiration = int.Parse(ConfigurationManager.AppSettings["RefreshTokenExpiration"]);

                            response = TokenManager.GetAuthonticationResponse(userDetails.ID, userDetails.UserName, userDetails.UserType, userDetails.FirstName, userDetails.LastName,
                                                           userDetails.Email, userDetails.MobileNumber, userDetails.ProfileImage, userDetails.Status, userDetails.Gender,
                                                           userDetails.GymId, userDetails.Address, userDetails.TenantId, tokenExpiration);
                            return ((int)UserStatus.Active, response);// login success
                        }
                        return (userDetails.Status, null);// acount deleted or inactive
                    }
                    return ((int)UserStatus.NotFound, null);// username or password is invalid
                }
            }
            catch (Exception ex)
            {
                cGeneral.ExceptionLog(-1, ex);
                return ((int)ResultStatus.Exception, null);
            }
        }


    }
}