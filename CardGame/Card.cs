using System;
using System.Runtime.InteropServices.JavaScript;

namespace CardGame;
public abstract partial class Card
{
    public abstract void Play();
    public abstract string DisplayName { get; }
}
