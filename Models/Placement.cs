
namespace PlacementDashboard.Models
{
    public class Placement
    {
        public int PlacementId { get; set; }

        public int StudentId { get; set; }

        public string StudentName { get; set; } = string.Empty;

        public string Department { get; set; } = string.Empty;

        public string CompanyName { get; set; } = string.Empty;

        public string JobRole { get; set; } = string.Empty;

        public decimal PackageLPA { get; set; }

        public DateTime PlacementDate { get; set; }
    }
}
