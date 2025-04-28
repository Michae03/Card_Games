using System;
using System.Runtime.InteropServices.JavaScript;
using Avalonia.Media;

namespace CardGame;
public abstract partial class Card
{
    public IBrush UIColor { get; set; }

    public Card()
    {
        UIColor = Brushes.Gray;
    }
    public abstract void Play();
    public abstract string DisplayName { get; }
}
