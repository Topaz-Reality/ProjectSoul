using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace ArchiveTool.Views
{
    public class MainWindow : Window
    {
        public static MainWindow Instance;

        public MainWindow()
        {
            InitializeComponent();

            #if DEBUG
            this.AttachDevTools();
            #endif

            Instance = this;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
