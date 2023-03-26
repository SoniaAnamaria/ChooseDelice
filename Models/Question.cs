namespace ChooseDelice.Models;

public partial class Question
{
    public int Id { get; set; }

    public string Text { get; set; } = null!;

    public string Attribute { get; set; } = null!;

    public bool IsChecked { get; set; }
}
