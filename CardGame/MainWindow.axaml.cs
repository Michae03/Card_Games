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
       string Name = PlayerName.Text;
       if (Name is not null) Users.Add(Name);
       PlayersList.Text = Users.ShowUsers();

    }

    private void AddPlayers()
    {
        foreach (User u in Users.listOfPlayers)
        {
            GameEngine.Players.Add(new Player(u.name));
        }
    }
    private void PlayUno_OnClick(object? sender, RoutedEventArgs e)
    {
        GameEngine = new Uno();
        AddPlayers();
        GameEngine.DrawButton = DrawButton;
        GameEngine.LastPlayedCard = LastPlayedCard;
        GameEngine.RunGame();
        DataContext = GameEngine;
        GamePanel.IsVisible = true;
        MenuPanel.IsVisible = false;
        ConfirmButton.IsVisible = false;

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

 
    

}