using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace CardGame;

public partial class MainWindow : Window
{
    
    public GameEngine GameEngine;
    private Users Users = new Users();
   
    public MainWindow()
    {
        InitializeComponent();
    }

    public void InitializeGameObjects() 
    {
        GameEngine.ColorChangePanel = ColorChangePanel;
        GameEngine.MakaoColorPanel = MakaoColorPanel;
        GameEngine.DrawButton = DrawButton;
        GameEngine.LastPlayedCard = LastPlayedCard;
    }
    private void Card_OnClick(object? sender, RoutedEventArgs e)
    {
        GameEngine.HandleCardClick(sender);
    }

    private void Confirm_OnClick(object? sender, RoutedEventArgs e)
    {
        GameEngine.HandleCardConfirm(sender);
    }

    private void Plus_OnClick(object? sender, RoutedEventArgs e)
    {
        GameEngine.HandlePlus(sender);
    }

    private void Minus_OnClick(object? sender, RoutedEventArgs e)
    {
        GameEngine.HandleMinus(sender);
    }
    private void DrawACard_OnClick(object? sender, RoutedEventArgs e)
    {
        GameEngine.HandleDrawACardClick(sender);
    }

    private void AddPlayer_OnClick(object? sender, RoutedEventArgs e)
    {
        
        string Name = PlayerNameMenu.Text;
        if (Name is not null) Users.Add(Name);
        PlayersList.Text = Users.ShowUsers();
    }

    private void ColorChange_OnClick(object? sender, RoutedEventArgs e) 
    {
        if (sender is Button button && GameEngine is Uno uno) 
        {
            uno.HandleColorButton_click(button.Tag.ToString());
        }
    }

    private void AddPlayers()
    {
        foreach (User u in Users.listOfPlayers)
        {
            GameEngine.Players.Add(new Player(u.name));
        }
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
        InitializeGameObjects();
        AddPlayers();
        GameEngine.RunGame();
        DataContext = GameEngine;
        GamePanel.IsVisible = true;
        ColorChangePanel.IsVisible = false;
        MenuPanel.IsVisible = false;
        ConfirmButton.IsVisible = true;

    }

    private void ShowHistory_OnClick(object? sender, RoutedEventArgs e)
    {
        HistoryList.Text = GameEngine.History.Show();

    }

    private void PlayMember_OnClick(object? sender, RoutedEventArgs e)
    {
        GameEngine = new Member();
        AddPlayers();
        GameEngine.DrawButton = DrawButton;
        GameEngine.LastPlayedCard = LastPlayedCard;
        GameEngine.RunGame();
        DataContext = GameEngine;
        GamePanel.IsVisible = true;
        MenuPanel.IsVisible = false;
        ConfirmButton.IsVisible = true;
        
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