using System;

namespace ClubManagementSystem.Models
{
    public class ClubProject : BaseEntity
    {
        public string ProjectCode { get; set; } 
        public string ProjectName { get; set; } 
        public string AdvisorName { get; set; } 
        public string Status { get; set; }      
    }
}