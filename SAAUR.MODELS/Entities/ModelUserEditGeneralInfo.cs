using System.ComponentModel.DataAnnotations;

namespace SAAUR.MODELS.Entities
{
    public class ModelUserEditGeneralInfo
    {
        public int user_id { get; set; }
        [Required]
        public int id_rol { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public string p_last_name { get; set; }
        [Required]
        public string m_last_name { get; set; }
    }
}