namespace Locator.Common
{
    public class AppSetting
    {
        public MongoDbSetting MongoDbSetting { get; set; } = null;
        public Features Featuers { get; set; } = null;
        public RabbitMqConfigurations RabbitMqConfigurations { get; set; } = null;
    }

    public class MongoDbSetting
    {
        public string Host { get; set; } = null;
        public string DatabaseName { get; set; } = null;
    }

    public partial class Featuers
    {

    }

    public class RabbitMqConfigurations
    {
        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

}
