using System;
using System.Collections.Generic;

using System.Data.Entity;
using System.Data.Entity.Migrations;

using SampleApp.Entities;

namespace SampleApp.EF
{
    public class MigrationsConfiguration : DbMigrationsConfiguration<MainContext>
    {
        public MigrationsConfiguration()
        {
            AutomaticMigrationsEnabled = false;
        }
        
        protected override void Seed(MainContext db)
        {
            // pass
        }
    }
}