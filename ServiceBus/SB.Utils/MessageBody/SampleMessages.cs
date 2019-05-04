using System;
using System.Collections.Generic;

namespace SB.Utils
{
    public static class SampleMessages
    {
        public static IEnumerable<MeetupAttendee> Attendees 
            => new List<MeetupAttendee>
                {
                    MeetupAttendee.Nady,
                    MeetupAttendee.Rashed,
                    MeetupAttendee.Sameh,
                    MeetupAttendee.Maher,
                    MeetupAttendee.Wael
                };

        public static string Text01 => $"Message #{DateTime.Now.Ticks}";
    }
}
