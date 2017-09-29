using Questioning.Entity;
using Questioning.Interface;
using Questioning.Repository.Interface;
using System;
using System.Collections.Generic;
using System.IO;

namespace Questioning.Repository
{
    public class ProfileRepository: IRepository<Profile>
    {
        private readonly string separator = "\t";
        
        private readonly IPersistenceSettings persistenceSettings;

        public ProfileRepository(IPersistenceSettings persistenceSettings)
        {
            this.persistenceSettings = persistenceSettings;
        }

        private string getProfileFileName(string id)
        {
            return String.Format("{0}\\{1}{2}", persistenceSettings.GetDataPath(), 
                id, persistenceSettings.GetDataFileExtension());
        }
        public Profile Get(string id)
        {
            string[] lines = File.ReadAllLines(getProfileFileName(id));
            Profile p = new Profile();
            IList<ProfileItem> items = new List<ProfileItem>();

            foreach (var s in lines)
            {
                string[] parts = s.Split(new string[] { separator }, StringSplitOptions.None);
                items.Add(new ProfileItem() { Name = parts[0], Value = parts[1] });
            }

            p.Id = items[0].Value;
            p.Items = items;

            return p;
        }

        public IEnumerable<Profile> GetList()
        {
            IList<Profile> profiles = new List<Profile>();
            string path = persistenceSettings.GetDataPath();
            foreach (var fileName in Directory.EnumerateFiles(path, "*" + persistenceSettings.GetDataFileExtension()))
                profiles.Add(Get(Path.GetFileNameWithoutExtension(fileName)));

            return profiles;
        }

        public void Save(Profile entity)
        {
            if (!Directory.Exists(persistenceSettings.GetDataPath()))
                Directory.CreateDirectory(persistenceSettings.GetDataPath());

            using (FileStream fs = new FileStream(getProfileFileName(entity.Id), FileMode.Create, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                foreach (ProfileItem item in entity.Items)
                    sw.WriteLine(String.Format("{0}{1}{2}", item.Name, separator, item.Value));
            }
        }

        public void Delete(string id)
        {
            string profileFileName = getProfileFileName(id);
            if (File.Exists(profileFileName))
                File.Delete(profileFileName);
        }
    }
}
