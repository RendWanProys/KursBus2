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
    public partial class AddEditSchedule : Window
    {
        public Schedule Schedules { get;  private set; }
        public AddEditSchedule(Schedule _schedule)
        {
            InitializeComponent();
            Schedules = _schedule;
            DataContext = Schedules;
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
