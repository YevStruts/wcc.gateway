﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using wcc.gateway.Identity;
using wcc.gateway.Infrastructure;

namespace wcc.gateway.data
{
    public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        protected readonly IConfiguration Configuration;

        public ApplicationDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server with connection string from app settings
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<News> News { get; set; }
    }
}