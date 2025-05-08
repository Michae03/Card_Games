using System;
using Avalonia.Controls;

namespace CardGame;

public partial class ExplodingKittens : GameEngine
{
    public override void HandleCardClick(object sender)
    {
        if (sender is Button button && button.DataContext is Card clickedCard)
        {
            //TUTAJ MIEJSCE NA LOGIKE KLIKNIĘCIA KARTY
            //clickedCard = kliknięta karta
        }
    }


    //to jest tylko po to by działała część JZ
    public override void HandleCardConfirm(object sender) { }
    public override void HandlePlus(Object sender) { }
    public override void HandleMinus(Object sender) { }
    // koniec części jagody

    public override void HandleDrawACardClick(object sender)
    { 
        
    }
    public override void RunGame()
    {
        Console.WriteLine("Running exploding kittens");
    }
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