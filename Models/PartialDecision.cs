namespace ChooseDelice.Models;

public partial class PartialDecision
{
    public int Id { get; set; }

    public string Conclusion { get; set; } = null!;

    public int IntLactoza { get; set; }

    public int IntGluten { get; set; }

    public int IntOua { get; set; }

    public int Vegan { get; set; }
}
