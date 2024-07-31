using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using robot_controller_api.Models;

namespace robot_controller_api.Persistence;

public partial class RobotContext : DbContext
{
	public const string CONNECTION_STRING = "Host=localhost;Database=sit331;Username=postgres;Password=ben0009";

	public DbSet<RobotCommand> RobotCommands { get; set; }
	public DbSet<Map> Maps { get; set; }


	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		if (!optionsBuilder.IsConfigured)
		{
			optionsBuilder.UseNpgsql(CONNECTION_STRING).LogTo(Console.WriteLine)		
		.EnableSensitiveDataLogging();


			// .LogTo(Console.WriteLine, new[] {
			//DbLoggerCategory.Database.Name })


		}
}


	protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);


		modelBuilder.Entity<RobotCommand>(entity =>
		{
			entity.HasKey(x => x.Id);
			entity.ToTable("robotcommand");
			entity.Property(e => e.Id)
			.HasColumnName("id")
			.ValueGeneratedOnAdd();
			entity.Property(e => e.CreatedDate)
			.HasColumnType("timestamp without time zone")
			.HasColumnName("createddate");
			entity.Property(e => e.Description)
			.HasColumnType("character varying")
			.HasColumnName("description");
			entity.Property(e =>
		   e.IsMoveCommand).HasColumnName("ismovecommand");
			entity.Property(e => e.ModifiedDate)
			.HasColumnType("timestamp without time zone")
			.HasColumnName("modifieddate");
			entity.Property(e => e.Name)
			.HasColumnType("character varying")
			.HasColumnName("Name");
		});

		modelBuilder.Entity<Map>(entity =>
		{
			entity.HasKey(x => x.Id);
			entity.ToTable("map");
			entity.Property(e => e.Id)
				.HasColumnName("id")
				.ValueGeneratedOnAdd();
			entity.Property(e => e.CreatedDate)
				.HasColumnType("timestamp without time zone")
				.HasColumnName("createddate");
			entity.Property(e => e.Description)
				.HasColumnType("character varying")
				.HasColumnName("description");
			entity.Property(e => e.Rows).HasColumnName("rows");
			entity.Property(e => e.ModifiedDate)
				.HasColumnType("timestamp without time zone")
				.HasColumnName("modifieddate");
			entity.Property(e => e.Name)
				.HasColumnType("character varying")
				.HasColumnName("name");
			entity.Property(e => e.Columns).HasColumnName("columns");
		});

	}

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


}
