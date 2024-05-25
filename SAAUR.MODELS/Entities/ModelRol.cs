using System.ComponentModel.DataAnnotations;

namespace SAAUR.MODELS.Entities
{
    public class ModelRol
    {
        public int id { get; set; }
        [Required]
        public string rol { get; set; }
    }
}