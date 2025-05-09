using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using HarfBuzzSharp;
using Tmds.DBus.Protocol;

namespace CardGame;

public partial class Makao : GameEngine
{
    private string _lastPlayedColor;
    private string _lastPlayedValue;

    private int direction = 1;
    private int currentIndex = 0;

    private bool _waitingForColorChoice = false;
    private Player _playerToSkip = null;

    private Player _demandingPlayer = null;
    private HashSet<Player> _playersWhoRespondToDemand = new();

    //----------- DO DOBIERANIA KART -------------

    private int _pendingDrawAmount = 0;
    private bool _DrawAmountDefence = false;
    private int _turnsToSkip = 0;
    private bool _SkipTurnDefence = false;

    private bool _DemandingColor = false;
    private bool _DemandingValue = false;

    private void ForceNextPlayerToDraw(int cards)
    {
        _pendingDrawAmount += cards;
        _DrawAmountDefence = true;
        Console.WriteLine($"Następny gracz musi dobrać {_pendingDrawAmount} kart lub się obronić!");
    }

    // ---------- FUNKCJE KART SPECJALNYCH ----------
   
    private void SkipNextPlayer(int turns)
    {
        int nextPlayerIndex = (currentIndex + direction + Players.Count) % Players.Count;
        _playerToSkip = Players[nextPlayerIndex];
        _turnsToSkip += turns;
        _SkipTurnDefence = true;

    }
    // ---------- SWITCH DO FUNKCJI KART SPECJALNYCH ----------

    private void HandleSpecialCard(MakaoCard card)
    {

        int nextIndex = -1;

        switch (card.Value)
        {
            
            case "4":
                SkipNextPlayer(1);
                break;

            case "2":
                ForceNextPlayerToDraw(2);
                break;

            case "3":
                ForceNextPlayerToDraw(3);
                break;
            
            // ----------------------- PRZETESTOWAC --------------------------------
            case "Król":
                if (_DrawAmountDefence)
                {
                    _pendingDrawAmount += 5;
                }
                else
                {
                    ForceNextPlayerToDraw(5);
                }
                break;

            case "As":
                ValueDemanding();
                break;

            case "Walet":
                ColorDemanding();
                break;
        }
    }

    private void ColorDemanding() 
    {
       MakaoColorPanel.IsVisible = true;
       _DemandingColor = true;
       _playersWhoRespondToDemand.Clear();
    }

    private void ValueDemanding() 
    { 
        MakaoValuePanel.IsVisible = true;
        _DemandingValue = true;
        _playersWhoRespondToDemand.Clear();
    }

    public void MakaoHandleColorButton_click(string Tag)
    {
        if (!_waitingForColorChoice || CurrentPlayer != _demandingPlayer)
            return;

        _lastPlayedColor = Tag;
        LastPlayedCard.Content = Tag;
        _waitingForColorChoice = false;
        MakaoColorPanel.IsVisible = false;
        _DemandingColor = true;
        EndTurn();
    }

