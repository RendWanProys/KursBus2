using ClientKursBus2.Models;
using ClientKursBus2.Services;
using ClientKursBus2.Utills;
using ClientKursBus2.Views;
using ClientKursBus.Utills;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClientKursBus2.ViewModels
{
    class RaceViewModel:ViewModelBase
    {
        private RaceService raceService;
        private ObservableCollection<Race> raceList;
        public ObservableCollection<Race> RaceList
        {
            get { return raceList; }
            set
            {
                if (raceList != value)
                {
                    raceList = value;
                    OnPropertyChanged(nameof(RaceList));
                }
            }
        }

        private Race selected;
        public Race Selected
        {
            get { return selected; }
            set
            {
                selected = value;
                OnPropertyChanged(nameof(Selected));
            }
        }
        public RaceViewModel()
        {
            raceService = new RaceService();
            Load();
        }
        private void Load()
        {
            try
            {
                RaceList = null!;
                Task<List<Race>> task = Task.Run(() => raceService.GetAll());
                RaceList = new ObservableCollection<Race>(task.Result);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(async obj =>
                  {
                      try
                      {
                          AddEditRace window = new AddEditRace(new Race());
                          if (window.ShowDialog() == true)
                          {
                              await raceService.Add(window.Races);
                              Load();
                          }
                      }
                      catch { }
                  }));
            }
        }
        private RelayCommand editCommand;
        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ??
                  (editCommand = new RelayCommand(async obj =>
                  {
                      Race race = (obj as Race)!;
                      AddEditRace window = new AddEditRace(race);
                      if (window.ShowDialog() == true)
                      {
                          await raceService.Update(window.Races);
                      }
                  }));
            }
        }
        private RelayCommand deleteCommand;
        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                  (deleteCommand = new RelayCommand(async obj =>
                  {
                      Race race = (obj as Race)!;
                      MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить объект " + race!.Load + " " + race.Pass, "Удаление объекта", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                      if (result == MessageBoxResult.Yes)
                      {
                          await raceService.Delete(race);
                          Load();
                      }
                  }));
            }
        }
    }
}
