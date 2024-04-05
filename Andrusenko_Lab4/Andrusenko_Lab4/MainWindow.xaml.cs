using Andrusenko_Lab4.Tools;
using Andrusenko_Lab4.ViewModels;
using System.Windows;

namespace Andrusenko_Lab4
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            if (PersonDataGrid == null) Close();
            else
                DataContext = new PersonTableViewModel(PersonDataGrid);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            PersonToFile.Save(((PersonTableViewModel)DataContext).PersonList);
        }
    }
}