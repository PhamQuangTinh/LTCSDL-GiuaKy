using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LTCSDL.DAL.Models
{
    public partial class MyPhamContext : DbContext
    {
        public MyPhamContext()
        {
        }

        public MyPhamContext(DbContextOptions<MyPhamContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Dangnhap> Dangnhap { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<SanPham> SanPham { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=MyPham;Persist Security Info=True;User ID=sa;Password=sa;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dangnhap>(entity =>
            {
                entity.ToTable("dangnhap");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(20);

                entity.Property(e => e.Ho)
                    .HasColumnName("ho")
                    .HasMaxLength(20);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(20);

                entity.Property(e => e.Roleid).HasColumnName("roleid");

                entity.Property(e => e.Sdt)
                    .HasColumnName("SDT")
                    .HasMaxLength(20);

                entity.Property(e => e.Ten)
                    .HasColumnName("ten")
                    .HasMaxLength(20);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Dangnhap)
                    .HasForeignKey(d => d.Roleid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_dangnhap_role");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasColumnName("code")
                    .HasMaxLength(10);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<SanPham>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.ChucNang).HasMaxLength(20);

                entity.Property(e => e.Gia)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.MoTa).HasMaxLength(50);

                entity.Property(e => e.TenSanPham)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
