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

    public void InitializeGameObjects() 
    {
        GameEngine.ColorChangePanel = ColorChangePanel;
        GameEngine.DrawButton = DrawButton;
        GameEngine.LastPlayedCard = LastPlayedCard;
    }
    private void Card_OnClick(object? sender, RoutedEventArgs e)
    {
       GameEngine.HandleCardClick(sender);
    }

    private void DrawACard_OnClick(object? sender, RoutedEventArgs e)
    {
       GameEngine.HandleDrawACardClick(sender);
    }

    private void ColorChange_OnClick(object? sender, RoutedEventArgs e) 
    {
        if (sender is Button button && GameEngine is Uno uno) 
        {
            uno.HandleColorButton_click(button.Tag.ToString());
        }
    }

    private void PlayUno_OnClick(object? sender, RoutedEventArgs e)
    {

        GameEngine = new Uno();
        InitializeGameObjects();
        GameEngine.Players.Add(new Player("Gracz 1"));
        GameEngine.Players.Add(new Player("Gracz 2"));       
        GameEngine.RunGame();
        DataContext = GameEngine;
        GamePanel.IsVisible = true;
        ColorChangePanel.IsVisible = false;
        MenuPanel.IsVisible = false;
        
    }
}