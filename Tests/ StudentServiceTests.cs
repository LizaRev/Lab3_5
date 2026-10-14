using BLL;
using BLL.Entities;
using BLL.Interfaces;

namespace Tests;

[TestClass]
public class StudentServiceTests
{
    [TestMethod]
    public void GetForeignExcellentFirstYearStudents_ReturnsForeignExcellentStudent()
    {
        List<Student> students = new List<Student>
        {
            CreateStudent(1, "Анна", "Коваленко", 1, 95, "USA", "P123")
        };

        StudentService service = new StudentService(new TestStudentDataProvider(students));

        List<Student> result = service.GetForeignExcellentFirstYearStudents();

        Assert.HasCount(1, result);
        Assert.AreEqual("Анна", result[0].FirstName);
    }

    [TestMethod]
    public void GetForeignExcellentFirstYearStudents_DoesNotReturnUkrainianStudent()
    {
        List<Student> students = new List<Student>
        {
            CreateStudent(1, "Ілона", "Марченко", 1, 95, "Україна", "P123")
        };

        StudentService service = new StudentService(new TestStudentDataProvider(students));

        List<Student> result = service.GetForeignExcellentFirstYearStudents();

        Assert.IsEmpty(result);
    }

    [TestMethod]
    public void GetForeignExcellentFirstYearStudents_DoesNotReturnStudentFromAnotherCourse()
    {
        List<Student> students = new List<Student>
        {
            CreateStudent(1, "Марина", "Савич", 2, 95, "USA", "P123")
        };

        StudentService service = new StudentService(new TestStudentDataProvider(students));

        List<Student> result = service.GetForeignExcellentFirstYearStudents();

        Assert.IsEmpty(result);
    }

    [TestMethod]
    public void GetForeignExcellentFirstYearStudents_DoesNotReturnStudentWithLowMark()
    {
        List<Student> students = new List<Student>
        {
            CreateStudent(1, "Марія", "Тимощук", 1, 89, "USA", "P123")
        };

        StudentService service = new StudentService(new TestStudentDataProvider(students));

        List<Student> result = service.GetForeignExcellentFirstYearStudents();

        Assert.IsEmpty(result);
    }

    [TestMethod]
    public void GetForeignExcellentFirstYearStudents_ReturnsSeveralStudents()
    {
        List<Student> students = new List<Student>
        {
            CreateStudent(1, "Анна", "Коваленко", 1, 95, "USA", "P123"),
            CreateStudent(2, "Ілона", "Марченко", 1, 90, "Poland", "P456"),
            CreateStudent(3, "Марина", "Савич", 2, 99, "USA", "P789")
        };

        StudentService service = new StudentService(new TestStudentDataProvider(students));

        List<Student> result = service.GetForeignExcellentFirstYearStudents();

        Assert.HasCount(2, result);
    }

