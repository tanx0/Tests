using System.Configuration;

namespace ReactAppTest
{
    public class Config
    {
        public const int PollingIntervalMilliseconds = 100;
        //Url can be passed as a command line parameter or hardcoded in app.config
        //priority is given to command line parameter
        public static string Url = NUnit.Framework.TestContext.Parameters["url"] ?? ConfigurationManager.AppSettings["url"];

    }
}
