using ClientKursBus2.ViewModels;
using ClientKursBus2.Views;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClientKursBus2
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public static LoginWindow Instance { get; private set; }
        public LoginWindow()
        {
            InitializeComponent();
            Instance = this;
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow regWindow = new RegisterWindow();
            regWindow.Show();
        }
    }
}