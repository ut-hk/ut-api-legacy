using System;
using System.Data.Entity;
using System.Linq;
using Microsoft.AspNet.Identity;
using UniTime.Activities;
using UniTime.EntityFramework;
using UniTime.MultiTenancy;
using UniTime.Users;

namespace UniTime.Migrations.SeedData
{
    public class DefaultActivityCreator
    {
        private readonly UniTimeDbContext _context;

        public DefaultActivityCreator(UniTimeDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateActivity();
        }

        private void CreateActivity()
        {
            var user = _context.Users.FirstOrDefault(u => u.Name == "Leo");
            var activity = Activity.Create("Happy Activity", "Description", new DateTime(2017, 3, 13, 13, 00, 10), new DateTime(2017, 3, 13, 13, 00, 10), user);

            _context.AbstractActivities.Add(activity);
            _context.SaveChanges();
        }
    }
}