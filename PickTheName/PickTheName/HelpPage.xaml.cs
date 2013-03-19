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

namespace PickTheName
{
    public partial class HelpPage : PhoneApplicationPage
    {
        public HelpPage()
        {
            InitializeComponent();
        }

        private void Active_Click(object sender, EventArgs e)
        {
            try
            {

                Silverdb.LeegActiveDB();
                MessageBox.Show("All selected names are deleted");
            }
            catch { }
        }
        private void Name_Click(object sender, EventArgs e)
        {
            try
            {
                Silverdb.LeegNameDB();
                MessageBox.Show("All names are deleted");
            }
            catch { }
        }
    }
}