using GymAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GymAPI.BLL
{
    public class cGeneral
    {

        public static void ExceptionLog(int CurrentUser, Exception exe)
        {
            try
            {

                using (var db = new GymDbContext())
                {
                    var routeValues = HttpContext.Current.Request.RequestContext.RouteData.Values;
                    string action = routeValues["action"].ToString();
                    string controller = routeValues["controller"].ToString();
                    var newLog = new ExceptionLog()
                    {
                        Action = action,
                        Controller = controller,
                        CreatedBy = CurrentUser,
                        CreationDate = DateTime.Now,
                        Message = exe.Message,
                        StackTrace = exe.StackTrace
                    };
                    db.ExceptionLogs.Add(newLog);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}