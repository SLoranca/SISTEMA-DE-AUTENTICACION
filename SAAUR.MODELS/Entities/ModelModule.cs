using System.ComponentModel.DataAnnotations;

namespace SAAUR.MODELS.Entities
{
	public class ModelModule
	{
        public int id { get; set; }
        public int app_id { get; set; }
        [Required]
        public string title { get; set; }
		[Required]
		public string action { get; set; }
		[Required]
		public string controller { get; set; }
    }
}
