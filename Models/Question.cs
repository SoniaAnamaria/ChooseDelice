using System;
using System.Collections.Generic;

namespace ChooseDelice.Models;

public partial class Question
{
    public int Id { get; set; }

    public string Text { get; set; } = null!;
}
