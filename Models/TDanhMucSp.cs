using System;
using System.Collections.Generic;

namespace UTC_TKW_BTL.Models
{
    public partial class TDanhMucSp
    {
        public TDanhMucSp()
        {
            TAnhSps = new HashSet<TAnhSp>();
            TChiTietSanPhams = new HashSet<TChiTietSanPham>();
        }

        public string MaSp { get; set; } = null!;
        public string? TenSp { get; set; }
        public double? CanNang { get; set; }
        public string? MaNuocSx { get; set; }
        public DateTime? HanSuDung { get; set; }
        public string? GioiThieuSp { get; set; }
        public double? ChietKhau { get; set; }
        public string? MaLoai { get; set; }
        public string? AnhDaiDien { get; set; }
        public decimal? GiaNhoNhat { get; set; }
        public decimal? GiaLonNhat { get; set; }

        public virtual TLoaiSp? MaLoaiNavigation { get; set; }
        public virtual TQuocGium? MaNuocSxNavigation { get; set; }
        public virtual ICollection<TAnhSp> TAnhSps { get; set; }
        public virtual ICollection<TChiTietSanPham> TChiTietSanPhams { get; set; }
    }
}
