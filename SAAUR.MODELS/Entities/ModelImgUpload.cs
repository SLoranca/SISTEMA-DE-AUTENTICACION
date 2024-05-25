using System.ComponentModel.DataAnnotations;

namespace SAAUR.MODELS.Entities
{
    public class ModelImgUpload
    {
        public int user_id {  get; set; }
        [Required]
        public string image { get; set; }
    }
}