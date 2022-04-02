using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CrudWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        //лист 
        //private static List<Student> students = new List<Student>
        //    {
        //        new Student {Id = 1, Name = "Red",
        //            FirstName = "Shohruh",
        //            LastName = "Abduvohidov",
        //            Place = "Tashkent City"},
        //        new Student {Id = 2,
        //            Name = "Lazyman",
        //            FirstName = "Sarvar",
        //            LastName = "Ernazarov",
        //            Place = "Tashkent City"},
        //        new Student {Id = 3,
        //            Name = "SpiderMan",
        //            FirstName = "Peter",
        //            LastName = "PArker",
        //            Place = "NewYork City"}
        //    };
       
        private readonly DataContext _context;

        public StudentController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Student>>> Get()
        {
            
            return Ok(await _context.Students.ToListAsync());
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> Get(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
                return BadRequest("Student not found");
            
            return Ok(student);
        }

        [HttpPost]
        public async Task<ActionResult<List<Student>>> AddStudent([FromBody]Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return Ok(await _context.Students.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Student>>> UpdateStudent(Student request)
        {
            var dbStudent = await _context.Students.FindAsync(request.Id);
            if (dbStudent == null)
                return BadRequest("Student not found");

            dbStudent.Name = request.Name;
            dbStudent.FirstName = request.FirstName;
            dbStudent.LastName = request.LastName;
            dbStudent.Place = request.Place;

            await _context.SaveChangesAsync();

            return Ok(await _context.Students.ToListAsync());
        } 

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Student>>> Delete(int id)
        {
            var dbStudent = await _context.Students.FindAsync(id);
            
            if (dbStudent == null)
                return BadRequest("Student nout found");

            _context.Students.Remove(dbStudent);
            await _context.SaveChangesAsync();
            return Ok(await _context.Students.ToListAsync());
        }


    }
}
