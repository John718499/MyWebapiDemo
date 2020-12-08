using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebapiDemo.Models;

namespace MyWebapiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly ContosoUniversityContext db;
        public EnrollmentController(ContosoUniversityContext db)
        {
            this.db = db;
        }

        [HttpGet("course/{id}")]
        public ActionResult<List<Enrollment>> GetEnrollmentByCourseId(int id)
        {
            return db.Enrollments.AsNoTracking().Where(data =>data.CourseId==id).ToList();
        }

        [HttpGet("person/{id}")]
        public ActionResult<List<Enrollment>> GetEnrollmentByPersonId(int id)
        {
            return db.Enrollments.AsNoTracking().Where(data =>data.StudentId==id).ToList();
        }

        [HttpPost("")]
        public ActionResult<Enrollment> PostEnrollment(Enrollment model)
        {
            return null;
        }

        [HttpPut("{id}")]
        public IActionResult PutEnrollment(int id, Enrollment model)
        {
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<Enrollment> DeleteEnrollmentById(int id)
        {
            return null;
        }
    }
}