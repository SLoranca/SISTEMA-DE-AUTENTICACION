using System.ComponentModel.DataAnnotations;

namespace SAAUR.MODELS.Entities
{
    public class ModelUser
    {
        public int id { get; set; }
        [Required]
        public int id_rol { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string p_last_name { get; set; }
        [Required]
        public string m_last_name { get; set; }
        [Required]
        [EmailAddress]
        public string email { get; set; }
        public string password { get; set; }
        public string hashPass { get; set; }
        public string salt { get; set; }
    }
}