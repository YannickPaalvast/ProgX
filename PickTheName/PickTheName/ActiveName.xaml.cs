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
    public partial class ActiveName : PhoneApplicationPage
    {
        public ActiveName()
        {
            InitializeComponent();
            loadList();

        }

        private void loadList()
        {
            nameList.ItemsSource = Silverdb.getActiveNames();
        }

        private void Delete_Active(object sender, System.Windows.Input.GestureEventArgs e)
        {
            try
            {
                int _id = ((sender as ListBox).SelectedItem as Name).id;
                Silverdb.deleteActive(_id);
                MessageBox.Show(((sender as ListBox).SelectedItem as Name).name + " is not selected anymore");
                loadList();
            }
            catch { }
        }
    }
}