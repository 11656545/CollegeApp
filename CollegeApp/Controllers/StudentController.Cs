using Microsoft.AspNetCore.Mvc;
using CollegeApp.Models;
namespace CollegeApp.Controllers
{
    [Route("controller")]
    [ApiController]
    public class StudentController : ControllerBase
    { 
        [HttpGet]
        public IEnumerable<Student> GetStudentName()
        {
            return new List<Student>
            {
                new Student {
                Id = 1,
                Name = "John Doe",
                Email = "studnentemail1@gmail.com",
                Address = "123 Main St, City, Country"},
                new Student {
                Id = 2,
                Name = "Jane Smith",
                Email = "studentemail2@gmail.com",
                Address = "456 Oak Ave, City, Country"}
            };
        }
    }
}
