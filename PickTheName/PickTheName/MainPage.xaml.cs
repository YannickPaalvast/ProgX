using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using PickTheName_db.Models;
using PickTheName_db;
using Coding4Fun.Phone.Controls;

namespace PickTheName
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void ChooseName(object sender, RoutedEventArgs e)
        {
            Random _random = new Random(DateTime.Now.Millisecond);
            if (Silverdb.checkActive())
            {
                List<Name> choose_from = Silverdb.getActiveNames();
                int chosen_index = _random.Next(Silverdb.countActive());

                MessageBox.Show("is the chosen name.", Silverdb.getName(chosen_index), MessageBoxButton.OK);
                         
            }
            else
            {
                MessageBox.Show("There must be selected names");
            }
            
        }

        private void AddName(object sender, RoutedEventArgs e)
        {
            var pin = new InputPrompt();
            pin.Title = "Name";
            pin.Message = "Add ";
            pin.Completed += new EventHandler<PopUpEventArgs<string, PopUpResult>>(NameAdd);
            pin.Show();
        }
        void NameAdd(object sender, PopUpEventArgs<string, PopUpResult> e)
        {
            if (e.Result != null)
            {
                Silverdb.AddName(e.Result);
                MessageBox.Show(e.Result + " is added");
            }
        }

        private void GoToNames(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/NamePage.xaml", UriKind.Relative));
        }

        private void HelpClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/HelpPage.xaml", UriKind.Relative));
        }

        private void ClickSelected(object sender, RoutedEventArgs e)
        {
            if (Silverdb.checkActive())
            {
                NavigationService.Navigate(new Uri("/ActiveName.xaml", UriKind.Relative));
            }
            else
            {
                MessageBox.Show("There are no selected names, select first.");
            }
        }
    }
}