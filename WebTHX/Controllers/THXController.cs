using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using WebTHX.Entities;


namespace THXControllers.Controllers
{
    public class THXController : Controller
    {
        private readonly ILogger<THXController> _logger;
        private readonly TestDemoContext _context;


        public THXController(ILogger<THXController> logger, TestDemoContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index()
        {
            var tinhList = _context.Tinhs.ToList();
            ViewData["Tinh"] = tinhList;
            return View();
        }



        [HttpPost]
        public IActionResult GetHuyensByTinhId(int tinhId)
        {
            var huyens = _context.Huyens.Where(h => h.MaT == tinhId).ToList();
            return Json(huyens);
        }

        [HttpGet]
        public IActionResult GetTinhs()
        {
            var tinhs = _context.Tinhs.ToList();
            return Ok(tinhs);
        }

        [HttpGet]
        public IActionResult GetHuyens(int maT)
        {
            var huyens = _context.Huyens.Where(h => h.MaT == maT).ToList();
            return Ok(huyens);
        }

        [HttpGet]
        public IActionResult GetXas(int maH)
        {
            var xas = _context.Xas.Where(x => x.MaH == maH).ToList();
            return Ok(xas);
        }
    }
}