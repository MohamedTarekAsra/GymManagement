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
        public static (int status, AuthonticationResponse token) Login(string userName, string password)
        {
            try
            {
                AuthonticationResponse newToken;
                using (var db = new GymDbContext())
                {
                    //check if username is exists
                    bool isUserValid = db.Users.Any(user => user.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase) && user.Status != (int)UserStatus.Deleted);
                    if (isUserValid)
                    {
                        var decPassword = APICrypto.encrypt(password);
                        var userDetails = db.Users.FirstOrDefault(user => user.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase) && user.Password.Equals(decPassword) && user.Status != (int)UserStatus.Deleted);
                        if (userDetails != null)
                        {
                            // check if user is active
                            if (userDetails.Status == (int)UserStatus.Active)
                            {
                                int tokenExpiration = int.Parse(ConfigurationManager.AppSettings["TokenExpiration"]);
                                int refreshTokenExpiration = int.Parse(ConfigurationManager.AppSettings["RefreshTokenExpiration"]);

                                newToken = TokenManager.GenerateToken(userDetails.ID, userDetails.UserName, userDetails.UserType, userDetails.FirstName, userDetails.LastName, 
                                                               userDetails.Email, userDetails.MobileNumber, userDetails.ProfileImage, userDetails.Status, userDetails.Gender,
                                                               userDetails.GymId, userDetails.Address, userDetails.TenantId, tokenExpiration);

                               
                                return ((int)ResultStatus.Success, newToken);
                            }
                        }

                        int attempts = int.Parse(ConfigurationManager.AppSettings["MaxFailedLoginAttempts"]);
                        int blockMinutes = int.Parse(ConfigurationManager.AppSettings["BlockMinutes"]);
                        if (userDetails.LastLoginFailedDate.HasValue && DateTime.Now >= userDetails.LastLoginFailedDate.Value.AddMinutes(blockMinutes))
                        {
                        }

                        return ((int)ResultStatus.InvalidUsernameOrPassword, null);
                    }
                    return ((int)ResultStatus.InvalidUsernameOrPassword, null);
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