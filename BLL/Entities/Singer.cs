using BLL.Interfaces;

namespace BLL.Entities;

public class Singer : Person, IInstrumentLearner
{
    public string Instrument { get; }
    public bool IsStudying { get; private set; }

    public Singer(
        int id,
        string firstName,
        string lastName,
        string instrument)
        : base(id, firstName, lastName)
    {
        Instrument = instrument;
    }

    public void StudyInstrument()
    {
        IsStudying = true;
    }
}