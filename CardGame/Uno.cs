using System;
using System.Threading.Tasks;
using Avalonia.Controls;

namespace CardGame;

public partial class Uno : GameEngine
{
    private string _lastPlayedColor;
    private string _lastPlayedValue;
    
    public override void HandleCardClick(object sender)
    {
        if (sender is Button button && button.DataContext is UnoCard clickedCard)
        {
            if (clickedCard.Value == _lastPlayedValue || clickedCard.Color == _lastPlayedColor || _lastPlayedColor is null)
            {
                CurrentPlayer.Discard(clickedCard, DiscardDeck);
                _lastPlayedColor = clickedCard.Color; 
                _lastPlayedValue = clickedCard.Value;
                LastPlayedCard.Content = clickedCard.DisplayName;
                EndTurn();
            }
        }
    }

    public override void HandleDrawACardClick(object sender)
    { 
        CurrentPlayer.Draw(DrawDeck);
        DrawButton.Content = $"Dobierz kartę ({DrawDeck.Cards.Count} kart)";
        EndTurn();
    }


    //to jest tylko po to by działała część JZ
    public override void HandleCardConfirm(object sender) { }
    public override void HandlePlus(Object sender) { }
    public override void HandleMinus(Object sender) { }
    // koniec części jagody


    public async override void RunGame()
    {
        Console.WriteLine("Running uno");
        DrawDeck.CreateUnoDeck();
        DrawDeck.Shuffle();
        foreach (Player player in Players)
        {
            player.Draw(DrawDeck, 5);
        }
        DrawButton.Content = $"Dobierz kartę ({DrawDeck.Cards.Count} kart)";
        
        bool gameOver = false;
        while (!gameOver)
        {
            foreach (Player player in Players)
            {
                Console.WriteLine($"Talia: {DrawDeck.Cards.Count}");
                Console.WriteLine($"Odrzucone: {DiscardDeck.Cards.Count}");
                CurrentPlayer = player;
                await PlayerTurn();
                if (CurrentPlayer.Hand.Count == 0)
                {
                    EndGame(CurrentPlayer);
                    gameOver = true;
                    break;
                }

                if (DrawDeck.Cards.Count == 0)
                {
                    DrawDeck = DiscardDeck;
                    DrawDeck.Shuffle();
                }
            }   
        }
    }
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
        Console.WriteLine(DisplayName);
    }
    

    public override string DisplayName => $"{Color} {Value}";
}