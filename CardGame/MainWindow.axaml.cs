using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace CardGame;

public partial class MainWindow : Window
{
    public GameEngine GameEngine;
    public MainWindow()
    {
        InitializeComponent();
        
    }

    private void Card_OnClick(object? sender, RoutedEventArgs e)
    {
       GameEngine.HandleCardClick(sender);
    }

    private void DrawACard_OnClick(object? sender, RoutedEventArgs e)
    {
       GameEngine.HandleDrawACardClick(sender);
    }

    private void PlayUno_OnClick(object? sender, RoutedEventArgs e)
    {
        GameEngine = new Uno();
        GameEngine.Players.Add(new Player("Gracz 1"));
        GameEngine.Players.Add(new Player("Gracz 2"));
        GameEngine.DrawButton = DrawButton;
        GameEngine.LastPlayedCard = LastPlayedCard;
        GameEngine.RunGame();
        DataContext = GameEngine;
        GamePanel.IsVisible = true;
        MenuPanel.IsVisible = false;
        
    }

    private void PlayExplodingKittens_OnClick(object? sender, RoutedEventArgs e)
    {
        GameEngine = new ExplodingKittens();
        GameEngine.Players.Add(new Player("Gracz 1"));
        GameEngine.Players.Add(new Player("Gracz 2"));
        GameEngine.DrawButton = DrawButton;
        GameEngine.LastPlayedCard = LastPlayedCard;
        GameEngine.RunGame();
        DataContext = GameEngine;
        GamePanel.IsVisible = true;
        MenuPanel.IsVisible = false;

    }
}