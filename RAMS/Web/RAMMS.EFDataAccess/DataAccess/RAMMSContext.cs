using Microsoft.EntityFrameworkCore;
using RAMMS.EFDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.EFDataAccess.DataAccess
{
    public class RAMMSContext : DbContext
    {
        public RAMMSContext(DbContextOptions options) : base(options) { }

        //entities
        public DbSet<User> Users { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<UserScope> UserScopes { get; set; }



    }
}
