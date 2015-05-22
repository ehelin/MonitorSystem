namespace Shared.Helpers
{
    public class Utility
    {
        public static string GetAppSetting(string name)
        {
            return System.Configuration.ConfigurationManager.AppSettings[name];
        }

        public static string[] SplitString(char character, string listToSplit)
        {
            string[] entries = null;

            entries = listToSplit.Split(character);

            return entries;
        }     
    }
}
