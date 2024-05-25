using System.ComponentModel.DataAnnotations;

namespace SAAUR.MODELS.Entities
{
    public class ModelApplication
    {
        public int id {  get; set; }
        [Required]
		public string name { get; set; }
        [Required]
		public string description { get; set; }
        [Required]
        public string link { get; set; }
    }
}