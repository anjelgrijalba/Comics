using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Comics.Models;

namespace Comics.Controllers
{
    public class EditorialesController : ApiController
    {
        private ComicsContext db = new ComicsContext();

        // GET: api/Editoriales
        public IQueryable<Editoriales> GetEditoriales()
        {
            return db.Editoriales;
        }

        // GET: api/Editoriales/5
        [ResponseType(typeof(Editoriales))]
        public async Task<IHttpActionResult> GetEditoriales(int id)
        {
            Editoriales editoriales = await db.Editoriales.FindAsync(id);
            if (editoriales == null)
            {
                return NotFound();
            }

            return Ok(editoriales);
        }

        // PUT: api/Editoriales/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEditoriales(int id, Editoriales editoriales)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != editoriales.Id)
            {
                return BadRequest();
            }

            db.Entry(editoriales).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EditorialesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Editoriales
        [ResponseType(typeof(Editoriales))]
        public async Task<IHttpActionResult> PostEditoriales(Editoriales editoriales)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Editoriales.Add(editoriales);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = editoriales.Id }, editoriales);
        }

        // DELETE: api/Editoriales/5
        [ResponseType(typeof(Editoriales))]
        public async Task<IHttpActionResult> DeleteEditoriales(int id)
        {
            Editoriales editoriales = await db.Editoriales.FindAsync(id);
            if (editoriales == null)
            {
                return NotFound();
            }

            db.Editoriales.Remove(editoriales);
            await db.SaveChangesAsync();

            return Ok(editoriales);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EditorialesExists(int id)
        {
            return db.Editoriales.Count(e => e.Id == id) > 0;
        }
    }
}