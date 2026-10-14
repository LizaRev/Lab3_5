using BLL.Entities;
using BLL.Interfaces;

namespace BLL;

public class StudentService
{
    private readonly IStudentDataProvider _dataProvider;

    public StudentService(IStudentDataProvider dataProvider)
    {
        _dataProvider = dataProvider;
    }

    public List<Student> GetForeignExcellentFirstYearStudents()
    {
        List<Student> students = _dataProvider.GetAll();

        ValidateStudents(students);

        List<Student> result = new List<Student>();

        foreach (Student student in students)
        {
            if (student.Course == 1 &&
                student.Country != "Україна" &&
                student.AverageMark >= 90)
            {
                result.Add(student);
            }
        }

        return result;
    }

    public bool CanObtainUkrainianCitizenship(int yearsOfResidence)
    {
        if (yearsOfResidence < 0)
        {
            throw new ArgumentException(
                "Кількість років проживання не може бути від'ємною.");
        }

        return yearsOfResidence >= 5;
    }

    private void ValidateStudents(List<Student> students)
    {
        if (students == null)
        {
            throw new ArgumentNullException(nameof(students));
        }

        foreach (Student student in students)
        {
            if (student == null)
            {
                throw new ArgumentException(
                    "Список не може містити порожнього студента.");
            }

            if (student.Course <= 0)
            {
                throw new ArgumentException(
                    "Курс студента має бути більше 0.");
            }

            if (student.AverageMark < 0 ||
                student.AverageMark > 100)
            {
                throw new ArgumentException(
                    "Середній бал має бути від 0 до 100.");
            }

            if (string.IsNullOrWhiteSpace(student.FirstName) ||
                string.IsNullOrWhiteSpace(student.LastName) ||
                string.IsNullOrWhiteSpace(student.StudentTicket) ||
                string.IsNullOrWhiteSpace(student.Country) ||
                string.IsNullOrWhiteSpace(student.PassportNumber))
            {
                throw new ArgumentException(
                    "Дані студента не можуть бути порожніми.");
            }
        }
    }
}