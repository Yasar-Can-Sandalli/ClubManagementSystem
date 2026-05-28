namespace ClubManagementSystem.Models
{
    public class ClubMember : Person
    {
        public string StudentNumber { get; set; }
        public string Department { get; set; }
        public bool IsActive { get; set; }
        public string Role { get; set; } 

        public override string GetRoleDescription()
        {
            return Role ?? "Aktif Kulüp Üyesi";
        }
    }
}