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
    public class ComicsController : ApiController
    {
        private ComicsContext db = new ComicsContext();

        // GET: api/Comics
        public IQueryable<Comic> GetComics()
        {
            return db.Comics;
        }

        // GET: api/Comics/5
        [ResponseType(typeof(Comic))]
        public async Task<IHttpActionResult> GetComic(int id)
        {
            Comic comic = await db.Comics.FindAsync(id);
            if (comic == null)
            {
                return NotFound();
            }

            return Ok(comic);
        }

        // PUT: api/Comics/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutComic(int id, Comic comic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != comic.Id)
            {
                return BadRequest();
            }

            db.Entry(comic).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComicExists(id))
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

        // POST: api/Comics
        [ResponseType(typeof(Comic))]
        public async Task<IHttpActionResult> PostComic(Comic comic)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Comics.Add(comic);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = comic.Id }, comic);
        }

        // DELETE: api/Comics/5
        [ResponseType(typeof(Comic))]
        public async Task<IHttpActionResult> DeleteComic(int id)
        {
            Comic comic = await db.Comics.FindAsync(id);
            if (comic == null)
            {
                return NotFound();
            }

            db.Comics.Remove(comic);
            await db.SaveChangesAsync();

            return Ok(comic);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ComicExists(int id)
        {
            return db.Comics.Count(e => e.Id == id) > 0;
        }
    }
}