using Andrusenko_Lab4.Models;
using Andrusenko_Lab4.ViewModels;
using System.Collections.ObjectModel;
using System.Windows;

namespace Andrusenko_Lab4.Views
{
    public partial class AddPersonWindow : Window
    {
        public AddPersonWindow(ObservableCollection<Person> list, Person? person = null)
        {
            InitializeComponent();
            Application.Current.MainWindow.IsEnabled = false;
            DataContext = new AddPersonViewModel(list, person, this);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.MainWindow.IsEnabled = true;
        }
    }
}
