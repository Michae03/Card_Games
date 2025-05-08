using System;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Media;
using HarfBuzzSharp;
using Tmds.DBus.Protocol;

namespace CardGame;

public partial class Uno : GameEngine
{
    private string _lastPlayedColor;
    private string _lastPlayedValue;

    private int direction = 1;
    private int currentIndex = 0;

    private bool _waitingForColorChoice = false;
    private Player _playerToSkip = null;
    

    //----------- DO DOBIERANIA KART -------------

    private int _pendingDrawAmount = 0;
    private bool _DrawAmountDefence = false;
    private int _turnsToSkip = 0;
    private bool _SkipTurnDefence = false;

    private void ForceNextPlayerToDraw(int cards)
    {
        _pendingDrawAmount += cards;
        _DrawAmountDefence = true;
        Console.WriteLine($"Następny gracz musi dobrać {_pendingDrawAmount} kart lub się obronić!");
    }

    // ---------- FUNKCJE KART SPECJALNYCH ----------
    private void ReversePlayersOrder()
    {
        Console.WriteLine("Kolejność graczy została odwrócona!");
        direction *= -1;
    }

    private void SkipNextPlayer(int turns)
    {
        int nextPlayerIndex = (currentIndex + direction + Players.Count) % Players.Count;
        _playerToSkip = Players[nextPlayerIndex];
        _turnsToSkip += turns;
        _SkipTurnDefence = true;
    }
    private void ChangeToAnyColor() 
    {
       ColorChangePanel.IsVisible = true;
        _waitingForColorChoice = true;
    }
    // ---------- SWITCH DO FUNKCJI KART SPECJALNYCH ----------

    private void HandleSpecialCard(UnoCard card)
    {

        int nextIndex = -1;
        
        switch (card.Value) 
        {
            case "r":
                ReversePlayersOrder();
                break;

            case "s":
                SkipNextPlayer(1);
                break;

            case "+2":
                ForceNextPlayerToDraw(2);
                break;

            case "+4":
                ForceNextPlayerToDraw(4);
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
        ColorChangePanel.IsVisible = false;
        _waitingForColorChoice = false;
        LastPlayedCardUpdate();
        EndTurn();
    }
    public override void HandleCardClick(object sender)
    {
        if (_waitingForColorChoice)
            return;

        if (sender is Button button && button.DataContext is UnoCard clickedCard)
        {
            if (_DrawAmountDefence) 
            {
                if (clickedCard.Value == "+2" || clickedCard.Value == "+4")
                {
                    CurrentPlayer.Discard(clickedCard, DiscardDeck);
                    _pendingDrawAmount += (_pendingDrawAmount == 2) ? 2 : 4;
                    _lastPlayedColor = clickedCard.Color;
                    _lastPlayedValue = clickedCard.Value;
                    LastPlayedCard.Content = clickedCard.DisplayName;

                    HandleSpecialCard(clickedCard);

                    EndTurn();
                    return;
                }
                else 
                {
                    CurrentPlayer.Draw(DrawDeck, _pendingDrawAmount);
                    _pendingDrawAmount = 0;
                    _DrawAmountDefence = false;
                    DrawButton.Content = $"Dobierz karte {DrawDeck.Cards.Count} kart";
                    EndTurn();
                    return;
                }
            }
            if (_SkipTurnDefence) 
            {
                if (clickedCard.Value == "s") 
                {
                    CurrentPlayer.Discard(clickedCard, DiscardDeck);
                    _lastPlayedColor = clickedCard.Color;
                    _lastPlayedValue = clickedCard.Value;
                    LastPlayedCard.Content = clickedCard.DisplayName;
                    HandleSpecialCard(clickedCard);
                    EndTurn();
                    return;
                }
                else
                {
                    Console.WriteLine("Nie obroniłeś się przed pominięciem tury.");
                    _SkipTurnDefence = false;
                    EndTurn();
                    return;
                }
            }

             if (clickedCard.Value == _lastPlayedValue || clickedCard.Color == _lastPlayedColor || _lastPlayedColor is null || clickedCard.Color == "Any")
                {
                    CurrentPlayer.Discard(clickedCard, DiscardDeck);
                    _lastPlayedColor = clickedCard.Color;
                    _lastPlayedValue = clickedCard.Value;
                    LastPlayedCardUpdate();

                    HandleSpecialCard(clickedCard);

                    EndTurn();
                }
            
        }
    }

    public void LastPlayedCardUpdate()
    {
        var brush = new SolidColorBrush();
        switch (_lastPlayedColor)
        {
            case ("Blue"):
                brush = new SolidColorBrush(Colors.Blue);
                break;
            case ("Red"):
                brush = new SolidColorBrush(Colors.Red);
                break;
            case ("Yellow"):
                brush = new SolidColorBrush(Colors.Yellow);
                break;
            case ("Green"):
                brush = new SolidColorBrush(Colors.Green);
                break;
            case ("Any"):
                brush = new SolidColorBrush(Colors.White);
                break;
            default:
                brush = new SolidColorBrush(Colors.Gray);
                break;
        }
        LastPlayedCard.Background = brush;
        LastPlayedCard.Content = _lastPlayedValue;
    }
    
    public override void HandleDrawACardClick(object sender)
    {
        if (_waitingForColorChoice)
            return;
        if (_DrawAmountDefence) 
        {
            CurrentPlayer.Draw(DrawDeck, _pendingDrawAmount);
            _pendingDrawAmount = 0;
            _DrawAmountDefence = false;
        }
        else
        {
            CurrentPlayer.Draw(DrawDeck);
        }
            
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

                if (_turnsToSkip > 0 && CurrentPlayer == _playerToSkip)
                {
                    if (!_SkipTurnDefence)
                    {
                        Console.WriteLine($"{CurrentPlayer.Name} zostaje pominięty (nie obronił się)!");
                        _turnsToSkip--;
                        _playerToSkip = null;
                        currentIndex += direction;
                        continue;
                    }
                    else
                    {
                        
                        Console.WriteLine($"{CurrentPlayer.Name} ma szansę się obronić przed pominięciem tury.");
                    }
                }

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
    

    public override string DisplayName => $"{Value}";
}