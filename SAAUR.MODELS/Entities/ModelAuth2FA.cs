using System.ComponentModel.DataAnnotations;

namespace SAAUR.MODELS.Entities
{
    public class ModelAuth2FA
    {
        [Required]
        [EmailAddress]
        public string email { get; set; }
    }

    public class ModelAuth2FAValid
    {
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        public string code { get; set; }
    }
}