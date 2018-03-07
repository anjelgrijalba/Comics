using System.ComponentModel.DataAnnotations;

namespace Comics.Models
{
    public class Editoriales
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
    }
}

