using System;
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

        // Na start pokazujemy menu, ukrywamy panel gry
        MenuPanel.IsVisible = true;
        GamePanel.IsVisible = false;

        HideAllGameElements();
    }


    private void HideAllGameElements()
    {
        ColorChangePanel.IsVisible = false;
        MakaoColorPanel.IsVisible = false;
        MakaoValuePanel.IsVisible = false;

        DrawButton.IsVisible = false;
        PlusButton.IsVisible = false;
        MinusButton.IsVisible = false;
        ConfirmButton.IsVisible = false;
        LastPlayedCard.IsVisible = false;
        Hand.IsVisible = false;
        PlayerName.IsVisible = false;
    }


    private void ShowUnoElements()
    {
        HideAllGameElements();

        ColorChangePanel.IsVisible = true;
        DrawButton.IsVisible = true;
        ConfirmButton.IsVisible = true;
        LastPlayedCard.IsVisible = true;
        Hand.IsVisible = true;
        PlayerName.IsVisible = true;
    }


    private void ShowMakaoElements()
    {
        HideAllGameElements();

        MakaoColorPanel.IsVisible = true;
        MakaoValuePanel.IsVisible = true;
        DrawButton.IsVisible = true;
        ConfirmButton.IsVisible = true;
        LastPlayedCard.IsVisible = true;
        Hand.IsVisible = true;
        PlayerName.IsVisible = true;
    }

 
    private void ShowMemberElements()
    {
        HideAllGameElements();

        DrawButton.IsVisible = true;
        ConfirmButton.IsVisible = true;
        LastPlayedCard.IsVisible = true;
        Hand.IsVisible = true;
        PlayerName.IsVisible = true
    }

    public void InitializeGameObjects()
    {
        GameEngine.ColorChangePanel = ColorChangePanel;
        GameEngine.MakaoColorPanel = MakaoColorPanel;
        GameEngine.MakaoValuePanel = MakaoValuePanel;
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
        if (!string.IsNullOrWhiteSpace(Name))
        {
            Users.Add(Name);
            PlayersList.Text = Users.ShowUsers();
        }
    }

    private void ColorChange_OnClick(object? sender, RoutedEventArgs e)
    {
        if (sender is Button button && GameEngine is Uno uno)
        {
            uno.HandleColorButton_click(button.Tag.ToString());
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

        MenuPanel.IsVisible = false;
        GamePanel.IsVisible = true;

        ShowUnoElements();
    }

    private void PlayMember_OnClick(object? sender, RoutedEventArgs e)
    {
        GameEngine = new Member();
        InitializeGameObjects();
        AddPlayers();
        GameEngine.RunGame();
        DataContext = GameEngine;

        MenuPanel.IsVisible = false;
        GamePanel.IsVisible = true;

        ShowMemberElements();
    }

    private void PlayMakao_OnClick(object? sender, RoutedEventArgs e)
    {
        GameEngine = new Makao();
        InitializeGameObjects();
        AddPlayers();
        GameEngine.RunGame();
        DataContext = GameEngine;

        MenuPanel.IsVisible = false;
        GamePanel.IsVisible = true;

        ShowMakaoElements();
    }

    private void AddPlayers()
    {
        foreach (User u in Users.listOfPlayers)
        {
            GameEngine.Players.Add(new Player(u.name));
        }
    }

    private void ShowHistory_OnClick(object? sender, RoutedEventArgs e)
    {
        HistoryList.Text = GameEngine.History.Show();
    }
}
