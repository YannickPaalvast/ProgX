using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PickTheName_db;
using PickTheName_db.Models;

namespace PickTheName
{
    public partial class NamePage : PhoneApplicationPage
    {
        public NamePage()
        {
            InitializeComponent();
            //nameList.DataContext = Silverdb.getAllNames();
            loadList();
           
        }

        private void loadList()
        {
            nameList.ItemsSource = Silverdb.getAllNames();
        }

        private void Help_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/HelpPage.xaml", UriKind.Relative));
        }
        private void Delete_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/DeleteName.xaml", UriKind.Relative));
        }

        private void DeleteName(object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {
                int _id = ((sender as ListBox).SelectedItem as Name).id;
                Silverdb.DeleteName(_id);
                MessageBox.Show(((sender as ListBox).SelectedItem as Name).name + " is deleted");
                loadList();
            }
            catch { }
        }

       

        private void ActiveName(object sender, SelectionChangedEventArgs e)
        {
            if (nameList.SelectedIndex == -1)
                return;

            
            try
            {

                int _id = ((sender as ListBox).SelectedItem as Name).id;
                if (Silverdb.checkNameActive(_id))
                {
                    Silverdb.AddActive(_id);
                    MessageBox.Show(((sender as ListBox).SelectedItem as Name).name + " is now selected");
                }
                else
                {
                    MessageBox.Show(((sender as ListBox).SelectedItem as Name).name + " is already selected");
                }
            }
            catch(Exception ex) {
                //MessageBox.Show(ex.ToString());
            }
            nameList.SelectedIndex = -1;
        }

        private void Pagina_geladen(object sender, RoutedEventArgs e)
        {
            loadList();
        }
    }
}