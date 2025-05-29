using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Avalonia.Controls;

namespace CardGame;

public abstract class GameEngine : INotifyPropertyChanged
{
    private Player _currentPlayer;
    public Player CurrentPlayer
    {
        get => _currentPlayer;
        set
        {
            if (_currentPlayer != value)
            {
                _currentPlayer = value;
                OnPropertyChanged(nameof(CurrentPlayer));
                OnPropertyChanged(nameof(CurrentPlayerName));
            }
        }
    }


    public List<Player> Players{ get; set;}
    protected Deck DrawDeck = new Deck(); 
    protected Deck DiscardDeck = new Deck();
    protected TaskCompletionSource<bool>? WaitForPlayerAction;
    public static History History = new History();

    // TU SĄ OBJEKTY Z XML:
    public Button LastPlayedCard {get;  set; }
    public Button DrawButton {get;  set; }
    public Grid ColorChangePanel { get; set; }
    
    public Grid MakaoColorPanel { get; set; }

    public Grid MakaoValuePanel { get; set; }



    protected GameEngine()
    {
        Players = new List<Player>();

    } 
    public abstract void RunGame();

    public void EndGame(Player player)
    {
        Console.WriteLine(player.Name + " WYGRAl!!!!!");
    }
    
    public async Task PlayerTurn()
    {
        WaitForPlayerAction = new TaskCompletionSource<bool>();
        await WaitForPlayerAction.Task;
        WaitForPlayerAction = null;
    }
    public void EndTurn()
    {
        if (WaitForPlayerAction != null)
        {
            WaitForPlayerAction.TrySetResult(true);
        }
    }

    public abstract void HandleCardClick(Object sender);

    public abstract void HandleDrawACardClick(Object sender);

    public abstract void HandleCardConfirm(Object sender);

    public abstract void HandlePlus(Object sender);

    public abstract void HandleMinus(Object sender);
   

    public string CurrentPlayerName => CurrentPlayer.Name;
    
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
