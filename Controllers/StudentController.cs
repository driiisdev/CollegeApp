using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml.Linq;
using CollegeApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc; 

namespace CollegeApp.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;

        public StudentController(ILogger<StudentController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("All", Name = "GetAllStudents")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<StudentDTO>> GetStudents()
        {
            _logger.LogInformation("ur message");
            var students = new List<StudentDTO>();

            foreach (var item in CollegeRepository.Students)
            {
                StudentDTO obj = new StudentDTO()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Age = item.Age,
                    Address = item.Address,
                    Email = item.Email
                };
                students.Add(obj);
            }

            return Ok(students);
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetStudentById")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> GetStudentById(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Bad Request");
                return BadRequest();
            }

            var student = CollegeRepository.Students.Where(n => n.Id == id).FirstOrDefault();

            if (student == null)
            {
                _logger.LogError("student not found with given id");
                return NotFound($"The student name {id} not found");
            }

            var studentDTO = new StudentDTO
            {
                Id = student.Id,
                Name = student.Name,
                Age = student.Age,
                Address = student.Address,
                Email = student.Email
            };

            return Ok(studentDTO);
        }

        [HttpGet]
        [Route("{name:alpha}", Name = "GetStudentByName")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> GetStudentByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest();
            }

            var student = CollegeRepository.Students.Where(n => n.Name == name).FirstOrDefault();

            if (student == null)
            {
                return NotFound($"The student name {name} not found");
            }

            var studentDTO = new StudentDTO
            {
                Id = student.Id,
                Name = student.Name,
                Age = student.Age,
                Address = student.Address,
                Email = student.Email
            };

            return Ok(student);
        }

        [HttpPost]
        [Route("Create")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> CreateStudent([FromBody] StudentDTO model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            int newId = CollegeRepository.Students.LastOrDefault().Id + 1;

            Student student = new Student()
            {
                Id = newId,
                Name = model.Name,
                Age = model.Age,
                Address = model.Address,
                Email = model.Email
            };

            CollegeRepository.Students.Add(student);

            model.Id = student.Id;

            return CreatedAtRoute("GetStudentById", new { id = model.Id }, model);
        }

        [HttpPut]
        [Route("Update")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> UpdateStudent([FromBody] StudentDTO model)
        {
            if (model == null || model.Id <= 0)
            {
                return BadRequest();
            }

            var existingStudent = CollegeRepository.Students.Where((s) => s.Id == model.Id).FirstOrDefault();

            if (existingStudent == null)
            {
                return NotFound();
            }

            existingStudent.Name = model.Name;
            existingStudent.Age = model.Age;
            existingStudent.Address = model.Address;
            existingStudent.Email = model.Email;

            return NoContent();
        }

        [HttpPatch]
        [Route("UpdatePartial/{id:int}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> UpdateStudentPartial(int id, [FromBody] JsonPatchDocument<StudentDTO> patchDocument)
        {
            if (patchDocument == null || id <= 0)
            {
                return BadRequest();
            }

            var existingStudent = CollegeRepository.Students.Where((s) => s.Id == id).FirstOrDefault();

            if (existingStudent == null)
            {
                return NotFound();
            }

            var studentDTO = new StudentDTO
            {
                Id = existingStudent.Id,
                Name = existingStudent.Name,
                Age = existingStudent.Age,
                Address = existingStudent.Address,
                Email = existingStudent.Email,
            };

            patchDocument.ApplyTo(studentDTO, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            existingStudent.Name = studentDTO.Name;
            existingStudent.Age = studentDTO.Age;
            existingStudent.Address = studentDTO.Address;
            existingStudent.Email = studentDTO.Email;

            return NoContent();
        }

        [HttpDelete]
        [Route("Delete/{id:int}", Name = "DeleteStudentById")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<bool> DeleteStudentById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var student = CollegeRepository.Students.Where(n => n.Id == id).FirstOrDefault();
            
            if (student == null)
            {
                return NotFound($"The student name {id} not found");
            }

            CollegeRepository.Students.Remove(student);
            return Ok(true);
        }
    }
}
