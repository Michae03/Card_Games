using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CardGame;

public partial class Player
{
    public ObservableCollection<Card> Hand { get; set; }
    public string Name { get; set; }

    public Player()
    {
        Hand = new ObservableCollection<Card>();
        Name = "Unknown";
    }

    public Player(string name)
    {
        Hand = new ObservableCollection<Card>();
        Name = name;
    }

    public void Draw(Deck deck)
    {
        if (deck.Cards.Count > 0)
        {
            Card newCard = deck.Cards.Last();
            deck.Cards.RemoveAt(deck.Cards.Count - 1);
            Hand.Add(newCard);
        }
    }

    public void Draw(Deck deck, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if (deck.Cards.Count > 0)
            {
                Card newCard = deck.Cards.Last();
                deck.Cards.RemoveAt(deck.Cards.Count - 1);
                Hand.Add(newCard);
            }
           
        }
    }

    public void Discard(Card discardCard)
    {
        Hand.Remove(discardCard);
    }
    public void Discard(Card discardedCard, Deck discardDeck)
    {
        Hand.Remove(discardedCard);
        discardDeck.Cards.Add(discardedCard);
    }

    public void Insert(Card cardToInsert ,Deck deck, int positionInDeck)
    {
        Hand.Remove(cardToInsert);
        deck.Cards.Insert(positionInDeck, cardToInsert);
    }
    
}