    public void MakaoHandleValueButton_click(string Tag) 
    {
        Debug.WriteLine("Value");
        Debug.WriteLine(Tag);
        if (!_waitingForColorChoice || CurrentPlayer != _demandingPlayer)
            return;

        _lastPlayedColor = "Any";
        _lastPlayedValue = Tag;
        LastPlayedCard.Content = Tag;
        _waitingForColorChoice = false;
        MakaoValuePanel.IsVisible = false;
        _DemandingValue = true;
        EndTurn();

    }
    public override void HandleCardClick(object sender)
    {
        if (_waitingForColorChoice)
            return;
        if (_DemandingColor)
        {
            if (sender is Button button && button.DataContext is MakaoCard clickedCard)
            {
                if (clickedCard.Color == _lastPlayedColor)
                {
                    CurrentPlayer.Discard(clickedCard, DiscardDeck);
                    _lastPlayedValue = clickedCard.Value;
                    LastPlayedCard.Content = clickedCard.DisplayName;

                    if (_playersWhoRespondToDemand.Count == Players.Count) 
                    { 
                        _DemandingColor = false;
                        _demandingPlayer = null;
                    }
                }
            }
        }
        else if (_DemandingValue)
        {
            if (sender is Button button && button.DataContext is MakaoCard clickedCard)
            {
                
                if (clickedCard.Value == _lastPlayedValue)
                {
                    CurrentPlayer.Discard(clickedCard, DiscardDeck);
                    _lastPlayedColor = clickedCard.Color;        
                    LastPlayedCard.Content = clickedCard.DisplayName;

                    
                    _playersWhoRespondToDemand.Add(CurrentPlayer);
                    if (_playersWhoRespondToDemand.Count == Players.Count)
                    {
                        
                        _DemandingValue = false;
                        _demandingPlayer = null;
                    }

                }
            }
            return;
        }

        else
        {
            if (sender is Button button && button.DataContext is MakaoCard clickedCard)
            {
                if (_DrawAmountDefence)
                {
                    if (clickedCard.Value == "2" || clickedCard.Value == "3")
                    {
                        CurrentPlayer.Discard(clickedCard, DiscardDeck);
                        _pendingDrawAmount += (_pendingDrawAmount == 2) ? 2 : 3;
                        _lastPlayedColor = clickedCard.Color;
                        _lastPlayedValue = clickedCard.Value;
                        LastPlayedCard.Content = clickedCard.DisplayName;

                        HandleSpecialCard(clickedCard);

                        EndTurn();
                        return;
                    }
                    else if (clickedCard.Value == "Król" && (clickedCard.Color == "Pik" || clickedCard.Color == "Karo"))
                    {
                        CurrentPlayer.Discard(clickedCard, DiscardDeck);
                        _pendingDrawAmount += 5;
                        _lastPlayedColor = clickedCard.Color;
                        _lastPlayedValue = clickedCard.Value;
                        LastPlayedCard.Content = clickedCard.DisplayName;

                        HandleSpecialCard(clickedCard);
                        EndTurn();
                        return;
                    }
                    else if (clickedCard.Value == "Dama" && (clickedCard.Color == "Pik" || clickedCard.Color == "Karo"))
                    {
                        CurrentPlayer.Discard(clickedCard, DiscardDeck);
                        _lastPlayedColor = clickedCard.Color;
                        _lastPlayedValue = clickedCard.Value;
                        LastPlayedCard.Content = clickedCard.DisplayName;

                        _pendingDrawAmount = 0;
                        _DrawAmountDefence = false;

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
                    Debug.WriteLine($"{CurrentPlayer.Name} musi poczekac {_turnsToSkip} kolejek.");
                    if (clickedCard.Value == "4")
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
                        Debug.WriteLine("Nie obroniłeś się przed pominięciem tury.");
                        _SkipTurnDefence = false;
                        EndTurn();
                        return;
                    }
                }
                else if (clickedCard.Value == "Walet")
                {
                    CurrentPlayer.Discard(clickedCard, DiscardDeck);
                    _lastPlayedValue = "Walet";
                    _lastPlayedColor = null;
                    LastPlayedCard.Content = clickedCard.DisplayName;

                    if (!_DemandingColor)
                    {
                        MakaoColorPanel.IsVisible = true;
                        _waitingForColorChoice = true;
                        _demandingPlayer = CurrentPlayer;
                    }

                    // ------------ DODAC OBSLUGE ASA ----------------

                    return;
                }
                else if (clickedCard.Value == "As") 
                {
                    CurrentPlayer.Discard(clickedCard,DiscardDeck);
                    _lastPlayedValue = "As";
                    _lastPlayedColor = null;
                    LastPlayedCard.Content = clickedCard.DisplayName;

                    if (!_DemandingValue) 
                    {
                        MakaoValuePanel.IsVisible = true;
                        _waitingForColorChoice = true;
                        _demandingPlayer = CurrentPlayer;
                    }
                    return;
                }
                if (clickedCard.Value == _lastPlayedValue || clickedCard.Color == _lastPlayedColor || _lastPlayedColor is null)
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

        if (_SkipTurnDefence)
        {
            Debug.WriteLine($"{CurrentPlayer.Name} nie obronił się — pominięcie tury.");
            _SkipTurnDefence = false;
        }
        if (_DemandingColor)
        {
            _playersWhoRespondToDemand.Add(CurrentPlayer);

            if (_playersWhoRespondToDemand.Count == Players.Count)
            {
                _DemandingColor = false;
                _demandingPlayer = null;
            }
            EndTurn();
            return;
        }

        else if (_DemandingValue)
        {
            _playersWhoRespondToDemand.Add(CurrentPlayer);

            if (_playersWhoRespondToDemand.Count == Players.Count)
            {
                _DemandingValue = false;
                _demandingPlayer = null;
            }
            EndTurn();
            return;
        }


        DrawButton.Content = $"Dobierz kartę ({DrawDeck.Cards.Count} kart)";
        EndTurn();
    }

    public async override void RunGame()
    {
        Console.WriteLine("Running Makao");
        DrawDeck.CreateMakaoDeck();
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
public class MakaoCard : Card
{
    public string Color { get; set; }
    public string Value { get; set; }

    public MakaoCard(string color, string value)
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