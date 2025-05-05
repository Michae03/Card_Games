using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Microsoft.VisualBasic;

namespace CardGame;

public partial class ExplodingKittens : GameEngine
{
    public override void HandleCardClick(object sender)
    {
        if (sender is Button button && button.DataContext is Card clickedCard)
        {
            if (clickedCard.DisplayName != "Kitten" && clickedCard.DisplayName != "Defuse")
            {
                CurrentPlayer.Discard(clickedCard, DiscardDeck);
                LastPlayedCard.Content = clickedCard.DisplayName;
                EndTurn();
            }
            else 
            {
                Console.WriteLine("Nie możesz zagrac tej karty");
            }
            
        }
    }
    public override void HandleDrawACardClick(object sender)
    {
        CurrentPlayer.Draw(DrawDeck);
        DrawButton.Content = $"Dobierz kartę ({DrawDeck.Cards.Count} kart)";
        EndTurn();
    }
   
    public async override void RunGame()
    {
        Console.WriteLine("Running Exploding Kittens");
        DrawDeck.CreateExplodingKittensDeck();
        DrawDeck.Shuffle();
        foreach (Player player in Players)
        {
            var defuseCard = DrawDeck.Cards.FirstOrDefault(c => c is ExplodingKittenCard ekc && ekc.Effect == "Defuse");
            if (defuseCard != null)
            {
                DrawDeck.Cards.Remove(defuseCard);
                player.Hand.Add(defuseCard);
            }
            else
            {
                Console.WriteLine("Brakuje kart Defuse w talii!");
            }
        }
        DrawButton.Content = $"Dobierz kartę ({DrawDeck.Cards.Count} kart)";

        List<Player> Losers = new List<Player>();
        bool gameOver = false;

       while (!gameOver) {

            foreach (Player player in Players)
            {
                if (Players.Count > 1)
                {
                    if (Losers.Contains(player))
                    {
                        continue;
                    }

                    CurrentPlayer = player;
                    await PlayerTurn();

                    // ----------- PRZEGRANA ------------

                    if (player.Hand.Any(c => c.DisplayName == "Kitten") && player.Hand.Any(d => d.DisplayName != "Defuse"))
                    {

                        Losers.Add(player);
                    }
                    else 
                    {
                        
                    
                    }
                }

                else 
                {
                    Console.WriteLine($"Wygrywa gracz {CurrentPlayer} !");
                    gameOver = true;
                }

                


            }
       }
    }
}
public class ExplodingKittenCard : Card
{
    public string Effect { get; set; }

    public ExplodingKittenCard(string effect)
    {
        Effect = effect;

    }

    public override void Play()
    {
        throw new NotImplementedException();
    }

    public override string DisplayName => $"{Effect}";
}