using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuizBuilder.Quizes.WindowUI.Core;

namespace QuizBuilder.Quizes.WindowUI.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand ManageViewCommand { get; set; }
        public RelayCommand CreateViewComand { get; set; }


        public HomeViewModel homeVM;
        public ManageViewModel manageVM;
        public CreateViewModel createVM;

        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();

            }
        }

        public MainViewModel()
        {
            homeVM = new HomeViewModel();
            manageVM = new ManageViewModel();
            createVM = new CreateViewModel();

            CurrentView = homeVM;

            HomeViewCommand = new RelayCommand(o => { CurrentView = homeVM; });
            ManageViewCommand = new RelayCommand(o => { CurrentView = manageVM; });
            CreateViewComand = new RelayCommand(o => { CurrentView = createVM; });
        }
    }
}
