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
            new UnoCard("Red", "1"),
            new UnoCard("Red", "2"),
            new UnoCard("Red", "3"),
            new UnoCard("Green", "1"),
            new UnoCard("Green", "2"),
            new UnoCard("Green", "3"),
            new UnoCard("Blue", "1"),
            new UnoCard("Blue", "2"),
            new UnoCard("Blue", "3"),
            new UnoCard("Yellow", "0"),
            new UnoCard("Red", "1"),
            new UnoCard("Red", "2"),
            new UnoCard("Red", "3"),
            new UnoCard("Green", "1"),
            new UnoCard("Green", "2"),
            new UnoCard("Green", "3"),
            new UnoCard("Blue", "1"),
            new UnoCard("Blue", "2"),
            new UnoCard("Blue", "3"),
            new UnoCard("Yellow", "0"),
            new UnoCard("Red", "1"),
            new UnoCard("Red", "2"),
            new UnoCard("Red", "3"),
            new UnoCard("Green", "1"),
            new UnoCard("Green", "2"),
            new UnoCard("Green", "3"),
            new UnoCard("Blue", "1"),
            new UnoCard("Blue", "2"),
            new UnoCard("Blue", "3"),
            new UnoCard("Yellow", "0")
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