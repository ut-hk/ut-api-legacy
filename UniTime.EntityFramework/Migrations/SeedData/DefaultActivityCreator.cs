using System;
using System.Data.Entity;
using System.Linq;
using Microsoft.AspNet.Identity;
using UniTime.Activities;
using UniTime.EntityFramework;
using UniTime.Locations;
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

        }
    }
}