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
            return db.Departments.AsNoTracking().ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Department> GetDepartmentById(int id)
        {
            return db.Departments.AsNoTracking().Where(data => data.DepartmentId==id).FirstOrDefault();
        }

        [HttpPost("")]
        public ActionResult<Department> PostDepartment(Department model)
        {
            db.Departments.Add(model);

            db.SaveChanges();

            return Created($"api/department/{model.DepartmentId}",model);
        }

        [HttpPut("{id}")]
        public IActionResult PutDepartment(int id, UpdateDepartment model)
        {
            var department=db.Departments.Find(id);
            department.InjectFrom(model);

            db.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Department> DeleteDepartmentById(int id)
        {
            var department=db.Departments.Find(id);
            db.Departments.Remove(department);

            db.SaveChanges();

            return Ok();
        }
    }
}
