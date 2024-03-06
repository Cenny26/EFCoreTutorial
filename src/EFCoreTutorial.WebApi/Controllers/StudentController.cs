using EFCoreTutorial.Data.Context;
using EFCoreTutorial.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace EFCoreTutorial.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public StudentController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var students = await dbContext.Students.AsNoTracking().ToListAsync();

            return Ok(students);
        }

        [HttpPost]
        public async Task<IActionResult> Add()
        {
            Student student = new Student()
            {
                FirstName = "Kanan",
                LastName = "Huseynov",
                BirthDate = DateTime.Parse("06/26/2004"),
                Number = 1,
                Address = new StudentAddress()
                {
                    City = "Baku",
                    District = "Sabunchu",
                    Country = "Azerbaijan",
                    FullAddress = "Ramana town, Sabunchu region, Baku city"
                }
            };

            await dbContext.Students.AddAsync(student);
            await dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await dbContext.Students.FirstOrDefaultAsync(x => x.Id == id);

            dbContext.Students.Remove(student);
            await dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id)
        {
            var student = await dbContext.Students.FirstOrDefaultAsync(x => x.Id == id);
            student.Address.FullAddress = "Azerbaijan, Baku city, Sabunchu region, Ramana town";

            await dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
