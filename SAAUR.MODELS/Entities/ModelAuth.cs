using System.ComponentModel.DataAnnotations;

namespace SAAUR.MODELS.Entities
{
    public class ModelAuth
    {
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        public string pwd { get; set; }
    }
}