    [TestMethod]
    public void Citizenship_ReturnsTrueForFiveYears()
    {
        StudentService service = new StudentService(
            new TestStudentDataProvider(new List<Student>()));

        bool result = service.CanObtainUkrainianCitizenship(5);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void Citizenship_ReturnsTrueForMoreThanFiveYears()
    {
        StudentService service = new StudentService(
            new TestStudentDataProvider(new List<Student>()));

        bool result = service.CanObtainUkrainianCitizenship(10);

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void Citizenship_ReturnsFalseForLessThanFiveYears()
    {
        StudentService service = new StudentService(
            new TestStudentDataProvider(new List<Student>()));

        bool result = service.CanObtainUkrainianCitizenship(4);

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void Citizenship_ThrowsExceptionForNegativeYears()
    {
        StudentService service = new StudentService(
            new TestStudentDataProvider(new List<Student>()));

        Assert.Throws<ArgumentException>(
            () => service.CanObtainUkrainianCitizenship(-1));
    }

    [TestMethod]
    public void Validation_ThrowsExceptionForNullStudents()
    {
        StudentService service = new StudentService(new NullStudentDataProvider());

        Assert.Throws<ArgumentNullException>(
            () => service.GetForeignExcellentFirstYearStudents());
    }

    [TestMethod]
    public void Validation_ThrowsExceptionForNullStudent()
    {
        List<Student> students = new List<Student>
        {
            null!
        };

        StudentService service = new StudentService(new TestStudentDataProvider(students));

        Assert.Throws<ArgumentException>(
            () => service.GetForeignExcellentFirstYearStudents());
    }

    [TestMethod]
    public void Validation_ThrowsExceptionForInvalidCourse()
    {
        List<Student> students = new List<Student>
        {
            CreateStudent(1, "Анна", "Коваленко", 0, 95, "USA", "P123")
        };

        StudentService service = new StudentService(new TestStudentDataProvider(students));

        Assert.Throws<ArgumentException>(
            () => service.GetForeignExcellentFirstYearStudents());
    }

    [TestMethod]
    public void Validation_ThrowsExceptionForMarkBelowZero()
    {
        List<Student> students = new List<Student>
        {
            CreateStudent(1, "Анна", "Коваленко", 1, -1, "USA", "P123")
        };

        StudentService service = new StudentService(new TestStudentDataProvider(students));

        Assert.Throws<ArgumentException>(
            () => service.GetForeignExcellentFirstYearStudents());
    }

    [TestMethod]
    public void Validation_ThrowsExceptionForMarkAboveOneHundred()
    {
        List<Student> students = new List<Student>
        {
            CreateStudent(1, "Анна", "Коваленко", 1, 101, "USA", "P123")
        };

        StudentService service = new StudentService(new TestStudentDataProvider(students));

        Assert.Throws<ArgumentException>(
            () => service.GetForeignExcellentFirstYearStudents());
    }

    [TestMethod]
    public void Validation_ThrowsExceptionForEmptyFirstName()
    {
        List<Student> students = new List<Student>
        {
            CreateStudent(1, "", "Коваленко", 1, 95, "USA", "P123")
        };

        StudentService service = new StudentService(new TestStudentDataProvider(students));

        Assert.Throws<ArgumentException>(
            () => service.GetForeignExcellentFirstYearStudents());
    }

    [TestMethod]
    public void Validation_ThrowsExceptionForEmptyLastName()
    {
        List<Student> students = new List<Student>
        {
            CreateStudent(1, "Анна", "", 1, 95, "USA", "P123")
        };

        StudentService service = new StudentService(new TestStudentDataProvider(students));

        Assert.Throws<ArgumentException>(
            () => service.GetForeignExcellentFirstYearStudents());
    }

    [TestMethod]
    public void Validation_ThrowsExceptionForEmptyStudentTicket()
    {
        List<Student> students = new List<Student>
        {
            new Student(1, "Анна", "Коваленко", 1, "", 95, "USA", "P123")
        };

        StudentService service = new StudentService(new TestStudentDataProvider(students));

        Assert.Throws<ArgumentException>(
            () => service.GetForeignExcellentFirstYearStudents());
    }

    [TestMethod]
    public void Validation_ThrowsExceptionForEmptyCountry()
    {
        List<Student> students = new List<Student>
        {
            new Student(1, "Анна", "Коваленко", 1, "ST123", 95, "", "P123")
        };

        StudentService service = new StudentService(new TestStudentDataProvider(students));

        Assert.Throws<ArgumentException>(
            () => service.GetForeignExcellentFirstYearStudents());
    }

    [TestMethod]
    public void Validation_ThrowsExceptionForEmptyPassportNumber()
    {
        List<Student> students = new List<Student>
        {
            new Student(1, "Анна", "Коваленко", 1, "ST123", 95, "USA", "")
        };

        StudentService service = new StudentService(new TestStudentDataProvider(students));

        Assert.Throws<ArgumentException>(
            () => service.GetForeignExcellentFirstYearStudents());
    }

    private static Student CreateStudent(
        int id,
        string firstName,
        string lastName,
        int course,
        double averageMark,
        string country,
        string passportNumber)
    {
        return new Student(
            id,
            firstName,
            lastName,
            course,
            "ST123",
            averageMark,
            country,
            passportNumber);
    }

    private class TestStudentDataProvider : IStudentDataProvider
    {
        private readonly List<Student> _students;

        public TestStudentDataProvider(List<Student> students)
        {
            _students = students;
        }

        public List<Student> GetAll()
        {
            return _students;
        }

        public void SaveAll(List<Student> students)
        {
        }
    }

    private class NullStudentDataProvider : IStudentDataProvider
    {
        public List<Student> GetAll()
        {
            return null!;
        }

        public void SaveAll(List<Student> students)
        {
        }
    }
}
