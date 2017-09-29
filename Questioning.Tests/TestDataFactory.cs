using Questioning.Entity;
using System;
using System.Collections.Generic;

namespace Questioning.Tests
{
    public static class TestDataFactory
    {
        public static IEnumerable<Profile> GetCorrectProfileList()
        {
            return new List<Profile>()
            {
                new Profile()
                {
                    Id = "Иванов Иван Иванович",
                    Items = new List<ProfileItem>()
                    {
                        new ProfileItem(){ Name = Resource.Label_FIO, Value="Иванов Иван Иванович" },
                        new ProfileItem(){ Name = Resource.Label_DR, Value="01.02.1988" },
                        new ProfileItem(){ Name = Resource.Label_FavoriteProgramLanguage, Value="PHP" },
                        new ProfileItem(){ Name = Resource.Label_ExperienceProgrammingInspecifiedLanguage, Value="2"},
                        new ProfileItem(){ Name = Resource.Label_MobilePhone, Value="+72223334455" },
                        new ProfileItem(){ Name = Resource.Label_ProfileFilledOudDate, Value="01.02.2017" }
                    }
                },
                new Profile()
                {
                    Id = "Петров Петр Петрович",
                    Items = new List<ProfileItem>()
                    {
                        new ProfileItem(){ Name = Resource.Label_FIO, Value="Петров Петр Петрович" },
                        new ProfileItem(){ Name = Resource.Label_DR, Value="01.11.1989" },
                        new ProfileItem(){ Name = Resource.Label_FavoriteProgramLanguage, Value="C#" },
                        new ProfileItem(){ Name = Resource.Label_ExperienceProgrammingInspecifiedLanguage, Value="4"},
                        new ProfileItem(){ Name = Resource.Label_MobilePhone, Value="+72223334455" },
                        new ProfileItem(){ Name = Resource.Label_ProfileFilledOudDate, Value="10.02.2017" }
                    }
                },
                new Profile()
                {
                    Id = "Семенов Семен Семенович",
                    Items = new List<ProfileItem>()
                    {
                        new ProfileItem(){ Name = Resource.Label_FIO, Value="Семенов Семен Семенович" },
                        new ProfileItem(){ Name = Resource.Label_DR, Value="01.02.1977" },
                        new ProfileItem(){ Name = Resource.Label_FavoriteProgramLanguage, Value="C#" },
                        new ProfileItem(){ Name = Resource.Label_ExperienceProgrammingInspecifiedLanguage, Value="14"},
                        new ProfileItem(){ Name = Resource.Label_MobilePhone, Value="+72223334455" },
                        new ProfileItem(){ Name = Resource.Label_ProfileFilledOudDate, Value="01.03.2017" }
                    }
                },
                new Profile()
                {
                    Id = "Втулкин Сергей Сергеевич",
                    Items = new List<ProfileItem>()
                    {
                        new ProfileItem(){ Name = Resource.Label_FIO, Value="Втулкин Сергей Сергеевич" },
                        new ProfileItem(){ Name = Resource.Label_DR, Value="12.11.1985" },
                        new ProfileItem(){ Name = Resource.Label_FavoriteProgramLanguage, Value="PHP" },
                        new ProfileItem(){ Name = Resource.Label_ExperienceProgrammingInspecifiedLanguage, Value="5"},
                        new ProfileItem(){ Name = Resource.Label_MobilePhone, Value="+72223334455" },
                        new ProfileItem(){ Name = Resource.Label_ProfileFilledOudDate, Value="01.02.2017" }
                    }
                },
                new Profile()
                {
                    Id = "Печкин Эдуард Викторович",
                    Items = new List<ProfileItem>()
                    {
                        new ProfileItem(){ Name = Resource.Label_FIO, Value="Печкин Эдуард Викторович" },
                        new ProfileItem(){ Name = Resource.Label_DR, Value="01.12.1979" },
                        new ProfileItem(){ Name = Resource.Label_FavoriteProgramLanguage, Value="Java Script" },
                        new ProfileItem(){ Name = Resource.Label_ExperienceProgrammingInspecifiedLanguage, Value="10"},
                        new ProfileItem(){ Name = Resource.Label_MobilePhone, Value="+72223334455" },
                        new ProfileItem(){ Name = Resource.Label_ProfileFilledOudDate, Value="11.02.2017" }
                    }
                },
                new Profile()
                {
                    Id = "Куликов Виктор Семенович",
                    Items = new List<ProfileItem>()
                    {
                        new ProfileItem(){ Name = Resource.Label_FIO, Value="Куликов Виктор Семенович" },
                        new ProfileItem(){ Name = Resource.Label_DR, Value="01.02.1988" },
                        new ProfileItem(){ Name = Resource.Label_FavoriteProgramLanguage, Value="PHP" },
                        new ProfileItem(){ Name = Resource.Label_ExperienceProgrammingInspecifiedLanguage, Value="2"},
                        new ProfileItem(){ Name = Resource.Label_MobilePhone, Value="+72223334455" },
                        new ProfileItem(){ Name = Resource.Label_ProfileFilledOudDate, Value=DateTime.Now.ToShortDateString() }
                    }
                },
                new Profile()
                {
                    Id = "Петров Иван Иванович",
                    Items = new List<ProfileItem>()
                    {
                        new ProfileItem(){ Name = Resource.Label_FIO, Value="Петров Иван Иванович" },
                        new ProfileItem(){ Name = Resource.Label_DR, Value="15.07.1986" },
                        new ProfileItem(){ Name = Resource.Label_FavoriteProgramLanguage, Value="C#" },
                        new ProfileItem(){ Name = Resource.Label_ExperienceProgrammingInspecifiedLanguage, Value="4"},
                        new ProfileItem(){ Name = Resource.Label_MobilePhone, Value="+72223334455" },
                        new ProfileItem(){ Name = Resource.Label_ProfileFilledOudDate, Value=DateTime.Now.ToShortDateString() }
                    }
                }
            };
        }
    }
}
