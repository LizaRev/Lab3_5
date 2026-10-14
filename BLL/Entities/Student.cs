using System.Text.Json.Serialization;
using BLL.Interfaces;

namespace BLL.Entities;

public class Student : Person, IDiver
{
    public int Course { get; }
    public string StudentTicket { get; }
    public double AverageMark { get; }
    public string Country { get; }
    public string PassportNumber { get; }

    public bool IsDiving { get; private set; }

    [JsonConstructor]
    public Student(
        int id,
        string firstName,
        string lastName,
        int course,
        string studentTicket,
        double averageMark,
        string country,
        string passportNumber)
        : base(id, firstName, lastName)
    {
        Course = course;
        StudentTicket = studentTicket;
        AverageMark = averageMark;
        Country = country;
        PassportNumber = passportNumber;
    }

    public void Dive()
    {
        IsDiving = true;
    }
}