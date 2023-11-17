using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BTLW.Models;

public partial class Lttqnhom6Context : DbContext
{
    public Lttqnhom6Context()
    {
    }

    public Lttqnhom6Context(DbContextOptions<Lttqnhom6Context> options)
        : base(options)
    {
    }

    public virtual DbSet<CaLam> CaLams { get; set; }

    public virtual DbSet<ChatLieu> ChatLieus { get; set; }

    public virtual DbSet<ChiTietHddh> ChiTietHddhs { get; set; }

    public virtual DbSet<ChiTietHdn> ChiTietHdns { get; set; }

    public virtual DbSet<CongViec> CongViecs { get; set; }

    public virtual DbSet<DmnoiThat> DmnoiThats { get; set; }

    public virtual DbSet<DonDatHang> DonDatHangs { get; set; }

    public virtual DbSet<HoaDonNhap> HoaDonNhaps { get; set; }

    public virtual DbSet<KhachHang> KhachHangs { get; set; }

    public virtual DbSet<KieuDang> KieuDangs { get; set; }

    public virtual DbSet<MauSac> MauSacs { get; set; }

    public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; }

    public virtual DbSet<NhanVien> NhanViens { get; set; }

    public virtual DbSet<NuocSx> NuocSxes { get; set; }

    public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }

    public virtual DbSet<TheLoai> TheLoais { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-PP0U79P7\\SQLEXPRESS;Initial Catalog=lttqnhom6;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CaLam>(entity =>
        {
            entity.HasKey(e => e.Maca).HasName("pk_CaLam");

            entity.ToTable("CaLam");

            entity.Property(e => e.Maca)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Tenca)
                .HasMaxLength(100)
                .HasColumnName("tenca");
        });

        modelBuilder.Entity<ChatLieu>(entity =>
        {
            entity.HasKey(e => e.Machatlieu).HasName("pk_ChatLieu");

            entity.ToTable("ChatLieu");

            entity.Property(e => e.Machatlieu)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Tenchatlieu)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("tenchatlieu");
        });

        modelBuilder.Entity<ChiTietHddh>(entity =>
        {
            entity.HasKey(e => new { e.MaNoithat, e.SoDdh }).HasName("PK__ChiTietH__F1EA73BA69B256F8");

            entity.ToTable("ChiTietHDDH");

            entity.Property(e => e.MaNoithat)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.SoDdh)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("SoDDH");

            entity.HasOne(d => d.MaNoithatNavigation).WithMany(p => p.ChiTietHddhs)
                .HasForeignKey(d => d.MaNoithat)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietHD__MaNoi__70DDC3D8");

            entity.HasOne(d => d.SoDdhNavigation).WithMany(p => p.ChiTietHddhs)
                .HasForeignKey(d => d.SoDdh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietHD__SoDDH__71D1E811");
        });

        modelBuilder.Entity<ChiTietHdn>(entity =>
        {
            entity.HasKey(e => new { e.MaNoithat, e.SoHdn }).HasName("PK__ChiTietH__D1B8F755014BD70E");

            entity.ToTable("ChiTietHDN");

            entity.Property(e => e.MaNoithat)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.SoHdn)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("SoHDN");

            entity.HasOne(d => d.MaNoithatNavigation).WithMany(p => p.ChiTietHdns)
                .HasForeignKey(d => d.MaNoithat)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietHD__MaNoi__6D0D32F4");

            entity.HasOne(d => d.SoHdnNavigation).WithMany(p => p.ChiTietHdns)
                .HasForeignKey(d => d.SoHdn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietHD__SoHDN__6E01572D");
        });

        modelBuilder.Entity<CongViec>(entity =>
        {
            entity.HasKey(e => e.MaCv).HasName("pk_CongViec");

            entity.ToTable("CongViec");

            entity.Property(e => e.MaCv)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaCV");
            entity.Property(e => e.TenCv)
                .HasMaxLength(100)
                .HasColumnName("tenCV");
        });

        modelBuilder.Entity<DmnoiThat>(entity =>
        {
            entity.HasKey(e => e.MaNoiThat).HasName("pk_DMNoiThat");

            entity.ToTable("DMNoiThat");

            entity.Property(e => e.MaNoiThat)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Anh)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Machatlieu)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Makieu)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Maloai)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Mamau)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Manuocsx)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.TenNoiThat)
                .HasMaxLength(50)
                .HasColumnName("tenNoiThat");
            entity.Property(e => e.ThoiGianBaoHanh).HasColumnType("date");

            entity.HasOne(d => d.MachatlieuNavigation).WithMany(p => p.DmnoiThats)
                .HasForeignKey(d => d.Machatlieu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Machatlieu_DMNoiThat");

            entity.HasOne(d => d.MakieuNavigation).WithMany(p => p.DmnoiThats)
                .HasForeignKey(d => d.Makieu)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Makieu_DMNoiThat");

            entity.HasOne(d => d.MaloaiNavigation).WithMany(p => p.DmnoiThats)
                .HasForeignKey(d => d.Maloai)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Maloai_DMNoiThat");

            entity.HasOne(d => d.MamauNavigation).WithMany(p => p.DmnoiThats)
                .HasForeignKey(d => d.Mamau)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Mamau_DMNoiThat");

            entity.HasOne(d => d.ManuocsxNavigation).WithMany(p => p.DmnoiThats)
                .HasForeignKey(d => d.Manuocsx)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Manuocsx_DMNoiThat");
        });

        modelBuilder.Entity<DonDatHang>(entity =>
        {
            entity.HasKey(e => e.SoDdh).HasName("pk_DonDatHang");

            entity.ToTable("DonDatHang");

            entity.Property(e => e.SoDdh)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("SoDDH");
            entity.Property(e => e.MaKhach)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.MaNv)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaNV");
            entity.Property(e => e.NgayDat).HasColumnType("date");
            entity.Property(e => e.NgayGiao).HasColumnType("date");

            entity.HasOne(d => d.MaKhachNavigation).WithMany(p => p.DonDatHangs)
                .HasForeignKey(d => d.MaKhach)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_MaKhach_HoaDonNhap");

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.DonDatHangs)
                .HasForeignKey(d => d.MaNv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_MaNV_DonDatHang");
        });

        modelBuilder.Entity<HoaDonNhap>(entity =>
        {
            entity.HasKey(e => e.SoHdn).HasName("pk_HoaDonNhap");

            entity.ToTable("HoaDonNhap");

            entity.Property(e => e.SoHdn)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("SoHDN");
            entity.Property(e => e.MaNcc)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaNCC");
            entity.Property(e => e.MaNv)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaNV");
            entity.Property(e => e.NgayNhap).HasColumnType("date");

            entity.HasOne(d => d.MaNccNavigation).WithMany(p => p.HoaDonNhaps)
                .HasForeignKey(d => d.MaNcc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_MaNCC_HoaDonNhap");

            entity.HasOne(d => d.MaNvNavigation).WithMany(p => p.HoaDonNhaps)
                .HasForeignKey(d => d.MaNv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_MaNV_HoaDonNhap");
        });

        modelBuilder.Entity<KhachHang>(entity =>
        {
            entity.HasKey(e => e.MaKhach).HasName("pk_KhachHang");

            entity.ToTable("KhachHang");

            entity.Property(e => e.MaKhach)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.DiaChi).HasMaxLength(100);
            entity.Property(e => e.DienThoai)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.TenKhach)
                .HasMaxLength(100)
                .HasColumnName("tenKhach");
        });

        modelBuilder.Entity<KieuDang>(entity =>
        {
            entity.HasKey(e => e.Makieu).HasName("pk_KieuDang");

            entity.ToTable("KieuDang");

            entity.Property(e => e.Makieu)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Tenkieu)
                .HasMaxLength(100)
                .HasColumnName("tenkieu");
        });

        modelBuilder.Entity<MauSac>(entity =>
        {
            entity.HasKey(e => e.Mamau).HasName("pk_MauSac");

            entity.ToTable("MauSac");

            entity.Property(e => e.Mamau)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Tenmau)
                .HasMaxLength(100)
                .HasColumnName("tenmau");
        });

        modelBuilder.Entity<NhaCungCap>(entity =>
        {
            entity.HasKey(e => e.MaNcc).HasName("pk_NhaCungCap");

            entity.ToTable("NhaCungCap");

            entity.Property(e => e.MaNcc)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaNCC");
            entity.Property(e => e.DiaChi).HasMaxLength(100);
            entity.Property(e => e.DienThoai)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.TenNcc)
                .HasMaxLength(100)
                .HasColumnName("tenNCC");
        });

        modelBuilder.Entity<NhanVien>(entity =>
        {
            entity.HasKey(e => e.MaNv).HasName("pk_NhanVien");

            entity.ToTable("NhanVien");

            entity.Property(e => e.MaNv)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaNV");
            entity.Property(e => e.DiaChi).HasMaxLength(50);
            entity.Property(e => e.DienThoai)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.GioiTinh)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.MaCv)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaCV");
            entity.Property(e => e.Maca)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.NgaySinh).HasColumnType("date");
            entity.Property(e => e.TenNv)
                .HasMaxLength(50)
                .HasColumnName("tenNV");

            entity.HasOne(d => d.MaCvNavigation).WithMany(p => p.NhanViens)
                .HasForeignKey(d => d.MaCv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_MaCV_NhanVien");

            entity.HasOne(d => d.MacaNavigation).WithMany(p => p.NhanViens)
                .HasForeignKey(d => d.Maca)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Maca_NhanVien");
        });

        modelBuilder.Entity<NuocSx>(entity =>
        {
            entity.HasKey(e => e.Manuocsx).HasName("pk_NuocSX");

            entity.ToTable("NuocSX");

            entity.Property(e => e.Manuocsx)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Tennuocsx)
                .HasMaxLength(100)
                .HasColumnName("tennuocsx");
        });

        modelBuilder.Entity<TaiKhoan>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("TaiKhoan");

            entity.Property(e => e.MaNv)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("MaNV");
            entity.Property(e => e.MaTk).HasColumnName("MaTK");
            entity.Property(e => e.MatKhau).HasMaxLength(50);
            entity.Property(e => e.TenTk)
                .HasMaxLength(50)
                .HasColumnName("TenTK");

            entity.HasOne(d => d.MaNvNavigation).WithMany()
                .HasForeignKey(d => d.MaNv)
                .HasConstraintName("FK_TaiKhoan_NhanVien");
        });

        modelBuilder.Entity<TheLoai>(entity =>
        {
            entity.HasKey(e => e.Maloai).HasName("pk_TheLoai");

            entity.ToTable("TheLoai");

            entity.Property(e => e.Maloai)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Tenloai)
                .HasMaxLength(100)
                .HasColumnName("tenloai");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
