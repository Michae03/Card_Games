using System;

namespace CardGame;

public abstract class GameEngine
{
    public Player Player1 { get; set; }
    protected Deck DrawDeck = new Deck();
    protected Deck DiscardDeck = new Deck();

    protected GameEngine()
    {
        Player1 = new Player();
    } 
    public abstract void RunGame();
}

public partial class ExplodingKittens : GameEngine
{
   
    public override void RunGame()
    {
        Console.WriteLine("Running exploding kittens");
    }
}

public partial class Uno : GameEngine
{
  
    public override void RunGame()
    {
        Console.WriteLine("Running uno");
        DrawDeck.Shuffle();
        Player1.Draw(DrawDeck, 19);
        foreach (var card in Player1.Hand)
        {
            Console.WriteLine(card.DisplayName);
        }
    }
}

public partial class Member : GameEngine
{
    public override void RunGame()
    {
        Console.WriteLine("Running member");
    }
}