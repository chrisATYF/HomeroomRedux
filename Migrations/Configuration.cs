namespace HomeroomRedux.Migrations
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HomeroomRedux.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(HomeroomRedux.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            if (!context.Roles.Any(r => r.Name == Constants.RoleInstructor))
            {
                context.Roles.Add(new IdentityRole(Constants.RoleInstructor));
            }
            if (!context.Roles.Any(r => r.Name == Constants.RoleStudent))
            {
                context.Roles.Add(new IdentityRole(Constants.RoleStudent));
            }

            context.SaveChanges();
        }
    }
}
