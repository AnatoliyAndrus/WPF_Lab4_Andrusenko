﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using Andrusenko_Lab4.Models;
using Andrusenko_Lab4.Tools;

namespace Andrusenko_Lab4.ViewModels
{
    internal class AddPersonViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private RelayCommand<object> commandSave;
        private RelayCommand<object> commandCancel;

        private string name = "";
        private string surname = "";
        private string email = "";
        private DateTime dateOfBirth = DateTime.Now;
        Window _window;

        ObservableCollection<Person> _list;
        Person _person;

        public RelayCommand<object> CommandCancel
        {
            get
            {
                return commandCancel ??= new RelayCommand<object>(_ => ExecuteCancel(), CanExecuteCancel);
            }
        }
        public RelayCommand<object> CommandSave
        {
            get
            {
                return commandSave ??= new RelayCommand<object>(_ => ExecuteSave(), CanExecuteSave);
            }
        }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }
        public string Surname
        {
            get => surname; set
            {
                surname = value;
                OnPropertyChanged();
            }
        }
        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }
        public DateTime DateOfBirth
        {
            get => dateOfBirth;
            set
            {
                dateOfBirth = value;
                OnPropertyChanged();
            }
        }

        public AddPersonViewModel(ObservableCollection<Person> list, Person? person, Window window)
        {
            if (person != null)
            {
                Name = list[list.IndexOf(person)].Name;
                Surname = list[list.IndexOf(person)].Surname;
                Email = list[list.IndexOf(person)].Email;
                DateOfBirth = list[list.IndexOf(person)].DateOfBirth;
            }
            _list = list;
            _person = person;
            _window = window;
        }
        protected void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private bool CanExecuteSave(object obj)
        {
            return Name != "" && Surname != "" && Email != "";
        }

        private void ExecuteSave()
        {
            try
            {
                Person person = new Person(Name, Surname, Email, DateOfBirth);
                if (_person == null) _list.Add(person);
                else _list[_list.IndexOf(_person)] = person;
                _window.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private bool CanExecuteCancel(object obj)
        {
            return true;
        }
        private void ExecuteCancel()
        {
            _window.Close();
        }
    }
}
