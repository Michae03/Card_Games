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
    public List<Player> Players{ get; set; }
    protected Deck DrawDeck = new Deck(); 
    protected Deck DiscardDeck = new Deck();
    protected TaskCompletionSource<bool>? WaitForPlayerAction;


    protected GameEngine()
    {
        Players = new List<Player>();
    } 
    public abstract void RunGame();

    public abstract void HandleCardClick(Object sender);
    public string CurrentPlayerName => CurrentPlayer.Name;
    
    
    
    
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
