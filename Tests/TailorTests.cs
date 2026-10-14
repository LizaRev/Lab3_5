using BLL.Entities;

namespace Tests;

[TestClass]
public class TailorTests
{
    [TestMethod]
    public void Constructor_CreatesTailorWithCorrectId()
    {
        int expectedId = 1;

        Tailor tailor = new Tailor(expectedId, "Anna", "Brown");

        Assert.AreEqual(expectedId, tailor.Id);
    }

    [TestMethod]
    public void Properties_ReturnCorrectValues()
    {
        Tailor tailor = new Tailor(5, "Anna", "Brown");

        int id = tailor.Id;
        string firstName = tailor.FirstName;
        string lastName = tailor.LastName;

        Assert.AreEqual(5, id);
        Assert.AreEqual("Anna", firstName);
        Assert.AreEqual("Brown", lastName);
    }
}
