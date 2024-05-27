using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortail.Web.Data;
using StudentPortail.Web.Models;
using StudentPortail.Web.Models.Entities;

namespace StudentPortail.Web.Controllers
{
    public class StudentsController : Controller
    {
        private readonly AppDbContext dbContext;

        public StudentsController(AppDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel viewModel)
        {
            var student = new Student
            {
                Name = viewModel.Name,
                Email = viewModel.Email,
                Phone = viewModel.Phone,
                Suscribed = viewModel.Suscribed
            };

            await dbContext.Students.AddAsync(student);
            await dbContext.SaveChangesAsync();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var students = await dbContext.Students.ToListAsync();

            return View(students);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var student = await dbContext.Students.FindAsync(id);

            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Edit
            (Student viewModdel)
        {
            var student = await dbContext.Students.FindAsync(viewModdel.Id);

            if (student is not null)
            {
                student.Name = viewModdel.Name;
                student.Email = viewModdel.Email;
                student.Phone = viewModdel.Phone;
                student.Suscribed = viewModdel.Suscribed;

                await dbContext.SaveChangesAsync();

            }

            return RedirectToAction("List", "Students");
        }

        [HttpPost]
        public async Task<IActionResult> Deete(Student viewModel)
        {
            var student = await dbContext.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == viewModel.Id);
        if (student is not null)
            {
                dbContext.Students.Remove(viewModel);
                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Students");
        }

    }
}
