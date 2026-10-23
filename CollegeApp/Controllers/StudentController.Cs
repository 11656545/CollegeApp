using Microsoft.AspNetCore.Mvc;
using CollegeApp.Models;
namespace CollegeApp.Controllers
{
    [Route("controller")]
    [ApiController]
    public class StudentController : ControllerBase
    { 
        [HttpGet]
        public IEnumerable<Student> GetStudents()
        {
            return CollegeRepository.Students;
        }

        [HttpGet("{id}:int")]
        public Student GetStudentById(int id)
        {
            return CollegeRepository.Students.Where(n=>n.Id == id).FirstOrDefault();
        }
    }
}
