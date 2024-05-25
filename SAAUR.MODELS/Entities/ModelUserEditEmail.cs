using System.ComponentModel.DataAnnotations;

namespace SAAUR.MODELS.Entities
{
    public class ModelUserEditEmail
    {
        public int user_id {get;set;}
        [Required]
        [EmailAddress]
        public string email {get;set;}
    }
}