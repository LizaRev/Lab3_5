using BLL.Entities;

namespace Tests;

[TestClass]
public class StudentTests
{
    [TestMethod]
    public void Dive_SetsIsDivingToTrue()
    {
        Student student = new Student(1, "Анна", "Коваленко", 1, "ST123", 95, "USA", "P123");

        student.Dive();

        Assert.IsTrue(student.IsDiving);
    }

    [TestMethod]
    public void Properties_ReturnCorrectValues()
    {
        Student student = new Student(1, "Анна", "Коваленко", 1, "ST123", 95, "USA", "P123");

        int id = student.Id;
        string firstName = student.FirstName;
        string lastName = student.LastName;
        int course = student.Course;
        string ticket = student.StudentTicket;
        double mark = student.AverageMark;
        string country = student.Country;
        string passport = student.PassportNumber;

        Assert.AreEqual(1, id);
        Assert.AreEqual("Анна", firstName);
        Assert.AreEqual("Коваленко", lastName);
        Assert.AreEqual(1, course);
        Assert.AreEqual("ST123", ticket);
        Assert.AreEqual(95, mark);
        Assert.AreEqual("USA", country);
        Assert.AreEqual("P123", passport);
    }
}
