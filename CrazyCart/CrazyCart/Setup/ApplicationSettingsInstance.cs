namespace CrazyCart.Setup
{
    public static class ApplicationSettingsInstance
    {
        private static IApplicationSettings _applicationSettings;

        public static IApplicationSettings Instance
        {
            get
            {
                if (_applicationSettings == null)
                {
                    _applicationSettings = DependecyResolver.Resolve<IApplicationSettings>();
                }
                return _applicationSettings;
            }
        }
    }
}