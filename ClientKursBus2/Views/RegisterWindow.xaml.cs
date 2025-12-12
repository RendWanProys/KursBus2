using ClientKursBus2.Models;
using ClientKursBus2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClientKursBus2.Views
{
    /// <summary>
    /// Логика взаимодействия для RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private AuthService authService;

        public RegisterWindow()
        {
            InitializeComponent();
            authService = new AuthService();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Password.Password == PasswordRepeat.Password)
            {
                UserData userdata = new UserData { Email = Login.Text, PassWord = Password.Password };
                Task<string> message = Task.Run(() => Register(userdata));
                MessageBox.Show(message.Result);
                this.Close();
            }
        }
        private async Task<string> Register(UserData userdata)
        {
            return await authService.Register(userdata);
        }
    }
}
