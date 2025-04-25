using System;
using Avalonia.Controls;

namespace CardGame;

public partial class Member : GameEngine
{
    public override void HandleCardClick(object sender)
    {
        if (sender is Button button && button.DataContext is Card clickedCard)
        {
            //TUTAJ MIEJSCE NA LOGIKE KLIKNIĘCIA KARTY
            //clickedCard = kliknięta karta
        }
    }
    public override void HandleDrawACardClick(object sender)
    { 
     
    }
    public override void RunGame()
    {
        Console.WriteLine("Running member");
    }
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