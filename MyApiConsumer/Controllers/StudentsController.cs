using Microsoft.AspNetCore.Mvc;
using MyApiConsumer.Models;
using System.Threading.Tasks;

public class StudentsController : Controller
{
    private readonly ApiService _apiService;

    public StudentsController(ApiService apiService)
    {
        _apiService = apiService;
    }

    // GET: Students
    public async Task<IActionResult> Index()
    {
        var students = await _apiService.GetStudentsAsync();
        return View(students);
    }

    // GET: Students/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var student = await _apiService.GetStudentAsync(id);
        if (student == null)
        {
            return NotFound();
        }
        return View(student);
    }

    // GET: Students/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Students/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Student student)
    {
        if (ModelState.IsValid)
        {
            var isSuccess = await _apiService.AddStudentAsync(student);
            if (isSuccess)
                return RedirectToAction(nameof(Index));
        }
        return View(student);
    }

    // GET: Students/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var student = await _apiService.GetStudentAsync(id);
        if (student == null)
        {
            return NotFound();
        }
        return View(student);
    }

    // POST: Students/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Student student)
    {
        if (id != student.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var isSuccess = await _apiService.UpdateStudentAsync(id, student);
            if (isSuccess)
                return RedirectToAction(nameof(Index));
        }
        return View(student);
    }

    // GET: Students/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var student = await _apiService.GetStudentAsync(id);
        if (student == null)
        {
            return NotFound();
        }
        return View(student);
    }

    // POST: Students/Delete/5
    // POST: Students/DeleteConfirmed
    [HttpPost, ActionName("DeleteConfirmed")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        // Check if the student exists before attempting to delete
        var student = await _apiService.GetStudentAsync(id);
        if (student == null)
        {
            return NotFound(); // Return 404 if the student is not found
        }

        // Attempt to delete the student
        var isSuccess = await _apiService.DeleteStudentAsync(id);
        if (isSuccess)
        {
            // Redirect to the Index action if deletion is successful
            return RedirectToAction(nameof(Index));
        }

        // If deletion failed, add an error to the model state
        ModelState.AddModelError("", "Unable to delete student. Please try again.");

        // Return to the same view with the error message
        return View(student);
    }


}
