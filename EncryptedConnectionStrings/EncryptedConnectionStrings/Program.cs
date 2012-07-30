using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace EncryptedConnectionStrings
{
    class Program
    {
        static void Main(string[] args)
        {
            // Uncomment for testing
            args = new string[2];
            args[0] = @"F:\Github\EncryptedConnectionStrings\EncryptedConnectionStrings\EncryptedConnectionStrings\App.config";
            args[1] = "Data.Sql.ConnectionString";

            Console.WriteLine("The Connection String: {0}", OpenRemoteConnectionString(args[0], args[1]));
            Console.WriteLine("The Connection String: {0}, is Encrypted = {1}", args[1], DecryptEncryptConnectionString(args[0]).ToString());
            Console.ReadLine();
        }

        private static bool DecryptEncryptConnectionString(string path)
        {
            ExeConfigurationFileMap file = new ExeConfigurationFileMap();
            file.ExeConfigFilename = path;
            ConfigurationUserLevel level = new ConfigurationUserLevel();

            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(file, level);
            ConfigurationSection section = config.GetSection("connectionStrings");

            // Encrypt Configuration ConnectionString
            if (!section.SectionInformation.IsProtected)
            {
                section.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");
                section.SectionInformation.ForceSave = true;
                config.Save(ConfigurationSaveMode.Modified);
                return true;
            }

            // Decrypt Configuration ConnectionString
            else 
            {
                section.SectionInformation.UnprotectSection();
                config.Save();
                return false;
            }
        }

        private static string OpenRemoteConnectionString(string path, string name)
        {
            ExeConfigurationFileMap file = new ExeConfigurationFileMap();
            file.ExeConfigFilename = path;
            ConfigurationUserLevel level = new ConfigurationUserLevel();
            Configuration config = ConfigurationManager.OpenMappedExeConfiguration(file, level);
            return config.ConnectionStrings.ConnectionStrings[name].ConnectionString;
        }
    }
}
