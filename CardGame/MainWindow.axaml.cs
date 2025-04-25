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
        GameEngine = new Uno();
        GameEngine.Players.Add(new Player("Gracz 1"));
        GameEngine.Players.Add(new Player("Gracz 2"));
        GameEngine.Players.Add(new Player("Gracz 3"));
        GameEngine.Players.Add(new Player("Gracz 4"));
        GameEngine.RunGame();
        DataContext = GameEngine;
    }

    private void Card_OnClick(object? sender, RoutedEventArgs e)
    {
       GameEngine.HandleCardClick(sender);
    }
    
}