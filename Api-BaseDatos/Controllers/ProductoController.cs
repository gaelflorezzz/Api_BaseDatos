using Api_BaseDatos.Models;
using Api_BaseDatos.Models.DB;
using Api_BaseDatos.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api_BaseDatos.Controllers
{
    public class ProductoController : Controller
    {
        private readonly ProductosService _service;
        public ProductoController(ProductosService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var lista = await _service.ObtenerTodos();
            return View("~/Views/Productos/Index.cshtml", lista);
        }

        public IActionResult Crear()
        {
            return View(); 
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Crear(Producto p)
        {
            if (!ModelState.IsValid)  
                return View(p);

            await _service.Insertar(p);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Eliminar(long id)
        {
            await _service.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> GetById(long id)
        {
            var producto = await _service.ObtenerPorId(id);

            if(producto == null)
                return NotFound();

            return View(producto);
        }

        [HttpGet]
        public IActionResult Editar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(long id, ProductoDb p)
        {
            if (id != p.Pro_Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(p);

            var resultado = await _service.Actualizar(p);

            if(resultado)
                return RedirectToAction(nameof(Index));

            return View(p);
        }
    }
}
