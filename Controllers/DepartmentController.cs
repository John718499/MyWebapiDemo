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
    public class DepartmentController : ControllerBase
    {
        private readonly ContosoUniversityContext db;
        public DepartmentController(ContosoUniversityContext db)
        {
            this.db = db;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<Department>> GetDepartments()
        {
            return db.Departments.AsNoTracking().Where(data => data.IsDeleted==0).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Department> GetDepartmentById(int id)
        {
            return db.Departments.AsNoTracking().Where(data => data.DepartmentId==id &&  data.IsDeleted==0).FirstOrDefault();
        }

        [HttpPost("")]
        public ActionResult<Department> PostDepartment(Department model)
        {
            db.Departments.FromSqlRaw($"dbo.Department_Insert @Name = {model.Name}, @Budget = {model.Budget} ,@StartDate ={model.StartDate} , @InstructorID ={model.InstructorId}");

            db.SaveChanges();

            return Created($"api/department/{model.DepartmentId}",model);
        }

        [HttpPut("{id}")]
        public IActionResult PutDepartment(int id, UpdateDepartment model)
        {
            // var department=db.Departments.Find(id);
            // department.InjectFrom(model);
            db.Departments.FromSqlRaw($"dbo.Department_Update @DepartmentID={id}, @Name = {model.Name}");

            db.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Department> DeleteDepartmentById(int id)
        {
            // var department=db.Departments.Find(id);
            // db.Departments.Remove(department);
            db.Departments.FromSqlRaw($"dbo.Department_Delete @DepartmentID = {id}");

            db.SaveChanges();

            return Ok();
        }

        [HttpGet("departmentCourseCount/{id}")]
        public ActionResult<VwDepartmentCourseCount> getDepartmentCourseCountById(int id)
        {
            var courseStudentCount=db.VwDepartmentCourseCounts.FromSqlRaw($"select * from vwDepartmentCourseCount where DepartmentID ={id}").FirstOrDefault();
            return courseStudentCount;
        }
    }
}
