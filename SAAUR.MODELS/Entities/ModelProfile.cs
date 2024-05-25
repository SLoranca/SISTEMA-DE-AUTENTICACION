namespace SAAUR.MODELS.Entities
{
    public class ModelProfile
    {
        public int user_id { get; set; }
        public string name { get; set; }
        public string p_last_name { get; set; }
        public string m_last_name { get; set; }
        public string email { get; set; }
    }

    public class ModelProfilePassword
	{
		public int user_id { get; set; }
		public string password { get; set; }
        public string new_password { get; set; }
        public string confirm_password { get; set; }
        public string hashPass { get; set; }
		public string salt { get; set; }
	}

    public class ModelProfileEmail
    {
        public int user_id { get; set; }
        public string new_email { get; set; }
        public string confirm_password { get; set; }
    }
}