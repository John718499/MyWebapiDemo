using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWebapiDemo.Models;
using Omu.ValueInjecter;

namespace MyWebapiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ContosoUniversityContext db;
        public CourseController(ContosoUniversityContext db)
        {
            this.db = db;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<Course>> GetCourses()
        {
            return db.Courses.AsNoTracking().Where(data => data.IsDeleted==0).ToList();
        }

        [HttpGet("courseStudent/{id}")]
        public ActionResult<IEnumerable<VwCourseStudent>> getCourseStudentsById(int id)
        {
            var courseStudents=db.VwCourseStudents.AsNoTracking().Where(data =>data.CourseId==id).ToList();
            return courseStudents;
        }

        [HttpGet("courseStudentCount/{id}")]
        public ActionResult<VwCourseStudentCount> getCourseStudentCountById(int id)
        {
            var courseStudentCount=db.VwCourseStudentCounts.AsNoTracking().Where(data =>data.CourseId==id).FirstOrDefault();
            return courseStudentCount;
        }

        [HttpGet("{id}")]
        public ActionResult<Course> GetCourseById(int id)
        {
            // return db.Courses.AsNoTracking().Where(data => data.CourseId==id && data.IsDeleted==0).FirstOrDefault();
            return db.Courses.AsNoTracking().Where(data => data.CourseId==id &&  data.IsDeleted==0).FirstOrDefault();
        }

        [HttpPost("")]
        public ActionResult<Course> PostCourse(Course model)
        {
            db.Courses.Add(model);

            db.SaveChanges();

            return Created($"api/course/{model.CourseId}",model);
        }

        [HttpPut("{id}")]
        public IActionResult PutCourse(int id, UpdateCourse model)
        {
            var course=db.Courses.Find(id);

            course.InjectFrom(model);

            db.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Course> DeleteCourseById(int id)
        {
            var remove_course=db.Courses.Find(id);
            db.Courses.Remove(remove_course);
            db.SaveChanges();
            return Ok();
        }
    }
}
