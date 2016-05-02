using MovieDomain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDomain.Configurations
{
   internal class ShowtimeConfigurations : EntityTypeConfiguration<Showtime>
    {
		public ShowtimeConfigurations()
		{
			HasRequired(p => p.Cinema)
                .WithMany(c => c.Showtimes)
                .HasForeignKey(p => p.CinemaId);
            HasRequired(p => p.Movie)
                .WithMany(c => c.Showtimes)
                .HasForeignKey(p => p.MovieId);
		}
	}
}
