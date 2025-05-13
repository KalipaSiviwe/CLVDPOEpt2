using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace POEPartOne.Models;

public partial class Poept1Context : DbContext
{
    public Poept1Context()
    {
    }

    public Poept1Context(DbContextOptions<Poept1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Venue> Venues { get; set; }

    public virtual DbSet<BookingDetailsView> BookingDetailsViews { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BookingDetailsView>(entity =>
        {
            entity.HasNoKey();
            entity.ToView("vw_BookingDetails");
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Bookings__73951ACD9FF6FE77");

            entity.Property(e => e.BookingId).ValueGeneratedNever();

            entity.HasOne(d => d.Event).WithMany(p => p.Bookings).HasConstraintName("FK__Bookings__EventI__4E88ABD4");

            entity.HasOne(d => d.Venue).WithMany(p => p.Bookings).HasConstraintName("FK__Bookings__VenueI__5165187F");
        });

        // ... existing code ...
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}