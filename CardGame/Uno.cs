using System;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using HarfBuzzSharp;
using Tmds.DBus.Protocol;

namespace CardGame;

public partial class Uno : GameEngine
{
    private string _lastPlayedColor;
    private string _lastPlayedValue;

    private int direction = 1;
    private int currentIndex = 0;

    // ---------- FUNKCJE KART SPECJALNYCH ----------
    private void ReversePlayersOrder()
    {
        Console.WriteLine("Kolejność graczy została odwrócona!");
        direction *= -1;
    }

    private void SkipNextPlayer()
    {
     
        Console.WriteLine("Następny gracz zostaje pominięty!");
        
    }

    private void ForceNextPlayerToDraw(int cards)
    {
        Console.WriteLine($"Dobiera {cards} kart!");
    }

    private void ChangeToAnyColor() 
    {
       ColorChangePanel.IsVisible = true;
    }
    // ---------- SWITCH DO FUNKCJI KART SPECJALNYCH ----------

    private void HandleSpecialCard(UnoCard card)
    {
        switch (card.Value) 
        {
            case "r":
                ReversePlayersOrder();
                break;

            case "s":
                
                break;

            case "+2":

                break;

            case "+4":

                break;

            case "c":

                break;

            case "p":
                ChangeToAnyColor();

                break;



        }
    }

    public void HandleColorButton_click(string Tag) 
    {
        _lastPlayedColor = Tag;
        LastPlayedCard.Content = Tag;
    }
    public override void HandleCardClick(object sender)
    {
        if (sender is Button button && button.DataContext is UnoCard clickedCard)
        {
            
             if (clickedCard.Value == _lastPlayedValue || clickedCard.Color == _lastPlayedColor || _lastPlayedColor is null || clickedCard.Color == "Any")
                {
                    CurrentPlayer.Discard(clickedCard, DiscardDeck);
                    _lastPlayedColor = clickedCard.Color;
                    _lastPlayedValue = clickedCard.Value;
                    LastPlayedCard.Content = clickedCard.DisplayName;

                    HandleSpecialCard(clickedCard);

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
            while (currentIndex >= 0 && currentIndex < Players.Count)
            {
                CurrentPlayer = Players[currentIndex];
                Console.WriteLine($"Talia: {DrawDeck.Cards.Count}");
                Console.WriteLine($"Odrzucone: {DiscardDeck.Cards.Count}");

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

                currentIndex += direction;
            }

            // Jak wyjdzie poza zakres, wracamy
            if (!gameOver)
            {
                if (currentIndex < 0)
                    currentIndex = Players.Count - 1;
                else if (currentIndex >= Players.Count)
                    currentIndex = 0;
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