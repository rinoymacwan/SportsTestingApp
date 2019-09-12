using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DbTrials.Models;

namespace DbTrials.Models
{
    public class DbTrialsContext : DbContext
    {
        public DbTrialsContext (DbContextOptions<DbTrialsContext> options)
            : base(options)
        {
        }

        public DbSet<DbTrials.Models.User> User { get; set; }

        public DbSet<DbTrials.Models.Test> Test { get; set; }

        public DbSet<DbTrials.Models.UserTestMapping> UserTestMapping { get; set; }
    }
}
