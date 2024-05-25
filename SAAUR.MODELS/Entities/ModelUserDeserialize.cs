

namespace SAAUR.MODELS.Entities
{
    
    public class ModelUserDeserialize : ModelResponse
    {
        public int id { get; set; }
        public string name { get; set; }
        public string p_last_name { get; set; }
        public string m_last_name { get; set; }
        public string rol { get; set; }
		public string email { get; set; }
		public string img { get; set; }
		public string hashPass { get; set; }
        public string salt { get; set; }
        public string json_apps { get; set; }
		public string json_modules { get; set; }
	}

    public class ModelInfoResult : ModelResponse
    {
		public string token { get; set; }
        public string json_apps { get; set; }
		public string json_modules { get; set; }
        public string Url{get;set;}
        public int twofactor {get;set;}
        public string email {get;set;}
	}

    public class ModelTwoFactorString : ModelUserDeserialize
    {
		public string twofactor {get;set;}
	}

    public class ModelTwoFactorInteger : ModelUserDeserialize
    {
		public int twofactor {get;set;}
	}

}