using System.ComponentModel.DataAnnotations;

namespace SAAUR.MODELS.Entities
{
    public class ModelActivity
    {
        public int item_id { get; set; }
        public int user_id { get; set; }
        [Required]
        public string title_act { get; set; }
        [Required]
        public string desc_act { get; set; }
        public DateTime date_assigned { get; set; }
    }
}