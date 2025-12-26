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
using System.Windows.Shapes;

namespace ClientKursBus2.Views
{
    public partial class AddEditRace : Window
    {
        public Race Races { get;  private set; }
        private ScheduleService _scheduleService;
        public AddEditRace(Race _race)
        {
            InitializeComponent();
            Races = _race;
            DataContext = Races;
            _scheduleService= new ScheduleService();
            Loads();
        }
        private async Task Loads()
        {
            NumberFlightsComboBox.ItemsSource = await _scheduleService.GetAll();
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
