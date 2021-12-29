namespace GymAPI.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GymAPI.Models.GymDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GymAPI.Models.GymDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //context.Users.AddOrUpdate(x => x.ID, new Models.User()
            //{
            //    FirstName = "mohamed",
            //    LastName = "kamel",
            //    UserName = "mohamedkamel",
            //    Password = "pO1QW28G4Mg46uvpKfsAww==",
            //    ChangePassowrdOnLogin = false,
            //    Email = "mohamed@gmail.com",
            //    MobileNumber = "+201092564672",
            //    Status = 1,
            //    CreatedDate = DateTime.Now
            //});
        }
    }
}
