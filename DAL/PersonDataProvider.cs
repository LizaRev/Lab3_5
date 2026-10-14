using System.Text.Json;
using BLL.Entities;

namespace DAL;

public class PersonDataProvider
{
    private readonly string _filePath;

    public PersonDataProvider(string filePath)
    {
        _filePath = filePath;
    }

    public List<Person> GetAll()
    {
        if (!File.Exists(_filePath))
        {
            return new List<Person>();
        }

        string json = File.ReadAllText(_filePath);

        if (string.IsNullOrWhiteSpace(json))
        {
            return new List<Person>();
        }

        List<PersonData> data =
            JsonSerializer.Deserialize<List<PersonData>>(json)
            ?? new List<PersonData>();

        List<Person> people = new List<Person>();

        foreach (PersonData item in data)
        {
            if (item.Type == "Student")
            {
                people.Add(new Student(
                    item.Id,
                    item.FirstName,
                    item.LastName,
                    item.Course,
                    item.StudentTicket,
                    item.AverageMark,
                    item.Country,
                    item.PassportNumber));
            }
            else if (item.Type == "Tailor")
            {
                people.Add(new Tailor(
                    item.Id,
                    item.FirstName,
                    item.LastName));
            }
            else if (item.Type == "Singer")
            {
                people.Add(new Singer(
                    item.Id,
                    item.FirstName,
                    item.LastName,
                    item.Instrument));
            }
        }

        return people;
    }

    public void SaveAll(List<Person> people)
    {
        List<PersonData> data = new List<PersonData>();

        foreach (Person person in people)
        {
            if (person is Student student)
            {
                data.Add(new PersonData
                {
                    Type = "Student",
                    Id = student.Id,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Course = student.Course,
                    StudentTicket = student.StudentTicket,
                    AverageMark = student.AverageMark,
                    Country = student.Country,
                    PassportNumber = student.PassportNumber
                });
            }
            else if (person is Tailor tailor)
            {
                data.Add(new PersonData
                {
                    Type = "Tailor",
                    Id = tailor.Id,
                    FirstName = tailor.FirstName,
                    LastName = tailor.LastName
                });
            }
            else if (person is Singer singer)
            {
                data.Add(new PersonData
                {
                    Type = "Singer",
                    Id = singer.Id,
                    FirstName = singer.FirstName,
                    LastName = singer.LastName,
                    Instrument = singer.Instrument
                });
            }
        }

        string json = JsonSerializer.Serialize(
            data,
            new JsonSerializerOptions
            {
                WriteIndented = true
            });

        File.WriteAllText(_filePath, json);
    }

    private class PersonData
    {
        public string Type { get; set; } = "";
        public int Id { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public int Course { get; set; }
        public string StudentTicket { get; set; } = "";
        public double AverageMark { get; set; }
        public string Country { get; set; } = "";
        public string PassportNumber { get; set; } = "";
        public string Instrument { get; set; } = "";
    }
}