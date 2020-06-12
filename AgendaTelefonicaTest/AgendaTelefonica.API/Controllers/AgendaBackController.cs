using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AgendaTelefonica.Modelo;
using AgendaTelefonica.Repositorio;

namespace AgendaTelefonica.API.Controllers
{
    //[RoutePrefix("api/agenda")]
    public class AgendaBackController : ApiController
    {
        private ContactoContext db = new ContactoContext();

        //[Route, HttpGet]
        // GET: api/Agenda
        public IQueryable<Contacto> GetContactos()
        {
            return db.Contactos.Include(x => x.Telefonos);
        }

        //[Route("/{id}"), HttpGet]
        // GET: api/Agenda/5
        [ResponseType(typeof(Contacto))]
        public IHttpActionResult GetContacto(int id)
        {
            Contacto contacto = db.Contactos.Include(x=> x.Telefonos).FirstOrDefault(x=>x.IdContacto == id);
            if (contacto == null)
            {
                return NotFound();
            }

            return Ok(contacto);
        }

        // PUT: api/Agenda/5
        [ResponseType(typeof(void))]
        //[Route, HttpPut]

        public IHttpActionResult PutContacto(int id, Contacto contacto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contacto.IdContacto)
            {
                return BadRequest();
            }

            var telUpd = contacto.Telefonos.Where(t => t.IdTelefono > 0).ToList();
            var telNew = contacto.Telefonos.Where(t => t.IdTelefono == 0).ToList();

            try
            {
                db.Entry(contacto).State = EntityState.Modified;
                telUpd.ForEach(t =>
                {
                    db.Entry(t).State = EntityState.Modified;
                });
                telNew.ForEach(t =>
                {
                    t.Contacto = contacto;
                    db.Telefonos.Add(t);
                });
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch(Exception err)
            {

            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Agenda
        [ResponseType(typeof(Contacto))]
        //[Route, HttpPost]
        public IHttpActionResult PostContacto(Contacto contacto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Contactos.Add(contacto);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = contacto.IdContacto }, contacto);
        }

        // DELETE: api/Agenda/5
        [ResponseType(typeof(Contacto))]
        //[Route, HttpDelete]
        public IHttpActionResult DeleteContacto(int id)
        {
            Contacto contacto = db.Contactos.Find(id);
            if (contacto == null)
            {
                return NotFound();
            }

            db.Contactos.Remove(contacto);
            db.SaveChanges();

            return Ok(contacto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ContactoExists(int id)
        {
            return db.Contactos.Count(e => e.IdContacto == id) > 0;
        }
    }
}