using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace UTC_TKW_BTL.Models
{
    public partial class QLBanThucPhamContext : DbContext
    {
        public QLBanThucPhamContext()
        {
        }

        public QLBanThucPhamContext(DbContextOptions<QLBanThucPhamContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TAnhChiTietSp> TAnhChiTietSps { get; set; } = null!;
        public virtual DbSet<TAnhSp> TAnhSps { get; set; } = null!;
        public virtual DbSet<TChiTietHdb> TChiTietHdbs { get; set; } = null!;
        public virtual DbSet<TChiTietSanPham> TChiTietSanPhams { get; set; } = null!;
        public virtual DbSet<TDanhMucSp> TDanhMucSps { get; set; } = null!;
        public virtual DbSet<THoaDonBan> THoaDonBans { get; set; } = null!;
        public virtual DbSet<TKhachHang> TKhachHangs { get; set; } = null!;
        public virtual DbSet<TLoaiSp> TLoaiSps { get; set; } = null!;
        public virtual DbSet<TNhanVien> TNhanViens { get; set; } = null!;
        public virtual DbSet<TQuocGium> TQuocGia { get; set; } = null!;
        public virtual DbSet<TUser> TUsers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-P9RRDLP\\SQLEXPRESS;Initial Catalog=QLBanThucPham;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TAnhChiTietSp>(entity =>
            {
                entity.HasKey(e => new { e.MaChiTietSp, e.TenFileAnh });

                entity.ToTable("tAnhChiTietSP");

                entity.Property(e => e.MaChiTietSp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("MaChiTietSP")
                    .IsFixedLength();

                entity.Property(e => e.TenFileAnh)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.MaChiTietSpNavigation)
                    .WithMany(p => p.TAnhChiTietSps)
                    .HasForeignKey(d => d.MaChiTietSp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tAnhChiTietSP_tChiTietSanPham");
            });

            modelBuilder.Entity<TAnhSp>(entity =>
            {
                entity.HasKey(e => new { e.MaSp, e.TenFileAnh });

                entity.ToTable("tAnhSP");

                entity.Property(e => e.MaSp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("MaSP")
                    .IsFixedLength();

                entity.Property(e => e.TenFileAnh)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.MaSpNavigation)
                    .WithMany(p => p.TAnhSps)
                    .HasForeignKey(d => d.MaSp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tAnhSP_tDanhMucSP");
            });

            modelBuilder.Entity<TChiTietHdb>(entity =>
            {
                entity.HasKey(e => new { e.MaHoaDon, e.MaChiTietSp });

                entity.ToTable("tChiTietHDB");

                entity.Property(e => e.MaHoaDon)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.MaChiTietSp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("MaChiTietSP")
                    .IsFixedLength();

                entity.Property(e => e.DonGiaBan).HasColumnType("money");

                entity.Property(e => e.GhiChu).HasMaxLength(100);

                entity.HasOne(d => d.MaChiTietSpNavigation)
                    .WithMany(p => p.TChiTietHdbs)
                    .HasForeignKey(d => d.MaChiTietSp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tChiTietHDB_tChiTietSanPham");

                entity.HasOne(d => d.MaHoaDonNavigation)
                    .WithMany(p => p.TChiTietHdbs)
                    .HasForeignKey(d => d.MaHoaDon)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tChiTietHDB_tHoaDonBan");
            });

            modelBuilder.Entity<TChiTietSanPham>(entity =>
            {
                entity.HasKey(e => e.MaChiTietSp);

                entity.ToTable("tChiTietSanPham");

                entity.Property(e => e.MaChiTietSp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("MaChiTietSP")
                    .IsFixedLength();

                entity.Property(e => e.AnhDaiDien)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.MaSp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("MaSP")
                    .IsFixedLength();

                entity.Property(e => e.Slton).HasColumnName("SLTon");

                entity.Property(e => e.Video)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.MaSpNavigation)
                    .WithMany(p => p.TChiTietSanPhams)
                    .HasForeignKey(d => d.MaSp)
                    .HasConstraintName("FK_tChiTietSanPham_tDanhMucSP");
            });

            modelBuilder.Entity<TDanhMucSp>(entity =>
            {
                entity.HasKey(e => e.MaSp);

                entity.ToTable("tDanhMucSP");

                entity.Property(e => e.MaSp)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("MaSP")
                    .IsFixedLength();

                entity.Property(e => e.AnhDaiDien)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.GiaLonNhat).HasColumnType("money");

                entity.Property(e => e.GiaNhoNhat).HasColumnType("money");

                entity.Property(e => e.GioiThieuSp)
                    .HasMaxLength(255)
                    .HasColumnName("GioiThieuSP");

                entity.Property(e => e.HanSuDung).HasColumnType("datetime");

                entity.Property(e => e.MaLoai)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.MaNuocSx)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("MaNuocSX")
                    .IsFixedLength();

                entity.Property(e => e.TenSp)
                    .HasMaxLength(150)
                    .HasColumnName("TenSP");

                entity.HasOne(d => d.MaLoaiNavigation)
                    .WithMany(p => p.TDanhMucSps)
                    .HasForeignKey(d => d.MaLoai)
                    .HasConstraintName("FK_tDanhMucSP_tLoaiSP");

                entity.HasOne(d => d.MaNuocSxNavigation)
                    .WithMany(p => p.TDanhMucSps)
                    .HasForeignKey(d => d.MaNuocSx)
                    .HasConstraintName("FK_tDanhMucSP_tQuocGia");
            });

            modelBuilder.Entity<THoaDonBan>(entity =>
            {
                entity.HasKey(e => e.MaHoaDon);

                entity.ToTable("tHoaDonBan");

                entity.Property(e => e.MaHoaDon)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.GhiChu).HasMaxLength(100);

                entity.Property(e => e.GiamGiaHd).HasColumnName("GiamGiaHD");

                entity.Property(e => e.MaKhachHang)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.MaNhanVien)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.MaSoThue)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.NgayHoaDon).HasColumnType("datetime");

                entity.Property(e => e.ThongTinThue).HasMaxLength(250);

                entity.Property(e => e.TongTienHd)
                    .HasColumnType("money")
                    .HasColumnName("TongTienHD");

                entity.HasOne(d => d.MaKhachHangNavigation)
                    .WithMany(p => p.THoaDonBans)
                    .HasForeignKey(d => d.MaKhachHang)
                    .HasConstraintName("FK_tHoaDonBan_tKhachHang");

                entity.HasOne(d => d.MaNhanVienNavigation)
                    .WithMany(p => p.THoaDonBans)
                    .HasForeignKey(d => d.MaNhanVien)
                    .HasConstraintName("FK_tHoaDonBan_tNhanVien");
            });

            modelBuilder.Entity<TKhachHang>(entity =>
            {
                entity.HasKey(e => e.MaKhanhHang);

                entity.ToTable("tKhachHang");

                entity.Property(e => e.MaKhanhHang)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.AnhDaiDien)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.DiaChi).HasMaxLength(150);

                entity.Property(e => e.GhiChu).HasMaxLength(100);

                entity.Property(e => e.NgaySinh).HasColumnType("date");

                entity.Property(e => e.SoDienThoai)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.TenKhachHang).HasMaxLength(100);

                entity.Property(e => e.Username)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("username")
                    .IsFixedLength();

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.TKhachHangs)
                    .HasForeignKey(d => d.Username)
                    .HasConstraintName("FK_tKhachHang_tUser");
            });

            modelBuilder.Entity<TLoaiSp>(entity =>
            {
                entity.HasKey(e => e.MaLoai);

                entity.ToTable("tLoaiSP");

                entity.Property(e => e.MaLoai)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Loai).HasMaxLength(100);
            });

            modelBuilder.Entity<TNhanVien>(entity =>
            {
                entity.HasKey(e => e.MaNhanVien);

                entity.ToTable("tNhanVien");

                entity.Property(e => e.MaNhanVien)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.AnhDaiDien)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.ChucVu).HasMaxLength(100);

                entity.Property(e => e.DiaChi).HasMaxLength(150);

                entity.Property(e => e.GhiChu).HasMaxLength(100);

                entity.Property(e => e.NgaySinh).HasColumnType("date");

                entity.Property(e => e.SoDienThoai1)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.SoDienThoai2)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.TenNhanVien).HasMaxLength(100);

                entity.Property(e => e.Username)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("username")
                    .IsFixedLength();

                entity.HasOne(d => d.UsernameNavigation)
                    .WithMany(p => p.TNhanViens)
                    .HasForeignKey(d => d.Username)
                    .HasConstraintName("FK_tNhanVien_tUser");
            });

            modelBuilder.Entity<TQuocGium>(entity =>
            {
                entity.HasKey(e => e.MaNuoc);

                entity.ToTable("tQuocGia");

                entity.Property(e => e.MaNuoc)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.TenNuoc).HasMaxLength(100);
            });

            modelBuilder.Entity<TUser>(entity =>
            {
                entity.HasKey(e => e.Username);

                entity.ToTable("tUser");

                entity.Property(e => e.Username)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("username")
                    .IsFixedLength();

                entity.Property(e => e.Password)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("password")
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
