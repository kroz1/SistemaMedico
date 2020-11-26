using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaMedico.Models;
using SistemaMedico.cModels;
using Newtonsoft.Json;
using System.IO;

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

        public JsonResult guardar(cProductosInventario cproductosInventario)
        {
            ProductosInventario objProductosInventario = new ProductosInventario();
            if (cproductosInventario.Id != 0)
            {
                //editar
                objProductosInventario = db.ProductosInventario.Where(a => a.Id == cproductosInventario.Id).FirstOrDefault();
                if (objProductosInventario == null)
                {
                    return Json(new { status = false, mensaje = "No existe el registro" });
                }
                else
                {
                    objProductosInventario.Codigo = cproductosInventario.Codigo;
                    objProductosInventario.Presentacion = cproductosInventario.Presentacion;
                    objProductosInventario.Nombre = cproductosInventario.Nombre;
                    objProductosInventario.Descripcion = cproductosInventario.Descripcion;
                    objProductosInventario.Costo = cproductosInventario.Costo;
                    objProductosInventario.Utilidad = cproductosInventario.Utilidad;
                    objProductosInventario.PrecioVenta = cproductosInventario.PrecioVenta;
                    objProductosInventario.StockInicial = cproductosInventario.StockInicial;
                    objProductosInventario.Estado = cproductosInventario.Estado;
                    //objProductosInventario.Imagen = cproductosInventario.Imagen;
                    objProductosInventario.Id_Categoria = cproductosInventario.Id_Categoria;

                    db.ProductosInventario.Attach(objProductosInventario);
                    db.Entry(objProductosInventario).State = System.Data.Entity.EntityState.Modified;
                }
            }
            else
            {
                //nuevo
                objProductosInventario.Codigo = cproductosInventario.Codigo;
                objProductosInventario.Presentacion = cproductosInventario.Presentacion;
                objProductosInventario.Nombre = cproductosInventario.Nombre;
                objProductosInventario.Descripcion = cproductosInventario.Descripcion;
                objProductosInventario.Costo = cproductosInventario.Costo;
                objProductosInventario.Utilidad = cproductosInventario.Utilidad;
                objProductosInventario.PrecioVenta = cproductosInventario.PrecioVenta;
                objProductosInventario.StockInicial = cproductosInventario.StockInicial;
                objProductosInventario.Estado = cproductosInventario.Estado;
                //objProductosInventario.Imagen = cproductosInventario.Imagen;
                objProductosInventario.Id_Categoria = cproductosInventario.Id_Categoria;
                db.ProductosInventario.Add(objProductosInventario);
            }
            db.SaveChanges();
            return Json(new { status = true, mensaje = "Datos guardados", datos = objProductosInventario });
        }

        public JsonResult ImageUpload(cProductosInventario model) 
        {            
            int imgId = model.Id;
            var file = model.ImageFile;
            byte[] ImageByte = null;
            if (file != null)
            {
                file.SaveAs(Server.MapPath("/UploadImage/" + file.FileName));
                BinaryReader reader = new BinaryReader(file.InputStream);
                ImageByte = reader.ReadBytes(file.ContentLength);
                ProductosInventario img = new ProductosInventario();
                img = db.ProductosInventario.Where(a => a.Id == imgId).FirstOrDefault();
                img.Imagen = ImageByte;
                db.ProductosInventario.Attach(img);
                db.Entry(img).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                //imgId = img.Id;
            }
            //return Json(imgId, JsonRequestBehavior.AllowGet);
            return Json(new { status = true, mensaje = "Imagen guardada", JsonRequestBehavior.AllowGet});
        }

        public ActionResult DisplayingImage(int Id)
        {
            var img = db.ProductosInventario.Where(a => a.Id == Id).FirstOrDefault();
            return File(img.Imagen, "image/jpg");
        }


        public JsonResult editarImg(int Id)
        {
            ProductosInventario objProdInv = new ProductosInventario();
            if(Id == 0)
            {
                return Json(new { status = false, mensaje = "El id esta en 0" });
            }
            else
            {
                objProdInv = db.ProductosInventario.Where(a => a.Id == Id).FirstOrDefault();
                if (objProdInv == null)
                {
                    return Json(new { status = false, mensaje = "No existe el registro" });
                }

                return Json(new { status = true, mensaje = "Datos cargados", datos = objProdInv });
            }          
        }
    }
}