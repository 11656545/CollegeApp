using CollegeApp.Data;
using CollegeApp.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly CollegeDBContext _dBContext;
        public StudentController(CollegeDBContext dBContext)
        {
            _dBContext = dBContext;
        }
        [HttpGet("All")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<StudentDTO>> GetStudents()
        {
            var students = _dBContext.Students.Select(s => new StudentDTO
            {
                Id = s.Id,
                Name = s.Name,
                Email = s.Email,
                Address = s.Address,
                DOB = s.DOB
            }).ToList();

            return Ok(students);
        }

        [HttpGet("{id}:int", Name = "GetStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> GetStudentById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid student ID was entered");
            }
            var student = _dBContext.Students.Where(n => n.Id == id).FirstOrDefault();
            if (student == null)
            {
                return NotFound($"Student with {id} not found");
            }
            var studentDTO = new StudentDTO
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                Address = student.Address,
                DOB = student.DOB
            };
            return Ok(studentDTO);
        }
        [HttpGet]
        [Route("{name}:alpha")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> GetStudentByName(string name)
        {
            var student = _dBContext.Students.Where(n => n.Name == name).FirstOrDefault();
            if (student == null)
            {
                return NotFound($"Student with name {name} not found");
            }
            var studentDTO = new StudentDTO
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                Address = student.Address,
                DOB = student.DOB
            };
            return Ok(studentDTO);
        }

        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> CreateStudent([FromBody] StudentDTO model)
        {
            //Manual validation
            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);
            if (model == null)
            {
                return BadRequest("Student data is null");
            }
            int newId = _dBContext.Students.Max(s => s.Id) + 1;
            Student student = new Student
            {
                Name = model.Name,
                Email = model.Email,
                Address = model.Address,
                DOB = model.DOB
            };
            _dBContext.Students.Add(student);
            _dBContext.SaveChanges();
            return CreatedAtRoute("GetStudentById", new { id = student.Id }, model);
        }

        [HttpPut("{id:int}")]
        public ActionResult<StudentDTO> UpdateStudent([FromBody] StudentDTO model)
        {
            if (model == null)
            {
                return BadRequest("Student data is null");
            }
            var existingstudent = _dBContext.Students.Where(s => s.Id == model.Id).FirstOrDefault();
            if (existingstudent == null)
            {
                return NotFound($"Student with ID {model.Id} not found");
            }
            if (string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Address))
            {
                return BadRequest("Student data is incomplete");
            }
            existingstudent.Name = model.Name;
            existingstudent.Email = model.Email;
            existingstudent.Address = model.Address;
            existingstudent.DOB = model.DOB;
            _dBContext.SaveChanges();
            return Ok(model);
        }

        [HttpPatch]
        [Route("{id:int}/UpdatePartial")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult UpdateStudentPartial(int id, [FromBody] JsonPatchDocument<StudentDTO> patchmodel)
        {
            if(patchmodel == null)
            {
                return BadRequest("Student data is null");
            }
            if (id <= 0)
            {
                return BadRequest("Invalid student ID was entered");
            }
            var existingstudent = _dBContext.Students.Where(s => s.Id == id).FirstOrDefault();
            var studentDTO = new StudentDTO
            {
                Id = existingstudent.Id,
                Name = existingstudent.Name,
                Email = existingstudent.Email,
                Address = existingstudent.Address,
                DOB = existingstudent.DOB
            };

            patchmodel.ApplyTo(studentDTO);

            if(string.IsNullOrEmpty(studentDTO.Name) || string.IsNullOrEmpty(studentDTO.Email) || string.IsNullOrEmpty(studentDTO.Address))
            {
                return BadRequest("Student data is incomplete");
            }
            existingstudent.Name = studentDTO.Name;
            existingstudent.Email = studentDTO.Email;
            existingstudent.Address = studentDTO.Address;
            existingstudent.DOB = studentDTO.DOB;
            _dBContext.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}:int")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<bool> DeleteStudentByID(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid student ID was entered");
            }
            var student = _dBContext.Students.Where(n => n.Id == id).FirstOrDefault();
            if (student == null)
            {
                return NotFound($"Student with {id} not found");
            }
            _dBContext.Students.Remove(student);
            _dBContext.SaveChanges();
            return Ok(true);
        }
        
    }
}
