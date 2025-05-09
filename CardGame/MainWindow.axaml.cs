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
        GameEngine.MakaoColorPanel = MakaoColorPanel;
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

    private void MakaoColorChange_OnClick(object? sender, RoutedEventArgs e)
    {
        if (sender is Button button && GameEngine is Makao makao)
        {
            makao.MakaoHandleColorButton_click(button.Tag.ToString());       
        }
    }

    private void MakaoValueChange_OnClick(object? sender, RoutedEventArgs e)
    {
        if (sender is Button button && GameEngine is Makao makao)
        {
            makao.MakaoHandleValueButton_click(button.Tag.ToString());
        }
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

    private void PlayMakao_OnClick(object? sender, RoutedEventArgs e)
    {
        GameEngine = new Makao();
        GameEngine.Players.Add(new Player("Gracz 1"));
        GameEngine.Players.Add(new Player("Gracz 2"));
        GameEngine.DrawButton = DrawButton;
        GameEngine.LastPlayedCard = LastPlayedCard;
        GameEngine.MakaoColorPanel = MakaoColorPanel;
        GameEngine.MakaoValuePanel = MakaoValuePanel;
        GameEngine.RunGame();
        DataContext = GameEngine;
        MakaoColorPanel.IsVisible = false;
        MakaoValuePanel.IsVisible = false;
        GamePanel.IsVisible = true;
        MenuPanel.IsVisible = false;

    }
}