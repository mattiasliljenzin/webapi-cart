using System.Configuration;

namespace CrazyCart.Setup
{
    public interface IApplicationSettings
    {
        string AppHostUrl { get; }
        string MongoDbConnectionString { get; }
    }

    public class ApplicationSettings : IApplicationSettings
    {
        public string AppHostUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["AppHostUrl"];
            }
        }

        public string MongoDbConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["CrazyCartMongoDbConnectionString"].ConnectionString;
            }
        }
    }
}