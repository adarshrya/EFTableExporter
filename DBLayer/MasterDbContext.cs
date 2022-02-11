using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EFTableExporter
{
    public partial class MasterDbContext : DbContext
    {
        public MasterDbContext()
        {
            this.Database.SetCommandTimeout(300);
        }

        public MasterDbContext(DbContextOptions<MasterDbContext> options)
            : base(options)
        {
            
        }

        public virtual DbSet<DataTable1> DataTable1s { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            { 
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=MasterDb;Integrated Security=true;");
                
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DataTable1>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("DataTable1");

                entity.Property(e => e.DataCol1).HasMaxLength(100);

                entity.Property(e => e.DataCol10).HasMaxLength(100);

                entity.Property(e => e.DataCol11).HasMaxLength(100);

                entity.Property(e => e.DataCol12).HasMaxLength(100);

                entity.Property(e => e.DataCol13).HasMaxLength(100);

                entity.Property(e => e.DataCol14).HasMaxLength(100);

                entity.Property(e => e.DataCol15).HasMaxLength(100);

                entity.Property(e => e.DataCol16).HasMaxLength(100);

                entity.Property(e => e.DataCol17).HasMaxLength(100);

                entity.Property(e => e.DataCol18).HasMaxLength(100);

                entity.Property(e => e.DataCol19).HasMaxLength(100);

                entity.Property(e => e.DataCol2).HasMaxLength(100);

                entity.Property(e => e.DataCol20).HasMaxLength(100);

                entity.Property(e => e.DataCol21).HasMaxLength(100);

                entity.Property(e => e.DataCol22).HasMaxLength(100);

                entity.Property(e => e.DataCol23).HasMaxLength(100);

                entity.Property(e => e.DataCol24).HasMaxLength(100);

                entity.Property(e => e.DataCol25).HasMaxLength(100);

                entity.Property(e => e.DataCol26).HasMaxLength(100);

                entity.Property(e => e.DataCol27).HasMaxLength(100);

                entity.Property(e => e.DataCol28).HasMaxLength(100);

                entity.Property(e => e.DataCol29).HasMaxLength(100);

                entity.Property(e => e.DataCol3).HasMaxLength(100);

                entity.Property(e => e.DataCol30).HasMaxLength(100);

                entity.Property(e => e.DataCol31).HasMaxLength(100);

                entity.Property(e => e.DataCol32).HasMaxLength(100);

                entity.Property(e => e.DataCol33).HasMaxLength(100);

                entity.Property(e => e.DataCol34).HasMaxLength(100);

                entity.Property(e => e.DataCol35).HasMaxLength(100);

                entity.Property(e => e.DataCol36).HasMaxLength(100);

                entity.Property(e => e.DataCol37).HasMaxLength(100);

                entity.Property(e => e.DataCol38).HasMaxLength(100);

                entity.Property(e => e.DataCol39).HasMaxLength(100);

                entity.Property(e => e.DataCol4).HasMaxLength(100);

                entity.Property(e => e.DataCol40).HasMaxLength(100);

                entity.Property(e => e.DataCol41).HasMaxLength(100);

                entity.Property(e => e.DataCol42).HasMaxLength(100);

                entity.Property(e => e.DataCol43).HasMaxLength(100);

                entity.Property(e => e.DataCol44).HasMaxLength(100);

                entity.Property(e => e.DataCol45).HasMaxLength(100);

                entity.Property(e => e.DataCol46).HasMaxLength(100);

                entity.Property(e => e.DataCol47).HasMaxLength(100);

                entity.Property(e => e.DataCol48).HasMaxLength(100);

                entity.Property(e => e.DataCol49).HasMaxLength(100);

                entity.Property(e => e.DataCol5).HasMaxLength(100);

                entity.Property(e => e.DataCol50).HasMaxLength(100);

                entity.Property(e => e.DataCol6).HasMaxLength(100);

                entity.Property(e => e.DataCol7).HasMaxLength(100);

                entity.Property(e => e.DataCol8).HasMaxLength(100);

                entity.Property(e => e.DataCol9).HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
