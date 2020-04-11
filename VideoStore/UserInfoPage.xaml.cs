using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VideoStore
{
    /// <summary>
    /// Логика взаимодействия для UserInfoPage.xaml
    /// </summary>
    public partial class UserInfoPage : Page
    {
        VideoStoreEntities _videoStore;
        public UserInfoPage(VideoStoreEntities videoStore)
        {
            InitializeComponent();

            this._videoStore = videoStore;

            textBoxDate.Text = CurrentUser.RegisterDate.ToString();
            textBoxName.Text = CurrentUser.UserName;
            textBoxSurname.Text = CurrentUser.UserSurname;
            textBoxMidname.Text = CurrentUser.UserMiddlename;
            textBoxCardNmber.Text = CurrentUser.CardNumber;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {         
                _videoStore.AddMoney(CurrentUser.UserID);
                AppUpdate.UpdateCashValue();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
