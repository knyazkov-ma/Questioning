using Questioning.Interface;
using System;
using System.Configuration;

namespace Questioning
{
    public class PersistenceSettings: IPersistenceSettings
    {
        private readonly string dataPath;
        private readonly string dataFileExtension;

        private PersistenceSettings(string dataPath, string dataFileExtension)
        {
            this.dataPath = dataPath;
            this.dataFileExtension = dataFileExtension;
        }

        public PersistenceSettings()
        {
       
        }

        private static Lazy<PersistenceSettings> instance = new Lazy<PersistenceSettings>(() =>
        {
            var appSettings = ConfigurationManager.AppSettings;
            var instance = new PersistenceSettings(appSettings["dataPath"], appSettings["dataFileExtension"]);
            return instance;
        });

        public string GetDataPath()
        {
            return instance.Value.dataPath;
        }

        public string GetDataFileExtension()
        {
            return instance.Value.dataFileExtension;
        }
    }
}
