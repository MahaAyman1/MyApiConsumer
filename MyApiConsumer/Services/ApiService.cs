
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;
using MyApiConsumer.Models;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // READ: Get all students
    public async Task<List<Student>> GetStudentsAsync()
    {
        var response = await _httpClient.GetAsync("Students");
        if (response.IsSuccessStatusCode)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Student>>(jsonString);
        }
        return new List<Student>();
    }

    // READ: Get a single student by ID
    public async Task<Student> GetStudentAsync(int id)
    {
        var response = await _httpClient.GetAsync($"Students/{id}");
        if (response.IsSuccessStatusCode)
        {
            var jsonString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Student>(jsonString);
        }
        return null;
    }

    // CREATE: Add a new student
    public async Task<bool> AddStudentAsync(Student student)
    {
        var jsonContent = JsonConvert.SerializeObject(student);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync("Students", content);
        return response.IsSuccessStatusCode;
    }

    // UPDATE: Update an existing student
    public async Task<bool> UpdateStudentAsync(int id, Student student)
    {
        var jsonContent = JsonConvert.SerializeObject(student);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var response = await _httpClient.PutAsync($"Students/{id}", content);
        return response.IsSuccessStatusCode;
    }

    // DELETE: Delete a student by ID
    public async Task<bool> DeleteStudentAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"Students/{id}");
        return response.IsSuccessStatusCode;
    }
}
