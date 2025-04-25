using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CardGame;

public partial class Player
{
    public ObservableCollection<Card> Hand { get; set; }

    public Player()
    {
        Hand = new ObservableCollection<Card>();
    }

    public void Draw(Deck deck)
    {
        Card newCard = deck.Cards.Last();
        deck.Cards.RemoveAt(deck.Cards.Count - 1);
        Hand.Add(newCard);
    }

    public void Draw(Deck deck, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Card newCard = deck.Cards.Last();
            deck.Cards.RemoveAt(deck.Cards.Count - 1);
            Hand.Add(newCard);
        }
    }

    public void Discard(Deck discardDeck, int cardPosition)
    {
        Card discardedCard = Hand[cardPosition];
        Hand.RemoveAt(cardPosition);
        discardDeck.Cards.Add(discardedCard);
    }

    public void Insert(Deck deck, int positionAtHand, int positionInDeck)
    {
        Card insertedCard = Hand[positionAtHand];
        Hand.RemoveAt(positionAtHand);
        deck.Cards.Insert(positionInDeck, insertedCard);
    }

    public void PlayCard(int cardPosition)
    {
        Hand[cardPosition].Play();
    }
}