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
    public class PersonController : ControllerBase
    {
        private readonly ContosoUniversityContext db;
        public PersonController(ContosoUniversityContext db)
        {
            this.db = db;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<Person>> GetPersons()
        {
            return db.People.AsNoTracking().Where(data => data.IsDeleted==0).ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Person> GetPersonById(int id)
        {
            return db.People.AsNoTracking().Where(data => data.Id==id && data.IsDeleted==0).FirstOrDefault();
        }

        [HttpPost("{id}")]
        public ActionResult<Person> PostPerson(Person model)
        {
            db.People.Add(model);

            db.SaveChanges();

            return Created($"api/department/{model.Id}",model);
        }

        [HttpPut("{id}")]
        public IActionResult PutPerson(int id, UpdatePerson model)
        {
            var people=db.People.Find(id);
            people.InjectFrom(model);

            db.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Person> DeletePersonById(int id)
        {
            var people=db.People.Find(id);
            db.People.Remove(people);

            db.SaveChanges();

            return Ok();
        }
    }
}