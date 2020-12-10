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
            db.Enrollments.Add(model);

            db.SaveChanges();

            return Created($"api/person/{model.StudentId}",model);
        }

        [HttpPut("")]
        public IActionResult PutEnrollment(Enrollment model)
        {
            var enrollment=db.Enrollments.Where(data => data.CourseId==model.CourseId && data.StudentId==model.StudentId).FirstOrDefault();

            enrollment.Grade=model.Grade;

            db.Enrollments.Update(enrollment);

            db.SaveChanges();

            return Ok();
        }

        [HttpDelete("")]
        public ActionResult<Enrollment> DeleteEnrollmentById(Enrollment model)
        {
            var enrollment=db.Enrollments.Where(data => data.CourseId==model.CourseId && data.StudentId==model.StudentId).FirstOrDefault();

            db.Enrollments.Remove(enrollment);

            db.SaveChanges();

            return Ok();
        }
    }
}