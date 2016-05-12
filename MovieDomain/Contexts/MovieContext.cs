using MovieDomain.Configurations;
using MovieDomain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDomain.Contexts
{
    public class MovieContext: DbContext
    {
        static MovieContext()
        {
            Database.SetInitializer(new MovieBaseInitializer());
        }

        public MovieContext()
            : base("name=MovieBase")
        { 
		}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ShowtimeConfigurations());
            ConfigureIdentityTables(modelBuilder);
        }

        private void ConfigureIdentityTables(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CustomRoleConfigurations());
            modelBuilder.Configurations.Add(new CustomUserClaimsConfigurations());
            modelBuilder.Configurations.Add(new CustomUserLoginsConfigurations());
            modelBuilder.Configurations.Add(new CustomUserRolesConfigurations());
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Showtime> Showtimes { get; set; }
        public virtual DbSet<Cinema> Cinemas { get; set; }
        public virtual DbSet<Rate> Likes { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }

    }
}
