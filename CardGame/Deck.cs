using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace CardGame;

public class Deck
{
    public List<Card> Cards;

    public Deck()
    {
       Cards = new List<Card>();
    }

    public void CreateUnoDeck()
    {
        Cards = new List<Card>
        {
            new UnoCard("Red", "0"),
            new UnoCard("Red", "1"),
            new UnoCard("Red", "2"),
            new UnoCard("Red", "3"),
            new UnoCard("Red", "4"),
            new UnoCard("Red", "5"),
            new UnoCard("Red", "6"),
            new UnoCard("Red", "7"),
            new UnoCard("Red", "8"),
            new UnoCard("Red", "9"),

            new UnoCard("Green", "0"),
            new UnoCard("Green", "1"),
            new UnoCard("Green", "2"),
            new UnoCard("Green", "3"),
            new UnoCard("Green", "4"),
            new UnoCard("Green", "5"),
            new UnoCard("Green", "6"),
            new UnoCard("Green", "7"),
            new UnoCard("Green", "8"),
            new UnoCard("Green", "9"),

            new UnoCard("Blue", "0"),
            new UnoCard("Blue", "1"),
            new UnoCard("Blue", "2"),
            new UnoCard("Blue", "3"),
            new UnoCard("Blue", "4"),
            new UnoCard("Blue", "5"),
            new UnoCard("Blue", "6"),
            new UnoCard("Blue", "7"),
            new UnoCard("Blue", "8"),
            new UnoCard("Blue", "9"),

            new UnoCard("Yellow", "0"),
            new UnoCard("Yellow", "1"),
            new UnoCard("Yellow", "2"),
            new UnoCard("Yellow", "3"),
            new UnoCard("Yellow", "4"),
            new UnoCard("Yellow", "5"),
            new UnoCard("Yellow", "6"),
            new UnoCard("Yellow", "7"),
            new UnoCard("Yellow", "8"),
            new UnoCard("Yellow", "9"),

            //--- FUNKCYJNE ---

            //--- ODWROCENIE KOLEJKI ---

            new UnoCard("Yellow", "r"),
            new UnoCard("Green", "r"),
            new UnoCard("Blue", "r"),
            new UnoCard("Red", "r"),

            //--- WYCISZENIE GRACZA ---

            new UnoCard("Yellow", "s"),
            new UnoCard("Green", "s"),
            new UnoCard("Blue", "s"),
            new UnoCard("Red", "s"),

            //--- DOBRANIE KART ---

            new UnoCard("Yellow", "+2"),
            new UnoCard("Green", "+2"),
            new UnoCard("Blue", "+2"),
            new UnoCard("Red", "+2"),

            new UnoCard("Yellow", "+4"),
            new UnoCard("Green", "+4"),
            new UnoCard("Blue", "+4"),
            new UnoCard("Red", "+4"),

            //--- ZAMIANA KOLORU ---
        
            new UnoCard("Any", "p"),
            
        };
    }

    public void create_test_developer_deck()
    {
        Cards = new List<Card>
        {
            new UnoCard("Any", "p"),
            new UnoCard("Any", "p"),
            new UnoCard("Any", "p"),
            new UnoCard("Any", "p"),
            new UnoCard("Any", "p"),
            new UnoCard("Any", "p"),
            
            new UnoCard("Blue", "0"),
            new UnoCard("Blue", "1"),
            new UnoCard("Blue", "2"),
            new UnoCard("Blue", "3"),
            new UnoCard("Blue", "4"),
            new UnoCard("Blue", "5"),
            new UnoCard("Blue", "6"),
            new UnoCard("Blue", "7"),
            new UnoCard("Blue", "8"),
            new UnoCard("Blue", "9"),

            new UnoCard("Yellow", "0"),
            new UnoCard("Yellow", "1"),
            new UnoCard("Yellow", "2"),
            new UnoCard("Yellow", "3"),
            new UnoCard("Yellow", "4"),
            new UnoCard("Yellow", "5"),
            new UnoCard("Yellow", "6"),
        };
    }
    
    public void CreateStandardDeck()
    {
        Cards = new List<Card>
    {
         
        new StandardCard("Hearts", "2"),
        new StandardCard("Hearts", "3"),
        new StandardCard("Hearts", "4"),
        new StandardCard("Hearts", "5"),
        new StandardCard("Hearts", "6"),
        new StandardCard("Hearts", "7"),
        new StandardCard("Hearts", "8"),
        new StandardCard("Hearts", "9"),
        new StandardCard("Hearts", "10"),
        new StandardCard("Hearts", "J"),
        new StandardCard("Hearts", "Q"),
        new StandardCard("Hearts", "K"),
        new StandardCard("Hearts", "A"),

        new StandardCard("Diamonds", "2"),
        new StandardCard("Diamonds", "3"),
        new StandardCard("Diamonds", "4"),
        new StandardCard("Diamonds", "5"),
        new StandardCard("Diamonds", "6"),
        new StandardCard("Diamonds", "7"),
        new StandardCard("Diamonds", "8"),
        new StandardCard("Diamonds", "9"),
        new StandardCard("Diamonds", "10"),
        new StandardCard("Diamonds", "J"),
        new StandardCard("Diamonds", "Q"),
        new StandardCard("Diamonds", "K"),
        new StandardCard("Diamonds", "A"),

        new StandardCard("Clubs", "2"),
        new StandardCard("Clubs", "3"),
        new StandardCard("Clubs", "4"),
        new StandardCard("Clubs", "5"),
        new StandardCard("Clubs", "6"),
        new StandardCard("Clubs", "7"),
        new StandardCard("Clubs", "8"),
        new StandardCard("Clubs", "9"),
        new StandardCard("Clubs", "10"),
        new StandardCard("Clubs", "J"),
        new StandardCard("Clubs", "Q"),
        new StandardCard("Clubs", "K"),
        new StandardCard("Clubs", "A"),

        new StandardCard("Spades", "2"),
        new StandardCard("Spades", "3"),
        new StandardCard("Spades", "4"),
        new StandardCard("Spades", "5"),
        new StandardCard("Spades", "6"),
        new StandardCard("Spades", "7"),
        new StandardCard("Spades", "8"),
        new StandardCard("Spades", "9"),
        new StandardCard("Spades", "10"),
        new StandardCard("Spades", "J"),
        new StandardCard("Spades", "Q"),
        new StandardCard("Spades", "K"),
        new StandardCard("Spades", "A")
    };
    }



    public void Shuffle()
    {
        Random rng = new Random();
        int n = Cards.Count;

        for (int i = n - 1; i > 0; i--)
        {
            int j = rng.Next(i + 1);
            (Cards[i], Cards[j]) = (Cards[j], Cards[i]);
        }
    }

    public void OrderStandardDeck()
    {
        {
            var suitOrder = new Dictionary<string, int>
    {
        { "Clubs", 0 },
        { "Diamonds", 1 },
        { "Hearts", 2 },
        { "Spades", 3 }
    };

            var rankOrder = new Dictionary<string, int>
    {
        { "2", 2 },
        { "3", 3 },
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

            Cards = Cards
                .OrderBy(card => rankOrder[((StandardCard)card).Rank])
                .ThenBy(card => suitOrder[((StandardCard)card).Suit])
                .ToList();
        }
    }
}