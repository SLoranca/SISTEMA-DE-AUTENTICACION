using System.ComponentModel.DataAnnotations;

namespace SAAUR.MODELS.Entities
{
    public class ModelAccountRecovery
    {
        [Required]
        [EmailAddress]
        public string destination_email { get; set; }
    }

    public class ModelRecovery
    {
        public string email { get; set; }
        public string pwd { get; set; }
        public string hashPass { get; set; }
        public string salt { get; set; }
    }
}