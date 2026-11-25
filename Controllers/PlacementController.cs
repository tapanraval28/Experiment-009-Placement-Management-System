
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PlacementDashboard.Models;

namespace PlacementDashboard.Controllers
{
    public class PlacementController : Controller
    {
        private readonly IConfiguration _configuration;

        public PlacementController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            List<Placement> placements = new List<Placement>();

            string connectionString =
                _configuration.GetConnectionString("DefaultConnection")!;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT p.PlacementId, s.StudentId,
                           s.StudentName, s.Department,
                           p.CompanyName, p.JobRole,
                           p.PackageLPA, p.PlacementDate
                    FROM Placements p
                    INNER JOIN Students s
                    ON p.StudentId = s.StudentId";

                SqlCommand cmd = new SqlCommand(query, con);

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    placements.Add(new Placement
                    {
                        PlacementId = Convert.ToInt32(reader["PlacementId"]),
                        StudentId = Convert.ToInt32(reader["StudentId"]),
                        StudentName = reader["StudentName"].ToString()!,
                        Department = reader["Department"].ToString()!,
                        CompanyName = reader["CompanyName"].ToString()!,
                        JobRole = reader["JobRole"].ToString()!,
                        PackageLPA = Convert.ToDecimal(reader["PackageLPA"]),
                        PlacementDate = Convert.ToDateTime(reader["PlacementDate"])
                    });
                }

                reader.Close();
            }

            return View(placements);
        }
    }
}
