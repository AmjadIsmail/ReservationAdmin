using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Reservation_Admin.Models;

public partial class ReservationsContext : DbContext
{
    public ReservationsContext()
    {
    }

    public ReservationsContext(DbContextOptions<ReservationsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AvailibilityResult> AvailibilityResults { get; set; }

    public virtual DbSet<FlightMarkup> FlightMarkups { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost;Database=Reservations;Port=5432;User Id=postgres;Password=ds");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AvailibilityResult>(entity =>
        {
            entity.HasKey(e => e.ResultId);

            entity.ToTable("availibility_results");

            entity.Property(e => e.ResultId).HasColumnName("result_id");
            entity.Property(e => e.CreatedOn).HasColumnName("created_on");
            entity.Property(e => e.Request)
                .HasColumnType("jsonb")
                .HasColumnName("request");
            entity.Property(e => e.Response)
                .HasColumnType("jsonb")
                .HasColumnName("response");
            entity.Property(e => e.TotalResults).HasColumnName("total_results");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<FlightMarkup>(entity =>
        {
            entity.HasKey(e => e.MarkupId);

            entity.ToTable("flight_markup");

            entity.Property(e => e.MarkupId).HasColumnName("markup_id");
            entity.Property(e => e.AdultMarkup).HasColumnName("adult_markup");
            entity.Property(e => e.Airline).HasColumnName("airline");
            entity.Property(e => e.ApplyAirlineDiscount).HasColumnName("apply_airline_discount");
            entity.Property(e => e.ApplyMarkup).HasColumnName("apply_markup");
            entity.Property(e => e.ChildMarkup).HasColumnName("child_markup");
            entity.Property(e => e.CreatedOn).HasColumnName("created_on");
            entity.Property(e => e.DiscountOnAirline).HasColumnName("discount_on_airline");
            entity.Property(e => e.DiscountOnMeta).HasColumnName("discount_on_meta");
            entity.Property(e => e.InfantMarkup).HasColumnName("infant_markup");
            entity.Property(e => e.Meta).HasColumnName("meta");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
