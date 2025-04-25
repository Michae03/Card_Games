using Avalonia.Controls;

namespace CardGame;

public partial class MainWindow : Window
{
    public GameEngine GameEngine;
    public MainWindow()
    {
        InitializeComponent();
        GameEngine = new Uno();
        GameEngine.RunGame();
        DataContext = GameEngine;
    }
}