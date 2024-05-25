using System.ComponentModel.DataAnnotations;

namespace SAAUR.MODELS.Entities
{
    public class ModelUserEditPassword
    {
        public int user_id {get;set;}
        [Required]
        public string password {get;set;}
        public string hashPass { get; set; }
        public string salt { get; set; }
    }
}