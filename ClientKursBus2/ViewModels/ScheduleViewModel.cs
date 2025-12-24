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
    class ScheduleViewModel:ViewModelBase
    {
        private ScheduleService scheduleService;
        private ObservableCollection<Schedule> scheduleList;
        public ObservableCollection<Schedule> ScheduleList
        {
            get { return scheduleList; }
            set
            {
                if (scheduleList != value)
                {
                    scheduleList = value;
                    OnPropertyChanged(nameof(ScheduleList));
                }
            }
        }

        private Schedule selected;
        public Schedule Selected
        {
            get { return selected; }
            set
            {
                selected = value;
                OnPropertyChanged(nameof(Selected));
            }
        }
        public ScheduleViewModel()
        {
            scheduleService = new ScheduleService();
            Name();
        }
        private void Name()
        {
            try
            {
                ScheduleList = null!;
                Task<List<Schedule>> task = Task.Run(() => scheduleService.GetAll());
                ScheduleList = new ObservableCollection<Schedule>(task.Result);
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
                          AddEditSchedule window = new AddEditSchedule(new Schedule());
                          if (window.ShowDialog() == true)
                          {
                              await scheduleService.Add(window.Schedules);
                              Name();
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
                      Schedule schedule = (obj as Schedule)!;
                      AddEditSchedule window = new AddEditSchedule(schedule);
                      if (window.ShowDialog() == true)
                      {
                          await scheduleService.Update(window.Schedules);
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
                      Schedule schedule = (obj as Schedule)!;
                      MessageBoxResult result = MessageBox.Show("Вы действительно хотите удалить объект " + schedule!.Name + " " + schedule.Route, "Удаление объекта", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                      if (result == MessageBoxResult.Yes)
                      {
                          await scheduleService.Delete(schedule);
                          Name();
                      }
                  }));
            }
        }
    }
}
