using System;
using System.Collections.Generic;
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
            new UnoCard("Yellow", "9")
        };
    }

    public void CreateMakaoDeck()
    {
        Cards = new List<Card>
        {
            new MakaoCard("Pik","As"),
            new MakaoCard("Kier","As"),
            new MakaoCard("Trefl","As"),
            new MakaoCard("Karo","As"),

            new MakaoCard("Pik","2"),
            new MakaoCard("Kier","2"),
            new MakaoCard("Trefl","2"),
            new MakaoCard("Karo","2"),

            new MakaoCard("Pik","3"),
            new MakaoCard("Kier","3"),
            new MakaoCard("Trefl","3"),
            new MakaoCard("Karo","3"),

            new MakaoCard("Pik","4"),
            new MakaoCard("Kier","4"),
            new MakaoCard("Trefl","4"),
            new MakaoCard("Karo","4"),

            new MakaoCard("Pik","5"),
            new MakaoCard("Kier","5"),
            new MakaoCard("Trefl","5"),
            new MakaoCard("Karo","5"),

            new MakaoCard("Pik","6"),
            new MakaoCard("Kier","6"),
            new MakaoCard("Trefl","6"),
            new MakaoCard("Karo","6"),

            new MakaoCard("Pik","7"),
            new MakaoCard("Kier","7"),
            new MakaoCard("Trefl","7"),
            new MakaoCard("Karo","7"),

            new MakaoCard("Pik","8"),
            new MakaoCard("Kier","8"),
            new MakaoCard("Trefl","8"),
            new MakaoCard("Karo","8"),

            new MakaoCard("Pik","9"),
            new MakaoCard("Kier","9"),
            new MakaoCard("Trefl","9"),
            new MakaoCard("Karo","9"),

            new MakaoCard("Pik","10"),
            new MakaoCard("Kier","10"),
            new MakaoCard("Trefl","10"),
            new MakaoCard("Karo","10"),

            new MakaoCard("Pik","Walet"),
            new MakaoCard("Kier","Walet"),
            new MakaoCard("Trefl","Walet"),
            new MakaoCard("Karo","Walet"),

            new MakaoCard("Pik","Dama"),
            new MakaoCard("Pik","Dama"),
            new MakaoCard("Pik","Dama"),
            new MakaoCard("Karo","Dama"),

            new MakaoCard("Pik","Król"),
            new MakaoCard("Pik","Król"),
            new MakaoCard("Pik","Król"),
            new MakaoCard("Karo","Król"),

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