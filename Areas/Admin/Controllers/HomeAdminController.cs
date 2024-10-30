using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UTC_TKW_BTL.Models;
using X.PagedList;

namespace UTC_TKW_BTL.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("admin/homeadmin")]
    [Route("HomeAdmin")]
    public class HomeAdminController : Controller
    {
        QLBanThucPhamContext db = new QLBanThucPhamContext();
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("danhmucsanpham")]
        public IActionResult DanhMucSanPham(int? page)
        {
            int pageSize = 12;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstsanpham = db.TDanhMucSps.AsNoTracking().OrderBy(x => x.TenSp);
            PagedList<TDanhMucSp> lst = new PagedList<TDanhMucSp>(lstsanpham, pageNumber, pageSize);
            return View(lst);
        }
        [Route("ThemSanPhamMoi")]
        [HttpGet]
        public IActionResult ThemSanPhamMoi()
        {
            ViewBag.MaNuocSx = new SelectList(db.TQuocGia.ToList(), "MaNuoc", "TenNuoc");
            ViewBag.MaLoai = new SelectList(db.TLoaiSps.ToList(), "MaLoai", "Loai");
            return View();
        }
        [Route("ThemSanPhamMoi")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemSanPhamMoi(TDanhMucSp sanPham)
        {
            if(ModelState.IsValid)
            {
                db.TDanhMucSps.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("DanhMucSanPham", "HomeAdmin");
            }
            return View(sanPham);
        }
        [Route("SuaSanPham")]
        [HttpGet]
        public IActionResult SuaSanPham(string maSanPham)
        {
            ViewBag.MaNuocSx = new SelectList(db.TQuocGia.ToList(), "MaNuoc", "TenNuoc");
            ViewBag.MaLoai = new SelectList(db.TLoaiSps.ToList(), "MaLoai", "Loai");
            var sanPham = db.TDanhMucSps.Find(maSanPham);
            return View(sanPham);
        }
        [Route("SuaSanPham")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaSanPham(TDanhMucSp sanPham)
        {
            if (ModelState.IsValid)
            {
                db.Update(sanPham);
                db.SaveChanges();
                return RedirectToAction("DanhMucSanPham", "HomeAdmin");
            }
            return View(sanPham);
        }
        [Route("XoaSanPham")]
        [HttpGet]
        public IActionResult XoaSanPham(string maSanPham)
        {
            TempData["Message"] = "";
            var chiTietSanPham = db.TChiTietSanPhams.Where(x => x.MaSp == maSanPham).ToList();
            if (chiTietSanPham.Count() > 0) //ko xoa neu nhu san pham da co chi tiet
            {
                TempData["Message"] = "Không xóa được sản phẩm này";
                return RedirectToAction("DanhMucSanPham", "HomeAdmin");
            }
            var anhSanPhams = db.TAnhSps.Where(x => x.MaSp==maSanPham);
            if (anhSanPhams.Any()) db.RemoveRange(anhSanPhams);
            db.Remove(db.TDanhMucSps.Find(maSanPham));
            db.SaveChanges();
            TempData["Message"] = "Sản phẩm đã được xóa";
            return RedirectToAction("DanhMucSanPham", "HomeAdmin");

        }
        [Route("loaisanpham")]
        public IActionResult LoaiSanPham(int? page)
        {
            int pageSize = 12;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstloaisanpham = db.TLoaiSps.AsNoTracking().OrderBy(x => x.Loai);
            PagedList<TLoaiSp> lst = new PagedList<TLoaiSp>(lstloaisanpham, pageNumber, pageSize);
            return View(lst);
        }
        [Route("ThemLoaiSanPhamMoi")]
        [HttpGet]
        public IActionResult ThemLoaiSanPhamMoi()
        {
            return View();
        }
        [Route("ThemLoaiSanPhamMoi")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemLoaiSanPhamMoi(TLoaiSp loaiSanPham)
        {
            if (ModelState.IsValid)
            {
                db.TLoaiSps.Add(loaiSanPham);
                db.SaveChanges();
                return RedirectToAction("LoaiSanPham", "HomeAdmin");
            }
            return View(loaiSanPham);
        }
        [Route("SuaLoaiSanPham")]
        [HttpGet]
        public IActionResult SuaLoaiSanPham(string maLoai)
        {
            var loaiSanPham = db.TLoaiSps.Find(maLoai);
            return View(loaiSanPham);
        }
        [Route("SuaLoaiSanPham")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaLoaiSanPham(TLoaiSp loaiSanPham)
        {
            if (ModelState.IsValid)
            {
                db.Update(loaiSanPham);
                db.SaveChanges();
                return RedirectToAction("LoaiSanPham", "HomeAdmin");
            }
            return View(loaiSanPham);
        }
        [Route("XoaLoaiSanPham")]
        [HttpGet]
        public IActionResult XoaLoaiSanPham(string maLoai)
        {
            TempData["Message"] = "";
            var danhMucSanPham = db.TDanhMucSps.Where(x => x.MaLoai == maLoai).ToList();
            if (danhMucSanPham.Count() > 0) //ko xoa neu nhu da co danh muc san pham
            {
                TempData["Message"] = "Không xóa được loại sản phẩm này";
                return RedirectToAction("LoaiSanPham", "HomeAdmin");
            }
            db.Remove(db.TLoaiSps.Find(maLoai));
            db.SaveChanges();
            TempData["Message"] = "Loại sản phẩm đã được xóa";
            return RedirectToAction("LoaiSanPham", "HomeAdmin");

        }
    }
}
