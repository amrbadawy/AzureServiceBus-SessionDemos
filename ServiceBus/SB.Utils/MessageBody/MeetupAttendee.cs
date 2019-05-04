using System;

namespace SB.Utils
{
    public class MeetupAttendee
    {
        #region » Sample Data 

        public static MeetupAttendee Maher =
            new MeetupAttendee
            {
                Id = 10,
                Name = "Maher Maher",
                DoB = DateTime.Now.AddYears(-30),
                City = Cities.Riyadh,
                Category = Categories.Regular,
                Contact = new ContactInfo
                {
                    Email = "Maher@Maher.dev",
                    Mobile = "9661010101010"
                }
            };

        public static MeetupAttendee Rashed =
            new MeetupAttendee
            {
                Id = 20,
                Name = "Rashed Rashed",
                DoB = DateTime.Now.AddYears(-35),
                City = Cities.Riyadh,
                Category = Categories.VIP,
                Contact = new ContactInfo
                {
                    Email = "Rashed@Rashed.dev",
                    Mobile = "96620202020"
                }
            };

        public static MeetupAttendee Nady =
            new MeetupAttendee
            {
                Id = 30,
                Name = "Nady Nady",
                DoB = DateTime.Now.AddYears(-40),
                City = Cities.Dammam,
                Category = Categories.VIP,
                Contact = new ContactInfo
                {
                    Email = "Nady@Nady.dev",
                    Mobile = "9663030303030"
                }
            };

        public static MeetupAttendee Sameh =
            new MeetupAttendee
            {
                Id = 40,
                Name = "Sameh Sameh",
                DoB = DateTime.Now.AddYears(-25),
                City = Cities.Dammam,
                Category = Categories.Regular,
                Contact = new ContactInfo
                {
                    Email = "Sameh@Sameh.dev",
                    Mobile = "9664040404040"
                }
            };

        public static MeetupAttendee Wael =
            new MeetupAttendee
            {
                Id = 50,
                Name = "Wael Wael",
                DoB = DateTime.Now.AddYears(-15),
                City = Cities.Mecca,
                Category = Categories.VIP,
                Contact = new ContactInfo
                {
                    Email = "Wael@Wael.dev",
                    Mobile = "9665050505050"
                }
            };

        #endregion

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DoB { get; set; }
        public ContactInfo Contact { get; set; }
        public string City { get; set; }
        public string Category { get; set; }

        public sealed class ContactInfo
        {
            public string Email { get; set; }
            public string Mobile { get; set; }
        }
    }

    public sealed class Cities
    {
        public const string Riyadh = "Riyadh";
        public const string Dammam = "Dammam";
        public const string Jeddah = "Jeddah";
        public const string Mecca = "Mecca";
    }
    public sealed class Categories
    {
        public const string VIP = "VIP";
        public const string Regular = "Regular";
    }
}
