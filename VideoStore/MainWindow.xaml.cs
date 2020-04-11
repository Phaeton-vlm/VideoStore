using Stimulsoft.Report;
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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        VideoStoreEntities videoStore = new VideoStoreEntities();

        SubscriptionPage subscriptionPage;
        MovieView movieView;
        LibraryView libraryView;
        HelpPage helpPage;
        UserInfoPage userInfoPage;

        public MainWindow()
        {
            InitializeComponent();

            movieView = new MovieView(videoStore);
            libraryView = new LibraryView(videoStore);
            subscriptionPage = new SubscriptionPage(videoStore);
            helpPage = new HelpPage();

            var sr = videoStore.Services.ToList();
            for (int i = 0; i < sr.Count; i++)
            {
                ServicesModel.addSerice(i, sr[i].ServiceName, sr[i].Price);
            }

            navigateFrame.Navigate(movieView);
            loginNameTextBox.Focus();

            menuList.SelectedIndex = 0;

            AppUpdate.ShowLoginDialog += LoginDialogOpen;
            AppUpdate.ChangeCash += ChangeCash;
            AppUpdate.ShowLibrary += ShowUserLibrary;
            AppUpdate.UpdateCash += UpdateCash;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


        }

        /// <summary>
        /// Закрытие программы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_Logout(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Показать форму регистрации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            HideErrors();
            registerPanel.Visibility = Visibility.Collapsed;
            loginPanel.Visibility = Visibility.Visible;
            loginNameTextBox.Focus();
        }

        /// <summary>
        /// Показать форму авторизации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadioButton2_Checked(object sender, RoutedEventArgs e)
        {
            HideErrors();
            loginPanel.Visibility = Visibility.Collapsed;
            registerPanel.Visibility = Visibility.Visible;
            registerName.Focus();
        }

        /// <summary>
        /// Выделить поле окна авторизации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DialogWindowLogin_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            loginNameTextBox.Focus();
        }

        /// <summary>
        /// Открытие окна авторизации/регистрации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void DialogWindowLogin_DialogOpened(object sender, MaterialDesignThemes.Wpf.DialogOpenedEventArgs eventArgs)
        {
            entriRB.IsChecked = true;
        }

        /// <summary>
        /// Очищает поля формы авторизации/регистрации
        /// </summary>
        void ClearLoginFormValues()
        {
            loginNameTextBox.Text = "";
            loginPasswordBox.Password = "";

            registerName.Text = "";
            registerSurname.Text = "";
            registerMidleName.Text = "";
            birthDayPicker.Text = "";
            registerLogin.Text = "";
            registerPassword.Password = "";
            registerRepeatPassword.Password = "";
        }

        /// <summary>
        /// Скрывает сообщение с ошибкой
        /// </summary>
        void HideErrors()
        {
            errorMassageTextBox.Text = "";
            errorMassageTextBox.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Закрытие окна авторизации/регистрации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void DialogWindowLogin_DialogClosing(object sender, MaterialDesignThemes.Wpf.DialogClosingEventArgs eventArgs)
        {
            ClearLoginFormValues();
            HideErrors();
        }
        
        /// <summary>
        /// Авторизация
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var user = videoStore.GetUser(loginNameTextBox.Text, loginPasswordBox.Password).FirstOrDefault();

                if (user == null)
                {
                    errorMassageTextBox.Text = "Неверный логин или пароль";
                    errorMassageTextBox.Visibility = Visibility.Visible;
                    return;
                }
                else
                {
                    CurrentUser.SetUserInfo(user.UserID, user.UserName, user.UserSurname, user.UserMiddleName, user.CardBalance, user.CardNumber, user.RegisterDate);

                    userInfoName.Text = $"{CurrentUser.UserSurname} {CurrentUser.UserName} {CurrentUser.UserMiddlename}";
                    userInfoBalance.Text = CurrentUser.CardBalance.ToString();
                    userInfo.Visibility = Visibility.Visible;

                    loginButton.Visibility = Visibility.Collapsed;
                    logoutButton.Visibility = Visibility.Visible;

                    DialogWindowLogin.IsOpen = false;

                    ClearLoginFormValues();
                    userInfoPage = new UserInfoPage(videoStore);

                    AppUpdate.Update();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }



        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentUser.ResetUser();

            userInfoName.Text = "";
            userInfo.Visibility = Visibility.Collapsed;

            logoutButton.Visibility = Visibility.Collapsed;
            loginButton.Visibility = Visibility.Visible;

            libraryView = new LibraryView(videoStore);
            subscriptionPage = new SubscriptionPage(videoStore);
            movieView = new MovieView(videoStore);

            navigateFrame.Navigate(movieView);

            menuList.SelectedIndex = 0;
        }

        /// <summary>
        /// Открывает страницу с купленными фильмами
        /// </summary>
        void ShowUserLibrary()
        {
            navigateFrame.Navigate(libraryView);
        }

        /// <summary>
        /// Открывает окно авторизации/регистрации
        /// </summary>
        void LoginDialogOpen()
        {
            DialogWindowLogin.IsOpen = true;
        }

        private void menuList_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {


                switch (menuList.SelectedIndex)
                {
                    case 0:
                        {
                            navigateFrame.Navigate(movieView);
                            break;
                        }
                    case 1:
                        {
                            if (CurrentUser.Authorized)
                            {
                                ShowUserLibrary();
                                break;
                            }
                            else
                            {
                                DialogWindowLogin.IsOpen = true;
                                AppUpdate.GoLibrary = true;
                                break;
                            }
                        }
                    case 2:
                        {
                            navigateFrame.Navigate(subscriptionPage);
                            break;
                        }
                    case 3:
                        {
                            StiReport report = new StiReport();
                            report.Load("Reports//Report.mrt");
                            report.Show();
                            break;
                        }

                }
            }
            catch (Exception ex) 
            { 
                MessageBox.Show(ex.Message);
            }


            
        }

        /// <summary>
        /// Обновление значения баланса после покупки
        /// </summary>
        /// <param name="cash"></param>
        void ChangeCash(double cash)
        {
            userInfoBalance.Text = cash.ToString();
        }

        /// <summary>
        /// Регистрация нового пользователя
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(registerName.Text) &&
                    !string.IsNullOrWhiteSpace(registerSurname.Text) &&
                    !string.IsNullOrWhiteSpace(birthDayPicker.Text) &&
                    !string.IsNullOrWhiteSpace(registerLogin.Text) &&
                    !string.IsNullOrWhiteSpace(registerPassword.Password) &&
                    !string.IsNullOrWhiteSpace(registerRepeatPassword.Password))
                {
                    if (registerPassword.Password != registerRepeatPassword.Password)
                    {
                        errorMassageTextBox.Text = "Пароли не совпадают";
                        errorMassageTextBox.Visibility = Visibility.Visible;
                        return;
                    }
                    else if (registerPassword.Password == registerRepeatPassword.Password)
                    {
                        videoStore.CreateNewUser(registerName.Text,
                            registerSurname.Text,
                            registerMidleName.Text,
                            birthDayPicker.SelectedDate,
                            registerLogin.Text,
                            registerPassword.Password,
                            CreateNewCardNumber());

                        DialogWindowLogin.IsOpen = false;                     
                        LoginAfterRegistration(registerLogin.Text, registerPassword.Password);
                    }

                }
                else
                {
                    errorMassageTextBox.Text = "Заполните все обязательные поля";
                    errorMassageTextBox.Visibility = Visibility.Visible;
                    return;
                }
            }
            catch
            {
                errorMassageTextBox.Text = "Пользователь с таким логином уже есть";
                errorMassageTextBox.Visibility = Visibility.Visible;
                return;
            }
        }

        /// <summary>
        /// Создание номера карточки
        /// </summary>
        /// <returns></returns>
        string CreateNewCardNumber()
        {
            Random random = new Random();
            string cardNumber = "";

            for (int i = 0; i < 16; i++)
            {
                cardNumber += random.Next(10).ToString();
            }

            return cardNumber;
        }

        /// <summary>
        /// Авторизация после успешной регистарции
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        void LoginAfterRegistration(string login, string password)
        {
            try
            {
                var user = videoStore.GetUser(login, password).FirstOrDefault();

                if (user == null)
                {
                    errorMassageTextBox.Text = "Неверный логин или пароль";
                    errorMassageTextBox.Visibility = Visibility.Visible;
                    return;
                }
                else
                {
                    CurrentUser.SetUserInfo(user.UserID, user.UserName, user.UserSurname, user.UserMiddleName, user.CardBalance, user.CardNumber, user.RegisterDate);

                    userInfoName.Text = $"{CurrentUser.UserSurname} {CurrentUser.UserName} {CurrentUser.UserMiddlename}";
                    userInfoBalance.Text = CurrentUser.CardBalance.ToString();
                    userInfo.Visibility = Visibility.Visible;

                    loginButton.Visibility = Visibility.Collapsed;
                    logoutButton.Visibility = Visibility.Visible;

                    userInfoPage = new UserInfoPage(videoStore);

                    AppUpdate.Update();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                ClearLoginFormValues();
                HideErrors();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            navigateFrame.Navigate(helpPage);       
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            navigateFrame.Navigate(userInfoPage);
        }

        void UpdateCash()
        {
            userInfoBalance.Text = (Convert.ToDouble(userInfoBalance.Text) + 500).ToString();
        }
    }
}
