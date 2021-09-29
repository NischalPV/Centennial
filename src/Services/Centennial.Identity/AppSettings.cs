namespace Centennial.Identity
{
    public class AppSettings
    {
        public string IsClusterEnv { get; set; }

        public string WebApiUrl { get; set; }
        public string AngularUrl { get; set; }

        public bool UseCustomizationData { get; set; }
    }
}