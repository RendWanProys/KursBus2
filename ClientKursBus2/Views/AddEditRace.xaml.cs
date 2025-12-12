using ClientKursBus2.Models;
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
using System.Windows.Shapes;

namespace ClientKursBus2.Views
{
    public partial class AddEditRace : Window
    {
        public Race Races { get;  private set; }
        public AddEditRace(Race _race)
        {
            InitializeComponent();
            Races = _race;
            DataContext = Races;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DialogResult= false;
        }
    }
}
