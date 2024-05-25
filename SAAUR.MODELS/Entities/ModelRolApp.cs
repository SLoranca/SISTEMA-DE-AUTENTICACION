namespace SAAUR.MODELS.Entities
{
    public class ModelRolApp
    {
        public int rol_id { get; set; }
        public int app_id { get; set; }
        public IEnumerable<ModelRolModuleList> modules { get; set; }
    }

    public class ModelRolModuleList
    {
        public int module_id { get; set; }
    }
}
