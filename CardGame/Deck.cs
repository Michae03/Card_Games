using System;
using System.Collections.Generic;
using System.Numerics;

namespace CardGame;

public class Deck
{
    public List<Card> Cards;

    public Deck()
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
            new UnoCard("Yellow", "9")
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
}