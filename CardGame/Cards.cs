using System;

public abstract partial class Card
{
    public abstract void Play();
    public abstract string DisplayName { get; }
}

public class UnoCard : Card
{
    public string Color { get; set; }
    public string Value { get; set; }

    public UnoCard(string color, string value)
    {
        Color = color;
        Value = value;
    }

    public override void Play()
    {
        throw new NotImplementedException();
    }

    public override string DisplayName => $"{Color} {Value}";
}

public class ExplodingKittenCard : Card
{
    public string Effect { get; set; }

    public ExplodingKittenCard(string effect)
    {
        Effect = effect;
    }

    public override void Play()
    {
        throw new NotImplementedException();
    }

    public override string DisplayName => $"Exploding Kitten: {Effect}";
}

public class StandardCard : Card
{
    public string Suit { get; set; }  
    public string Rank { get; set; }  

    public StandardCard(string suit, string rank)
    {
        Suit = suit;
        Rank = rank;
    }

    public override void Play()
    {
        throw new NotImplementedException();
    }

    public override string DisplayName => $"{Rank} of {Suit}";
}