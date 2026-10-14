using System.Text.Json;
using BLL.Entities;
using BLL.Interfaces;

namespace DAL;

public class StudentDataProvider : IStudentDataProvider
{
    private readonly string _filePath;

    public StudentDataProvider(string filePath)
    {
        _filePath = filePath;
    }

    public List<Student> GetAll()
    {
        if (!File.Exists(_filePath))
        {
            return new List<Student>();
        }

        string json = File.ReadAllText(_filePath);

        if (string.IsNullOrWhiteSpace(json))
        {
            return new List<Student>();
        }

        return JsonSerializer.Deserialize<List<Student>>(json)
               ?? new List<Student>();
    }

    public void SaveAll(List<Student> students)
    {
        string json = JsonSerializer.Serialize(
            students,
            new JsonSerializerOptions
            {
                WriteIndented = true
            });

        File.WriteAllText(_filePath, json);
    }
}