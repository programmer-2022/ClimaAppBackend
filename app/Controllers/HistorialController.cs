using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using app.Models;
using app.Models.Request;
using Microsoft.AspNetCore.Cors;

namespace app.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("permitir")]
    public class HistorialController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {
            //Conexion a ET
            using(climadbContext db = new climadbContext()) 
            {
                var historial = db.Historials.ToList();
                return Ok(historial);  
            }
        }

        [HttpPost]

        public IActionResult Add(HistorialRequest model) 
        {
            using(climadbContext db = new climadbContext())
            {
                Historial historial = new Historial();

                //Setear campos del modelo a Entity
                historial.City = model.City;
                historial.Info = model.Info;

                //Guardar los cambios en la BD
                db.Historials.Add(historial);
                db.SaveChanges();
                return Ok("Se guardó exitosamente");
            }
        }

        [HttpPut]

        public IActionResult Update(HistorialRequest model)
        {
            using(climadbContext db = new climadbContext())
            {
                Historial his = db.Historials.Find(model.Id);

                //Modificando los campos
                his.City = model.City;
                his.Info = model.Info;
                
                //Modifico la tabla
                db.Entry(his).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                return Ok("Datos modificados");
            }
        }   
    }
}
