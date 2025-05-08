using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.TextFormatting.Unicode;

namespace CardGame;

public partial class Member : GameEngine
{
    int maximumOnTable=0;
    int numberOfCardstoDraw=3;
    private List<Button> clickedButtons = new List<Button>();
    public List<StandardCard> cardsToPut = new List<StandardCard>();

    Dictionary<string, int> cardValues = new Dictionary<string, int>
    {
        { "1", 1},
        { "2", 2},
        { "3", 3},
        { "4", 4 },
        { "5", 5 },
        { "6", 6 },
        { "7", 7 },
        { "8", 8 },
        { "9", 9 },
        { "10", 10 },
        { "J", 11 },
        { "Q", 12 },
        { "K", 13 },
        { "A", 14 }
    };

    public override void HandleCardClick(object sender)
    {
        var card = sender as Button;
        if (sender is Button button && button.DataContext is Card clickedCard)
        {
            StandardCard scard = (StandardCard)clickedCard;
            if (cardsToPut.Any(c => c.DisplayName == scard.DisplayName))
            {
                cardsToPut.Remove(scard);
                clickedButtons.Remove(card);
                card.Background = Brushes.Black;
            }
            else if (cardValues[scard.Rank] >= maximumOnTable &&
                    (cardsToPut.Count == 0 || cardValues[scard.Rank] == cardValues[cardsToPut[0].Rank]))
            {
                cardsToPut.Add(scard);
                clickedButtons.Add(card);
                card.Background = Brushes.LightGray;
            }
        }
    }


    public override void HandleCardConfirm(object sender)
    { 

        if (cardsToPut.Count == 0)
        {
            return;
        }

        else if (cardsToPut.Count == 2)
        {
            foreach (Button b in clickedButtons)
            {
                b.Background = Brushes.Black;
            }
            cardsToPut.Clear();
            return;
        }

        foreach (StandardCard c in cardsToPut)
        {
            CurrentPlayer.Discard(c, DiscardDeck);
            maximumOnTable = cardValues[c.Rank];
            LastPlayedCard.Content = c.DisplayName;
        }
        cardsToPut.Clear();
        EndTurn();
    }

    public string GetTextBeforeSpace(string input)
    {
        int spaceIndex = input.IndexOf(' ');
        return spaceIndex == -1 ? input : input.Substring(0, spaceIndex);
    }
    public override void HandleDrawACardClick(object sender)
    {
        CurrentPlayer.Draw(DiscardDeck, numberOfCardstoDraw);
        CurrentPlayer.Order();

        if (DiscardDeck.Cards.Count == 0)
        {
            LastPlayedCard.Content = " ";
            maximumOnTable = 0;
        }
        else
        {
            LastPlayedCard.Content = DiscardDeck.Cards[DiscardDeck.Cards.Count - 1].DisplayName;
            string beforeSpace = GetTextBeforeSpace(DiscardDeck.Cards[DiscardDeck.Cards.Count - 1].DisplayName);
            maximumOnTable = cardValues[beforeSpace];
        }

        cardsToPut.Clear();
        EndTurn();
    }

    public override void HandlePlus(object sender)
    {
        var plus = sender as Button;

        if (DiscardDeck.Cards.Count < 3) numberOfCardstoDraw = DiscardDeck.Cards.Count;

        else if (numberOfCardstoDraw <= DiscardDeck.Cards.Count)
        {
            numberOfCardstoDraw++;
        }
 
        DrawButton.Content = $"Dobierz liczbę kart: {numberOfCardstoDraw}";
    }

    public override void HandleMinus(object sender)
    {
        var minus = sender as Button;
        if (DiscardDeck.Cards.Count < 3) numberOfCardstoDraw = DiscardDeck.Cards.Count;
        if (numberOfCardstoDraw > 3)
        {
            numberOfCardstoDraw -= 1;
        }

        DrawButton.Content = $"Dobierz liczbę kart: {numberOfCardstoDraw}";

    }

    

    public async override void RunGame()
    {
        Console.WriteLine("Running member");
        DrawDeck.CreateStandardDeck();
        DrawDeck.Shuffle();
        
        int CardsPerPlayer = 52 / Players.Count;
        foreach (Player player in Players)
        {
            player.Draw(DrawDeck, CardsPerPlayer);
        }

        foreach (Player player in Players)
        {
            player.Draw(DrawDeck);
            player.Order();


        }

        DrawButton.Content = $"Dobierz liczbę kart: {numberOfCardstoDraw}";

        bool gameOver = false;
        while (!gameOver)
        {
            foreach (Player player in Players)
            {
                CurrentPlayer = player;

                numberOfCardstoDraw = 3;
                if (DiscardDeck.Cards.Count < 3) numberOfCardstoDraw = DiscardDeck.Cards.Count;
                DrawButton.Content = $"Dobierz liczbę kart: {numberOfCardstoDraw}";
                await PlayerTurn();
                if (CurrentPlayer.Hand.Count == 0)
                {
                    EndGame(CurrentPlayer);
                    gameOver = true;
                    break;
                }
            }
        }

        GameData GameData = new GameData();
        GameData.dateTime = DateTime.Now;
        GameData.gameNumber = 1;
        GameData.category = "member";
        //GameData.winner = CurrentPlayer.user;

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

    public StandardCard()
    {
        Suit = "";
        Rank = "";
    }
    public override void Play()
    {
        throw new NotImplementedException();
        
    }

    public override string DisplayName => $"{Rank} {Suit}";
}