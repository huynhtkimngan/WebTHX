using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebTHX.Entities;

namespace WebTHX.Controllers
{
    public class XaController : Controller
    {
        private readonly TestDemoContext _context;

        public XaController(TestDemoContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var data = _context.Xas.ToList();
            ViewData["Huyen"] = _context.Huyens.ToList();
            ViewData["Tinh"] = _context.Tinhs.ToList();
            return View(data);
        }

        public IActionResult Create()
        {
            ViewData["Tinh"] = _context.Tinhs.ToList();

            // Lấy mã tỉnh từ query string (nếu có)
            string maTinh = Request.Query["MaT"];

            // Truy vấn danh sách các huyện thuộc tỉnh đã chọn
            if (!string.IsNullOrEmpty(maTinh))
            {
                int maTinhInt;
                if (int.TryParse(maTinh, out maTinhInt))
                {
                    ViewData["Huyen"] = _context.Huyens.Where(h => h.MaT == maTinhInt).ToList();
                }
            }
            else
            {
                // Không có tỉnh nào được chọn, hiển thị danh sách huyện trống
                ViewData["Huyen"] = new List<Huyen>();
            }

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Xa model)
        {
            if (ModelState.IsValid)
            {
                Xa newItem = new Xa()
                {
                    MaH = model.MaH,
                    MaX = model.MaX,
                    Ten = model.Ten,
                    Cap = model.Cap
                };
                _context.Xas.Add(newItem);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "THX");
            }
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var data = _context.Xas.FirstOrDefault(x => x.MaX == id);
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Edited(Xa model)
        {
            if (ModelState.IsValid)
            {
                var data = _context.Xas.FirstOrDefault(x => x.MaX == model.MaX);
                data.MaH = model.MaH;
                data.Ten = model.Ten;
                data.Cap = model.Cap;
                _context.Xas.Update(data);
                await _context.SaveChangesAsync();

                // Gửi kết quả trả về cho client JavaScript
                return Json(new { success = true });
            }
            return View(model);
        }


        public async Task<IActionResult> Delete(int id)
        {
            var data = _context.Xas.FirstOrDefault(x => x.MaX == id);
            if (data != null)
            {
                _context.Xas.Remove(data);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", "THX");
        }
    }
}