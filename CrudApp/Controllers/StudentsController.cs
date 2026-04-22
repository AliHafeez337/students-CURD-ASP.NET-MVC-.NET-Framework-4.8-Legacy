using CrudApp.Models;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace CrudApp.Controllers
{
    public class StudentsController : Controller
    {
        // This is our "door" to the database
        // We created this class in Step 5
        private ApplicationDbContext db = new ApplicationDbContext();

        // =====================================================================
        // READ - Index Action
        // URL: /Students
        // This action fetches ALL students from the DB and sends to the view
        // =====================================================================
        public ActionResult Index()
        {
            // db.Students fetches all rows from the Students table
            // .ToList() converts the result into a C# List
            var students = db.Students.ToList();

            // View() renders the Index.cshtml file
            // We pass the students list to the view so it can display them
            return View(students);
        }

        // =====================================================================
        // CREATE (GET) - Show the empty form
        // URL: /Students/Create (when you visit the page)
        // [HttpGet] means this runs when the user VISITS the page
        // =====================================================================
        [HttpGet]
        public ActionResult Create()
        {
            // Just show the empty form — no data needed
            return View();
        }

        // =====================================================================
        // CREATE (POST) - Actually save the new student
        // URL: /Students/Create (when you SUBMIT the form)
        // [HttpPost] means this runs when the user SUBMITS the form
        // =====================================================================
        [HttpPost]
        public ActionResult Create(Student student)
        {
            // Step 1: Check for duplicate email FIRST
            bool emailExists = db.Students.Any(s => s.Email == student.Email);

            if (emailExists)
            {
                // Step 2: Add error — this makes ModelState.IsValid = false
                ModelState.AddModelError("Email", "A student with this email already exists.");
            }

            // Step 3: Only save if ModelState is valid
            // If emailExists was true, this block will be SKIPPED entirely
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // Step 4: Return form with error messages
            return View(student);
        }

        // =====================================================================
        // EDIT (GET) - Show the form pre-filled with existing student data
        // URL: /Students/Edit/1 (where 1 is the student Id)
        // =====================================================================
        [HttpGet]
        public ActionResult Edit(int id)
        {
            // Find the student in the DB by their Id
            var student = db.Students.Find(id);

            // If no student found with that Id, return 404
            if (student == null)
            {
                return HttpNotFound();
            }

            // Send the student data to the view to pre-fill the form
            return View(student);
        }

        // =====================================================================
        // EDIT (POST) - Save the updated student data
        // URL: /Students/Edit/1 (when you SUBMIT the edit form)
        // =====================================================================
        [HttpPost]
        public ActionResult Edit(Student student)
        {
            // Check if another DIFFERENT student already has this email
            // We exclude the current student's own Id from the check
            bool emailExists = db.Students.Any(s => s.Email == student.Email && s.Id != student.Id);

            if (emailExists)
            {
                ModelState.AddModelError("Email", "A student with this email already exists.");
            }

            if (ModelState.IsValid)
            {
                // Modified tells EF that this student object has been changed
                // EF will run an UPDATE SQL command on SaveChanges()
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(student);
        }

        // =====================================================================
        // DELETE (GET) - Show confirmation page before deleting
        // URL: /Students/Delete/1
        // =====================================================================
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var student = db.Students.Find(id);

            if (student == null)
            {
                return HttpNotFound();
            }

            // Show the student details and ask "Are you sure?"
            return View(student);
        }

        // =====================================================================
        // DELETE (POST) - Actually delete the student
        // URL: /Students/Delete/1 (when you confirm deletion)
        // =====================================================================
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var student = db.Students.Find(id);

            // Remove the student from the Students table
            db.Students.Remove(student);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // =====================================================================
        // DISPOSE - Clean up the database connection when done
        // This is called automatically by MVC when the request finishes
        // =====================================================================
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}