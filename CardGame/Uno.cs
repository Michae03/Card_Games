using System;
using System.Threading.Tasks;
using Avalonia.Controls;

namespace CardGame;

public partial class Uno : GameEngine
{
    public override void HandleCardClick(object sender)
    {
        if (sender is Button button && button.DataContext is Card clickedCard)
        {
            CurrentPlayer.Discard(clickedCard);
            if (WaitForPlayerAction != null)
            {
                WaitForPlayerAction.TrySetResult(true);
            }
            
        }
    }

    public async override void RunGame()
    {
        Console.WriteLine("Running uno");
        
        DrawDeck.Shuffle();
        Console.WriteLine("Deck Shuffled");
        
        foreach (Player player in Players)
        {
            player.Draw(DrawDeck, 5);
            Console.WriteLine(player + " Draw 5 cards");
        }

        while (true)
        {
            foreach (Player player in Players)
            {
                CurrentPlayer = player;
                CurrentPlayer.Draw(DrawDeck);
                WaitForPlayerAction = new TaskCompletionSource<bool>();
                Console.WriteLine("Oczekiwanie na akcje " + CurrentPlayerName);
                await WaitForPlayerAction.Task;
                WaitForPlayerAction = null;
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
    

    public override string DisplayName => $"{Color} {Value}";
}