using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaMedico.Models;
using SistemaMedico.cModels;
using Newtonsoft.Json;

namespace SistemaMedico.Controllers
{
    public class ProductosInventarioController : Controller
    {
        public Models.citas_medicasEntities1 db = new Models.citas_medicasEntities1();
        // GET: ProductosInventario
        public ActionResult Index()
        {
            List<cCategorias> listaCategorias = null;

            listaCategorias = (from a in db.Categoria
                                   select new cCategorias
                                   {
                                       Id = a.Id,
                                       Categoria1 = a.Categoria1
                                   }).ToList();


            List<SelectListItem> items = listaCategorias.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.Categoria1.ToString(),
                    Value = a.Id.ToString(),
                    Selected = false
                };
            });

            ViewBag.items = items;

            return View();
        }

        public string listar()
        {
            var query = (from p in db.ProductosInventario select p).OrderBy(p => p.Id).ToList<ProductosInventario>();
            List<cProductosInventario> listaProductosInventario = new List<cProductosInventario>();
            foreach (ProductosInventario productoInventario in query)
            {
                cProductosInventario ObjProductoInventario = new cProductosInventario();
                ObjProductoInventario.Id = productoInventario.Id;
                ObjProductoInventario.Codigo = productoInventario.Codigo;
                ObjProductoInventario.Imagen = productoInventario.Imagen;
                ObjProductoInventario.Nombre = productoInventario.Nombre;
                ObjProductoInventario.Id_Categoria = productoInventario.Id_Categoria;
                ObjProductoInventario.Estado = productoInventario.Estado;
                ObjProductoInventario.StockInicial = productoInventario.StockInicial;
                ObjProductoInventario.PrecioVenta = productoInventario.PrecioVenta;

                listaProductosInventario.Add(ObjProductoInventario);
            }
            return JsonConvert.SerializeObject(new
            {
                status = true,
                mensaje = "Datos cargados",
                data = listaProductosInventario
            });
        }
    }
}