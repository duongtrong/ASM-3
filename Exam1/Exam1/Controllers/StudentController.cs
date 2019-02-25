using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exam1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Exam1.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentContext _context;

        public StudentController(StudentContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return new JsonResult(_context.Students.ToList());
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
            return Redirect("/Student/Index");
        }

        //Edit Student
        [HttpGet("{id}")]
        [Route("Student/Edit/{id}")]
        public IActionResult Edit(long id)
        {
            var student = _context.Students.Find(id);
            if (student == null)
            {
                return NotFound();
            }

            ViewData["id"] = student.Id;
            ViewData["rollNumber"] = student.RollNumber;
            ViewData["name"] = student.Name;
            return View();
        }

        // Update Student
        [HttpPost]
        [Route("Student/Update/{id}")]
        public IActionResult Update(long id, Student student)
        {
            var updateStudent = _context.Students.Find(id);
            updateStudent = student;
            _context.Students.Update(updateStudent);
            return Redirect("/Student/Index");
        }

        //Delete Student
        [HttpDelete("{id}")]
        [Route("Student/Delete/{id}")]
        public IActionResult Delete(long id)
        {
            var deleteStudent = _context.Students.Find(id);
            if (deleteStudent == null)
            {
                return NotFound();
            }
            _context.Students.Remove(deleteStudent);
            _context.SaveChanges();
            return Redirect("/Student/Index");
        }
    }
}