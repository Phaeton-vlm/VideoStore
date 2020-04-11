using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Логика взаимодействия для MovieDescription.xaml
    /// </summary>
    public partial class MovieDescription : Page
    {
        VideoStoreEntities _videoStore;
        MovieModel movieModel;
        public MovieDescription(int id, VideoStoreEntities videoStore)
        {
            InitializeComponent();

            _videoStore = videoStore;

            movieModel = _videoStore.Movies.Where(i => i.FilmID == id).Select(s => new MovieModel
            {
                MovieID = s.FilmID,
                MovieName = s.FilmName,
                Description = s.Description,
                ReleaseYear = s.ReleaseYear,
                Director = s.Director,
                Actors = s.Actors,
                Producers = s.Producers,
                Country = s.Country,
                GenreName = s.Genres.GenreName,
                Duration = s.Duration,
                Mark = s.Mark,
                MovieImage = s.FilmImage,
                Price = s.MoviePrice
            }).FirstOrDefault();

            ChangeBuyButton();
            ChecForServices();

            movieImage.Source = new BitmapImage(new Uri(movieModel.MovieImage));
            movieNameTextBox.Text = movieModel.MovieName;
            descriptionTextBox.Text = movieModel.Description;
            releaseYearTextBlock.Text += $" {movieModel.ReleaseYear}";
            directorTextBlock.Text += $" {movieModel.Director}";
            actorsTextBlock.Text += $"{movieModel.Actors}";
            producersTextBlock.Text += $"{movieModel.Producers}";
            countryTextBlock.Text += $" {movieModel.Country}";
            genreTextBlock.Text += $" {movieModel.GenreName}";
            durationTextBlock.Text += $" {movieModel.Duration}";
            markTextBlock.Text += $" {movieModel.Mark}";
            textBoxPrice.Text = movieModel.Price.ToString();

            AppUpdate.CheckButton += ChangeBuyButton;
            AppUpdate.CheckServices += ChecForServices;
        }

        /// <summary>
        /// Изменяет значение кнопки купить если фильм уже приобретен
        /// </summary>
        void ChangeBuyButton()
        {
            try
            {
                if (CurrentUser.Authorized)
                {
                    var isPurchased = _videoStore.PurchasedMovies
                        .Where(w => w.UserID == CurrentUser.UserID && w.FilmID == movieModel.MovieID)
                        .FirstOrDefault();

                    if (isPurchased != null)
                    {
                        buyButton.Content = "Куплено";
                        buyButton.IsEnabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void ChecForServices()
        {
            try
            {
                if (CurrentUser.Authorized)
                {
                    if (_videoStore.PurchasedMovies.Where(w => w.UserID == CurrentUser.UserID && w.FilmID == movieModel.MovieID).Any())
                    {
                        ResetButtons(true);
                        watchPriceValue.Text = "0";
                        return;
                    }

                    ResetButtons(false);

                    if (_videoStore.ConnectedServices
                        .Where(w => w.UserID == CurrentUser.UserID && w.IsActive == true)
                        .Any())
                    {
                        var services = _videoStore.ConnectedServices.Where(w => w.UserID == CurrentUser.UserID && w.IsActive == true).ToList();

                        for (int i = 0; i < services.Count; i++)
                        {
                            switch (services[i].Services.ServiceName)
                            {
                                case "Высокое качество":
                                    {
                                        highQualityButton.IsChecked = true;
                                        highQualityButton2.IsChecked = true;
                                        break;
                                    }
                                case "Качественный звук":
                                    {
                                        highSoundButton.IsChecked = true;
                                        highSoundButton2.IsChecked = true;
                                        break;
                                    }
                                case "Разные устройства":
                                    {
                                        allDevicesButton.IsChecked = true;
                                        allDevicesButton2.IsChecked = true;
                                        break;
                                    }
                                case "Без рекламы":
                                    {
                                        noAdvertizingButton.IsChecked = true;
                                        noAdvertizingButton2.IsChecked = true;
                                        break;
                                    }
                                case "Язык оригинала":
                                    {
                                        allLanguagesButton.IsChecked = true;
                                        allLanguagesButton2.IsChecked = true;
                                        break;
                                    }
                            }
                        }

                        DoClick();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void DoClick()
        {

            if (highQualityButton.IsChecked == false)
            {
                highQualityButton2.IsHitTestVisible = true;
                highQualityButton2.IsChecked = false;
            }
            else
            {
                highQualityButton2.IsHitTestVisible = false;
                highQualityButton2.IsChecked = true;
            }

            if (highSoundButton.IsChecked == false)
            {
                highSoundButton2.IsHitTestVisible = true;
                highSoundButton2.IsChecked = false;
            }
            else
            {
                highSoundButton2.IsHitTestVisible = false;
                highSoundButton2.IsChecked = true;
            }

            if (allDevicesButton.IsChecked == false)
            {
                allDevicesButton2.IsHitTestVisible = true;
                allDevicesButton2.IsChecked = false;
            }
            else
            {
                allDevicesButton2.IsHitTestVisible = false;
                allDevicesButton2.IsChecked = true;
            }

            if (noAdvertizingButton.IsChecked == false)
            {
                noAdvertizingButton2.IsHitTestVisible = true;
                noAdvertizingButton2.IsChecked = false;
            }
            else
            {
                noAdvertizingButton2.IsHitTestVisible = false;
                noAdvertizingButton2.IsChecked = true;
            }

            if (allLanguagesButton.IsChecked == false)
            {
                allLanguagesButton2.IsHitTestVisible = true;
                allLanguagesButton2.IsChecked = false;
            }
            else
            {
                allLanguagesButton2.IsHitTestVisible = false;
                allLanguagesButton2.IsChecked = true;
            }

        }

        void ResetButtons(bool ch)
        {
            highQualityButton.IsChecked = ch;
            highQualityButton2.IsChecked = ch;
            highSoundButton.IsChecked = ch;
            highSoundButton2.IsChecked = ch;
            allDevicesButton.IsChecked = ch;
            allDevicesButton2.IsChecked = ch;
            noAdvertizingButton.IsChecked = ch;
            noAdvertizingButton2.IsChecked = ch;
            allLanguagesButton.IsChecked = ch;
            allLanguagesButton2.IsChecked = ch;

            DoClick();
        }

        private void TextBlock_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }

        private void buyButton_Click(object sender, RoutedEventArgs e)
        {
            if (!CurrentUser.Authorized)
            {
                AppUpdate.LogonDialogOpen();
                AppUpdate.GoLibrary = false;
            }
            else
            {
                DialogWindowBuy.IsOpen = true;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CurrentUser.CanBuy(movieModel.Price))
                {
                    _videoStore.ToBuyMovie(movieModel.MovieID, CurrentUser.UserID, movieModel.Price);
                    AppUpdate.ToBuyMovie(movieModel.Price);
                    AppUpdate.ChangeButton();
                    ResetButtons(true);
                    DialogWindowBuy.IsOpen = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (!CurrentUser.Authorized)
            {
                AppUpdate.LogonDialogOpen();
                AppUpdate.GoLibrary = false;
            }
            else
            {
                DialogWindowWatchMovie.IsOpen = true;
            }
        }

        private void highQualityButton2_Checked(object sender, RoutedEventArgs e)
        {

            switch (((ToggleButton)sender).Name)
            {
                case "highQualityButton2":
                    {
                        watchPriceValue.Text = (Convert.ToDouble(watchPriceValue.Text) + ServicesModel.FindPriceByName("Высокое качество")).ToString();
                        break;
                    }
                case "highSoundButton2":
                    {
                        watchPriceValue.Text = (Convert.ToDouble(watchPriceValue.Text) + ServicesModel.FindPriceByName("Качественный звук")).ToString();
                        break;
                    }
                case "allDevicesButton2":
                    {
                        watchPriceValue.Text = (Convert.ToDouble(watchPriceValue.Text) + ServicesModel.FindPriceByName("Разные устройства")).ToString();
                        break;
                    }
                case "noAdvertizingButton2":
                    {
                        watchPriceValue.Text = (Convert.ToDouble(watchPriceValue.Text) + ServicesModel.FindPriceByName("Без рекламы")).ToString();
                        break;
                    }
                case "allLanguagesButton2":
                    {
                        watchPriceValue.Text = (Convert.ToDouble(watchPriceValue.Text) + ServicesModel.FindPriceByName("Язык оригинала")).ToString();
                        break;
                    }
            }
            
        }

        private void highQualityButton2_Unchecked(object sender, RoutedEventArgs e)
        {
            switch (((ToggleButton)sender).Name)
            {
                case "highQualityButton2":
                    {
                        watchPriceValue.Text = (Convert.ToDouble(watchPriceValue.Text) - ServicesModel.FindPriceByName("Высокое качество")).ToString();
                        break;
                    }
                case "highSoundButton2":
                    {
                        watchPriceValue.Text = (Convert.ToDouble(watchPriceValue.Text) - ServicesModel.FindPriceByName("Качественный звук")).ToString();
                        break;
                    }
                case "allDevicesButton2":
                    {
                        watchPriceValue.Text = (Convert.ToDouble(watchPriceValue.Text) - ServicesModel.FindPriceByName("Разные устройства")).ToString();
                        break;
                    }
                case "noAdvertizingButton2":
                    {
                        watchPriceValue.Text = (Convert.ToDouble(watchPriceValue.Text) - ServicesModel.FindPriceByName("Без рекламы")).ToString();
                        break;
                    }
                case "allLanguagesButton2":
                    {
                        watchPriceValue.Text = (Convert.ToDouble(watchPriceValue.Text) - ServicesModel.FindPriceByName("Язык оригинала")).ToString();
                        break;
                    }
            }
        }

        private void DialogWindowWatchMovie_Loaded(object sender, RoutedEventArgs e)
        {
          
        }

        private void DialogWindowWatchMovie_DialogOpened(object sender, MaterialDesignThemes.Wpf.DialogOpenedEventArgs eventArgs)
        {
            watchPriceValue.Text = "0";
        }

        private void DialogWindowWatchMovie_DialogClosing(object sender, MaterialDesignThemes.Wpf.DialogClosingEventArgs eventArgs)
        {
            DoClick();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {                      
                AppUpdate.ToBuyMovie(Convert.ToDouble(watchPriceValue.Text));
                _videoStore.LessMoney(CurrentUser.UserID,Convert.ToDouble(watchPriceValue.Text));
                DialogWindowWatchMovie.IsOpen = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
