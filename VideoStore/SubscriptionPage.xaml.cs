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
    /// Логика взаимодействия для SubscriptionPage.xaml
    /// </summary>
    public partial class SubscriptionPage : Page
    {
        VideoStoreEntities videoStore;

        public SubscriptionPage(VideoStoreEntities _videoStore)
        {
            InitializeComponent();

            this.videoStore = _videoStore;

            var subs = videoStore.Subscritions.ToList();

            subscriptionList.Items.Add(new SubscriptionListItem(0, "Бесплатная", "Подписка не предоставляет никаких дополнительных преимуществ, доступна всем пользователям", 0, 0));

            for (int i = 0; i < subs.Count; i++)
            {
                subscriptionList.Items.Add(new SubscriptionListItem(
                    subs[i].SubscriptionID,
                    subs[i].SubscriptionName,
                    subs[i].SubDescription,
                    subs[i].SubPeriodDays,
                    subs[i].SubPrice));
            }

            CheckSubscription();

            AppUpdate.UpdateApp += CheckSubscription;

        }

        private void CheckSubscription()
        {
            if (!CurrentUser.Authorized)
            {
                ((SubscriptionListItem)subscriptionList.Items[0]).ActiveItem();

                for (int i = 1; i < subscriptionList.Items.Count; i++)
                {
                    ((SubscriptionListItem)subscriptionList.Items[i]).ResetItem();
                }
            }
            else
            {
                var sID = videoStore.Users.Where(w => w.UserID == CurrentUser.UserID).FirstOrDefault().SubscriptionID;

                if (sID != null && sID != 0)
                {
                    ((SubscriptionListItem)subscriptionList.Items[(int)sID]).ActiveItem();

                    for (int i = 0; i < subscriptionList.Items.Count; i++)
                    {
                        if (i != (int)sID)
                        {
                            ((SubscriptionListItem)subscriptionList.Items[i]).ResetItem();
                        }
                    }
                }
                else
                {
                    ((SubscriptionListItem)subscriptionList.Items[0]).ActiveItem();


                }
            }
        }

        private void ForcedUpdateSubscripion(int id)
        {
            ((SubscriptionListItem)subscriptionList.Items[id]).ActiveItem();

            for (int i = 0; i < subscriptionList.Items.Count; i++)
            {
                if (i != id)
                {
                    ((SubscriptionListItem)subscriptionList.Items[i]).ResetItem();
                }
            }
        }
        private void moviesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void subscriptionList_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (CurrentUser.Authorized && !((SubscriptionListItem)subscriptionList.SelectedItem).IsActive)
                {
                    priceTextBox.Text = ((SubscriptionListItem)subscriptionList.SelectedItem).Price.ToString();
                    DialogWindowBuySubscription.IsOpen = true;
                }
                else if (((SubscriptionListItem)subscriptionList.SelectedItem).IsActive)
                {
                    return;
                }
                else
                {
                    AppUpdate.GoLibrary = false;
                    AppUpdate.LogonDialogOpen();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double price = ((SubscriptionListItem)subscriptionList.SelectedItem).Price;

                if (CurrentUser.CanBuy(price))
                {
                    if (subscriptionList.SelectedIndex == 0)
                    {
                        videoStore.ResetSubscription(CurrentUser.UserID);

                    }
                    else
                    {
                        videoStore.UpdateSubscription(((SubscriptionListItem)subscriptionList.SelectedItem).ID, CurrentUser.UserID);

                    }

                    AppUpdate.ToBuyMovie(price);
                    AppUpdate.UpdateServices();
                    AppUpdate.ChangeButton();

                    ForcedUpdateSubscripion(((SubscriptionListItem)subscriptionList.SelectedItem).ID);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                DialogWindowBuySubscription.IsOpen = false;

            }
        }

        private void DialogWindowBuySubscription_DialogClosing(object sender, MaterialDesignThemes.Wpf.DialogClosingEventArgs eventArgs)
        {

        }
    }
}
