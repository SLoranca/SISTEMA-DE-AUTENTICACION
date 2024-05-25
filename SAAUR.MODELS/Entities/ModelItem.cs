using System.ComponentModel.DataAnnotations;

namespace SAAUR.MODELS.Entities
{
    public class ModelItem
    {
        public int user_id {  get; set; }
        [Required]
        public string title { get; set; }
    }
}