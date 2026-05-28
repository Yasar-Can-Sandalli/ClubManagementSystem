using System;

namespace ClubManagementSystem.Models
{
    public class ClubEvent : BaseEntity
    {
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }

        public string DaysRemaining
        {
            get
            {
                var timeSpan = EventDate - DateTime.Now;
                if (timeSpan.Days > 0)
                    return $"{timeSpan.Days} gün kaldı";
                if (timeSpan.Days == 0)
                    return "Bugün!";
                return "Tamamlandı";
            }
        }
    }
}