using Questioning.Entity;
using System;
using System.Linq;
using System.Collections.Generic;
using Questioning.DTO;
using System.IO.Compression;
using System.IO;
using Questioning.Commands;
using Questioning.Helpers;
using Questioning.Services.Interface;
using Questioning.Interface;
using Questioning.Repository.Interface;

namespace Questioning.Services
{
    public class QuestionService: IQuestionService
    {
        private readonly IRepository<Profile> profileRepository;
        private readonly IPersistenceSettings persistenceSettings;


        public QuestionService(IRepository<Profile> profileRepository, IPersistenceSettings persistenceSettings)
        {
            this.profileRepository = profileRepository;
            this.persistenceSettings = persistenceSettings;
        }


        public QuestionDTO[] GetNewProfile()
        {
            return new QuestionDTO[]
            {
                new QuestionDTO()
                {
                    TypeAnswer = TypeAnswer.String,
                    Name = Resource.Label_FIO
                },
                new QuestionDTO()
                {
                    TypeAnswer = TypeAnswer.DateTime,
                    Name = Resource.Label_DR
                },
                new QuestionDTO()
                {
                    TypeAnswer = TypeAnswer.String,
                    Name = Resource.Label_FavoriteProgramLanguage
                },
                new QuestionDTO()
                {
                    TypeAnswer = TypeAnswer.Int,
                    Name = Resource.Label_ExperienceProgrammingInspecifiedLanguage
                },
                new QuestionDTO()
                {
                    TypeAnswer = TypeAnswer.String,
                    Name = Resource.Label_MobilePhone
                }
            };
        }

        public void SaveProfile(QuestionDTO[] questions)
        {
            IList<ProfileItem> items = new List<ProfileItem>();
            foreach (var q in questions)
            {
                string val = null;
                switch (q.TypeAnswer)
                {
                    case TypeAnswer.DateTime:
                        val = q.DateTimeValue.Value.ToShortDateString();
                        break;
                    case TypeAnswer.Int:
                        if (q.IntValue < 0)
                            q.IntValue = -q.IntValue;
                        val = q.IntValue.ToString();
                        break;
                    case TypeAnswer.String:
                        val = q.StringValue;
                        break;
                }
                items.Add(new ProfileItem() { Name = q.Name, Value = val });
            }
            items.Add(new ProfileItem() { Name = Resource.Label_ProfileFilledOudDate, Value = DateTime.Now.ToShortDateString() });

            profileRepository.Save(new Profile() { Id = items[0].Value, Items = items });
        }

        public void ValidateQuestionAnswer(QuestionDTO[] questions, QuestionDTO question)
        {
            string stringConstraintMsg = null;
            if (question.Name == Resource.Label_DR)
            {
                if(!question.DateTimeValue.HasValue)
                    throw new CommandException(Resource.Text_EmptyConstraintMsg);

                if(question.DateTimeValue.Value.Date > DateTime.Now.Date)
                    throw new CommandException(Resource.Test_DRCanNotBeMoreCurrentDate);
            }
            else if (question.Name == Resource.Label_ExperienceProgrammingInspecifiedLanguage)
            {
                if (questions[1].DateTimeValue.HasValue)
                {
                    if (questions!= null && questions[1].DateTimeValue.Value.GetAge(DateTime.Now) <= question.IntValue)
                        throw new CommandException(Resource.Test_ExperienceCanNotBeMoreAge);
                }
            }
            else if (question.Name == Resource.Label_FavoriteProgramLanguage)
            {
                string[] languages = new string[] { "PHP", "Java", "C#", "Java Script", "Python", "C++" };
                stringConstraintMsg = question.StringValue
                    .GetStringConstraintMsg(true, languages.Max(t => t.Length), languages.Min(t => t.Length));
                
                if (!languages.Select(l => l.ToLower()).Contains(question.StringValue.ToLower()))
                    throw new CommandException(String.Format(Resource.Text_LanguageMastBeIn, String.Join(", ", languages)));
            }
            else if (question.Name == Resource.Label_FIO)
            {
                stringConstraintMsg = question.StringValue.GetStringConstraintMsg(true, 200, 3);
            }
            else if (question.Name == Resource.Label_MobilePhone)
            {
                stringConstraintMsg = question.StringValue.GetStringConstraintMsg(true, "+00 000 000 00 00".Length, "000000".Length);
            }

            if (stringConstraintMsg != null)
                throw new CommandException(stringConstraintMsg);
        }

        public void DeleteProfile(string fio)
        {
            profileRepository.Delete(fio);
        }

        public QuestionDTO[] GetProfile(string fio)
        {
            Profile p = profileRepository.Get(fio);
            if (p == null)
                return null;
            IList<QuestionDTO> questions = new List<QuestionDTO>();
            foreach (ProfileItem item in p.Items)
            {
                QuestionDTO q = new QuestionDTO() { Name = item.Name };
                if (item.Name == Resource.Label_DR)
                {
                    q.TypeAnswer = TypeAnswer.DateTime;
                    q.DateTimeValue = DateTime.Parse(item.Value);
                }
                else if (item.Name == Resource.Label_ExperienceProgrammingInspecifiedLanguage)
                {
                    q.TypeAnswer = TypeAnswer.Int;
                    q.IntValue = Int32.Parse(item.Value);
                }
                else
                {
                    q.TypeAnswer = TypeAnswer.String;
                    q.StringValue = item.Value;
                }
                questions.Add(q);
            }
            return questions.ToArray();
        }

        public IEnumerable<string> GetAllProfileNames()
        {
            return profileRepository.GetList()
                .OrderBy(p => p.Id)
                .Select(p => p.Id);
        }

        public IEnumerable<string> GetAllTodayProfileNames()
        {
            return profileRepository.GetList()
                .Where(p => p.Items.LastOrDefault().Value == DateTime.Now.ToShortDateString())
                .OrderBy(p => p.Id)
                .Select(p => p.Id);
        }

        

        public StatisticsDTO GetStatistics()
        {
            IEnumerable<Profile> profiles = profileRepository.GetList();
            StatisticsDTO s = null;
            
            if (profiles != null && profiles.Any())
            {
                s = new StatisticsDTO();
                s.AverageAge = (int)profiles
                    .Select(p => DateTime.Parse(p.Items.Skip(1).FirstOrDefault().Value).GetAge(DateTime.Now))
                    .Average();
                s.TopProgramLanguage = profiles.GroupBy(t => t.Items.Skip(2).FirstOrDefault().Value)
                    .Select(g => new { g.Key, Count = g.Count() })
                    .OrderByDescending(t => t.Count)
                    .FirstOrDefault().Key; 
            }

            return s;
        }

        public void ZipProfile(string fio, string path)
        {
            string profileFileName = String.Format("{0}\\{1}{2}", persistenceSettings.GetDataPath(), 
                fio, persistenceSettings.GetDataFileExtension());

            if (!File.Exists(profileFileName))
                return;

            using (FileStream zipToOpen = new FileStream(String.Format("{0}\\{1}.zip", path, fio), 
                FileMode.Create))
            {
                using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Create))
                {
                    ZipArchiveEntry readmeEntry = archive
                        .CreateEntry(fio + persistenceSettings.GetDataFileExtension());

                    using (StreamWriter writer = new StreamWriter(readmeEntry.Open()))
                    {
                        string[] lines = File.ReadAllLines(profileFileName);
                        foreach(var s in lines)
                            writer.WriteLine(s);                        
                    }
                }
            }
        }
    }
}
