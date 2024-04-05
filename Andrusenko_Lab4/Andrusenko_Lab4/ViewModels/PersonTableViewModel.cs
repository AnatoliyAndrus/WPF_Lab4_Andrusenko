using Andrusenko_Lab4.Models;
using Andrusenko_Lab4.Tools;
using Andrusenko_Lab4.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;


namespace Andrusenko_Lab4.ViewModels
{
    internal class PersonTableViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        ObservableCollection<Person> personList;
        private DataGrid dataGrid;
        readonly List<string> filterOptions = new List<string>{"Name", "Surname", "Email", "DateOfBirth",
            "IsAdult", "SunSign", "ChineseSign", "IsBirthday"};
        string selectedFilterOption;
        string filterText;

        private RelayCommand<object> commandDelete;
        private RelayCommand<object> commandEdit;
        private RelayCommand<object> commandAdd;
        private Person selectedPerson;

        private bool isEnabled = true;

        public bool IsEnabled
        {
            get => isEnabled;
            set
            {
                isEnabled = value;
                OnPropertyChanged();
            }
        }
    
        public RelayCommand<object> CommandDelete
        {
            get
            {
                return commandDelete ??= new RelayCommand<object>(_ => ExecuteDelete(), CanExecuteEditOrDelete);
            }
        }
        public RelayCommand<object> CommandEdit
        {
            get
            {
                return commandEdit ??= new RelayCommand<object>(_ => ExecuteEdit(), CanExecuteEditOrDelete);
            }
        }
        public RelayCommand<object> CommandAdd
        {
            get
            {
                return commandAdd ??= new RelayCommand<object>(_ => ExecuteAdd(), _ => true);
            }
        }

        public ObservableCollection<Person> PersonList
        {
            get => personList;
            set => personList = value;
        }

        public Person SelectedPerson
        {
            get => selectedPerson; 
            set
            {
                selectedPerson = value;
                OnPropertyChanged();
            }
        }

        public List<string> FilterOptions => filterOptions;

        public string SelectedFilterOption
        {
            get => selectedFilterOption; set
            {
                selectedFilterOption = value;
                FilterText = "";
                UpdateFilter();
                OnPropertyChanged();
            }
        }
        public string FilterText
        {
            get => filterText; set
            {
                filterText = value;
                OnPropertyChanged();
                UpdateFilter();
            }
        }

        public PersonTableViewModel(DataGrid dataGrid)
        {
            SelectedFilterOption = filterOptions[0];
            FilterText = "";
            this.dataGrid = dataGrid;
            IsEnabled = true;
            personList = PersonToFile.Get();
            if (personList.Count<=0) personList = PersonGenerator.GeneratePeople(50);
            PersonToFile.Save(personList);
        }

        private void UpdateFilter()
        {
            if (dataGrid != null) { 
                dataGrid.Items.Filter = null;
                dataGrid.Items.Filter = new Predicate<object>(item => (((Person)item).GetType().GetProperty(SelectedFilterOption).GetValue(item, null)).ToString().Contains(FilterText));
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void ExecuteDelete()
        {
            int personIndex = personList.IndexOf(SelectedPerson);
            if (personIndex > 0) personList.RemoveAt(personIndex);
        }

        private void ExecuteAdd()
        {
            var window = new AddPersonWindow(personList);
            window.Show();
        }
        private void ExecuteEdit()
        {
            var window = new AddPersonWindow(personList, SelectedPerson);
            window.Show();
        }

        private bool CanExecuteEditOrDelete(object obj)
        {
            return IsEnabled && SelectedPerson != null;
        }
    }
}
