using Microsoft.AspNetCore.Mvc;

namespace CollegeApp.Controllers
{
    [Route("controller")]
    [ApiController]
    public class StudentController : ControllerBase
    { 
        [HttpGet]
        public string GetStudentName()
        {
            return "student name 1";
        }
    }
}
