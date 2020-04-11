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
    /// Логика взаимодействия для LibraryList.xaml
    /// </summary>
    public partial class LibraryList : Page
    {
        VideoStoreEntities _videoStore;
        event LibraryView.GetID _getIDHandler;

        Sorting sorting;

        public LibraryList(VideoStoreEntities videoStore, LibraryView.GetID getIDHandler)
        {
            this._videoStore = videoStore;
            this._getIDHandler = getIDHandler;

            InitializeComponent();

            genresComboBox.ItemsSource = _videoStore.Genres.Select(s => s.GenreName).ToList();
            countiesComboBox.ItemsSource = _videoStore.Movies.Select(s => s.Country).Distinct().ToList();
        }

        private void moviesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void moviesList_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _getIDHandler((moviesList.SelectedItem as LibraryListItem).ID);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            moviesList.Items.Clear();

            sorting = new Sorting(_videoStore.PurchasedMovies.Where(w => w.UserID == CurrentUser.UserID).Count(), 10, "none", "none");

            var movies = _videoStore.PurchasedMovies.Where(w => w.UserID == CurrentUser.UserID)
                .OrderBy(order => order.FilmID).Take(sorting.PageSize).ToList();

            prevButton.IsEnabled = sorting.CanPrev;
            nextButton.IsEnabled = sorting.CanNext;
            pageTextBox.Text = sorting.CurrentPage.ToString();

            for (int i = 0; i < movies.Count; i++)
            {

                moviesList.Items.Add(new LibraryListItem(movies[i].FilmID,
                    movies[i].Movies.FilmName,
                    movies[i].Movies.ShortDescription,
                    movies[i].Movies.ReleaseYear,
                    movies[i].Movies.Genres.GenreName,
                    movies[i].Movies.Mark,
                    movies[i].Movies.Duration,
                    movies[i].Movies.FilmImage));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            sorting.GoNext();
            ChangePagination();
        }

        /// <summary>
        /// Переход на другую страницу
        /// </summary>
        void ChangePagination()
        {
            try
            {
                moviesList.Items.Clear();

                prevButton.IsEnabled = sorting.CanPrev;
                nextButton.IsEnabled = sorting.CanNext;
                pageTextBox.Text = sorting.CurrentPage.ToString();

                var movies = _videoStore.PurchasedMovies
                    .Where(w => w.UserID == CurrentUser.UserID).AsEnumerable();

                if (sorting.GenresSort != "none")
                {
                    movies = movies.Where(w => w.Movies.Genres.GenreName == sorting.GenresSort);
                }

                if (sorting.CountrySort != "none")
                {
                    movies = movies.Where(w => w.Movies.Country == sorting.CountrySort);
                }

                var moviesI = movies.OrderBy(order => order.FilmID).Skip((sorting.CurrentPage - 1) * sorting.PageSize).Take(sorting.PageSize).ToList();

                for (int i = 0; i < moviesI.Count; i++)
                {

                    moviesList.Items.Add(new LibraryListItem(moviesI[i].FilmID,
                        moviesI[i].Movies.FilmName,
                        moviesI[i].Movies.ShortDescription,
                        moviesI[i].Movies.ReleaseYear,
                        moviesI[i].Movies.Genres.GenreName,
                        moviesI[i].Movies.Mark,
                        moviesI[i].Movies.Duration,
                        moviesI[i].Movies.FilmImage));
                }

                moviesList.ScrollIntoView(moviesList.Items[0]);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        private void prevButton_Click(object sender, RoutedEventArgs e)
        {
            sorting.GoPrev();
            ChangePagination();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DialogWindowSorting.IsOpen = true;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                string gS = "none";
                string cS = "none";

                DialogWindowSorting.IsOpen = false;
                var sortedMovies = _videoStore.PurchasedMovies
                    .Where(w => w.UserID == CurrentUser.UserID).AsEnumerable();

                if (genresComboBox.SelectedIndex != -1)
                {
                    sortedMovies = sortedMovies.Where(w => w.Movies.Genres.GenreName == genresComboBox.SelectedItem.ToString());
                    gS = genresComboBox.SelectedItem.ToString();
                }

                if (countiesComboBox.SelectedIndex != -1)
                {
                    sortedMovies = sortedMovies.Where(w => w.Movies.Country == countiesComboBox.SelectedItem.ToString());
                    cS = countiesComboBox.SelectedItem.ToString();
                }

                sorting = new Sorting(sortedMovies.Count(), 10, gS, cS);

                moviesList.Items.Clear();

                prevButton.IsEnabled = sorting.CanPrev;
                nextButton.IsEnabled = sorting.CanNext;
                pageTextBox.Text = sorting.CurrentPage.ToString();

                var sortedMoviesI = sortedMovies.OrderBy(order => order.FilmID).Skip((sorting.CurrentPage - 1) * sorting.PageSize).Take(sorting.PageSize).ToList();

                for (int i = 0; i < sortedMoviesI.Count; i++)
                {

                    moviesList.Items.Add(new LibraryListItem(sortedMoviesI[i].FilmID,
                        sortedMoviesI[i].Movies.FilmName,
                        sortedMoviesI[i].Movies.ShortDescription,
                        sortedMoviesI[i].Movies.ReleaseYear,
                        sortedMoviesI[i].Movies.Genres.GenreName,
                        sortedMoviesI[i].Movies.Mark,
                        sortedMoviesI[i].Movies.Duration,
                        sortedMoviesI[i].Movies.FilmImage));
                }

                moviesList.ScrollIntoView(moviesList.Items[0]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            moviesList.Items.Clear();

            sorting = new Sorting(_videoStore.Movies.Count(), 10, "none", "none");

            var movies = _videoStore.PurchasedMovies
                .Where(w => w.UserID == CurrentUser.UserID)
                .OrderBy(order => order.FilmID).Take(sorting.PageSize).ToList();

            prevButton.IsEnabled = false;
            nextButton.IsEnabled = true;
            pageTextBox.Text = sorting.CurrentPage.ToString();

            for (int i = 0; i < movies.Count; i++)
            {
                moviesList.Items.Add(new LibraryListItem(movies[i].FilmID,
                    movies[i].Movies.FilmName,
                    movies[i].Movies.ShortDescription,
                    movies[i].Movies.ReleaseYear,
                    movies[i].Movies.Genres.GenreName,
                    movies[i].Movies.Mark,
                    movies[i].Movies.Duration,
                    movies[i].Movies.FilmImage));
            }
        }
    }
}
