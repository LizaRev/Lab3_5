using BLL.Entities;

namespace Tests;

[TestClass]
public class SingerTests
{
    [TestMethod]
    public void StudyInstrument_SetsIsStudyingToTrue()
    {
        Singer singer = new Singer(1, "Anna", "Brown", "Piano");

        singer.StudyInstrument();

        Assert.IsTrue(singer.IsStudying);
    }

    [TestMethod]
    public void Instrument_ReturnsCorrectValue()
    {
        Singer singer = new Singer(1, "Anna", "Brown", "Piano");

        string instrument = singer.Instrument;

        Assert.AreEqual("Piano", instrument);
    }

    [TestMethod]
    public void Properties_ReturnCorrectValues()
    {
        Singer singer = new Singer(10, "Anna", "Brown", "Guitar");

        int id = singer.Id;
        string firstName = singer.FirstName;
        string lastName = singer.LastName;

        Assert.AreEqual(10, id);
        Assert.AreEqual("Anna", firstName);
        Assert.AreEqual("Brown", lastName);
        Assert.AreEqual("Guitar", singer.Instrument);
    }
}
