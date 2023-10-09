using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SachOnline.Models;

namespace SachOnline.Controllers
{
    public class SachOnlineController : Controller
    {
        private string connection;
        public dbSachOnlineDataContext data;
        private List<sach> LaySachMoi(int count) { 
            return data.saches.OrderByDescending(a => a.NgayCapNhap).Take(count).ToList();
        }

        public SachOnlineController()
        {
            // Khởi tạo chuỗi kết nối
            connection = "Data Source=LAPTOP-UN1O9AOH\\SQLEXPRESS;Initial Catalog=SachOnline;Integrated Security=True";
            data = new dbSachOnlineDataContext(connection);
        }

        // GET: SachOnline
        public ActionResult Index()
        {
            var listSachMoi = LaySachMoi(6);
            return View(listSachMoi);
        }

        public ActionResult ChuDePartial() {
            var listChuDe = from cd in data.chu_des select cd; 
            return PartialView(listChuDe); 
        }

        public ActionResult NXBPartial()
        {
            var listNXB = from cd in data.nha_xuat_bans select cd;
            return PartialView(listNXB);
        }

        public ActionResult SachTheoChuDe(int id)
        {
            var sach = from c in data.saches where c.chu_de_id==id select c;
            return PartialView(sach);
        }

        public ActionResult SachTheoNXB(int id)
        {
            var sach = from c in data.saches where c.nha_xuat_ban_id == id select c;
            return PartialView(sach);
        }

        public ActionResult ChiTietSach(int id)
        {
            var sach = from s in data.saches
                       where s.sach_id==id select s;
            return View(sach.Single());
        }
    }
}