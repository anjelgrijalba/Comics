using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Comics.Models
{
    public class Comic
    {
        public int Id { get; set; }
        [Required]
        public string Titulo { get; set; }
        public DateTime Fecha { get; set; }
        public string  Autor { get; set; }
        public decimal Precio { get; set; }
        public decimal Calificacion { get; set; }

        // Foreign Key
        public int EditorialId { get; set; }
        // Navigation property
        //public Editoriales Editorial { get; set; }
        public virtual Editoriales Editorial { get; set; }

    }
}