using System;
using System.Collections.Generic;
using System.Text;
using Bitfit.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bitfit.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Schedule> Schedules { get; set; }
		public DbSet<Workout> Workouts { get; set; }
	}
}