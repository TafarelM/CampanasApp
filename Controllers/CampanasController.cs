using Microsoft.AspNetCore.Mvc;
using CampanasApp.Models;
using System.Linq;
using System.Collections.Generic;

namespace CampanasApp.Controllers
{
    public class CampanasController : Controller
    {
        // 🔹 LISTADO + FILTRO
        public IActionResult Index(string categoria, string estado)
        {
            List<Campana> lista = CampanaData.Lista;

            // Filtro por categoría
            if (!string.IsNullOrEmpty(categoria))
            {
                lista = lista.Where(c => c.Categoria == categoria).ToList();
            }

            // Filtro por estado
            if (!string.IsNullOrEmpty(estado))
            {
                lista = lista.Where(c => c.Estado == estado).ToList();
            }

            return View(lista);
        }

        // 🔹 DETALLE
        public IActionResult Detalle(int id)
        {
            var campana = CampanaData.Lista.FirstOrDefault(c => c.Id == id);

            if (campana == null)
            {
                return NotFound();
            }

            return View(campana);
        }

        // 🔹 RESUMEN (DASHBOARD)
        public IActionResult Resumen()
        {
            var lista = CampanaData.Lista;

            var total = lista.Count;
            var vigentes = lista.Count(c => c.Estado == "Vigente");
            var proximas = lista.Count(c => c.Estado == "Próxima");
            var promedio = lista.Average(c => c.DescuentoPct);

            var web = lista.Count(c => c.Canal == "Web");
            var app = lista.Count(c => c.Canal == "App");
            var tienda = lista.Count(c => c.Canal == "Tienda");

            ViewBag.Total = total;
            ViewBag.Vigentes = vigentes;
            ViewBag.Proximas = proximas;
            ViewBag.Promedio = promedio;
            ViewBag.Web = web;
            ViewBag.App = app;
            ViewBag.Tienda = tienda;

            return View();
        }
    }
}