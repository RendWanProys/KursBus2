using ClientKursBus2.Utills;
using ClientKursBus.Utills;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ClientKursBus2.ViewModels
{
    public class NavigationViewModel : ViewModelBase
    {               
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }
        public ICommand HomeCommand { get; set; }
        public ICommand RaceCommand { get; set; }
        private void HomeView(object obj) => CurrentView = new HomeViewModel();
        private void RaceView(object obj) => CurrentView = new RaceViewModel();
        public NavigationViewModel()
        {
            HomeCommand = new RelayCommand(HomeView);
            RaceCommand = new RelayCommand(RaceView);
            CurrentView = new HomeViewModel();
        }
    }
}
