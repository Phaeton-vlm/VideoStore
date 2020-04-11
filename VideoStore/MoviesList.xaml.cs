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
    /// Логика взаимодействия для MoviesList.xaml
    /// </summary>
    public partial class MoviesList : Page
    {
        VideoStoreEntities _videoStore;
        event MovieView.GetID _getIDHandler;

        Sorting sorting;
        public MoviesList(VideoStoreEntities videoStore, MovieView.GetID getIDHandler)
        {
            this._videoStore = videoStore;
            this._getIDHandler = getIDHandler;

            InitializeComponent();

            genresComboBox.ItemsSource = _videoStore.Genres.Select(s => s.GenreName).ToList();
            countiesComboBox.ItemsSource = _videoStore.Movies.Select(s => s.Country).Distinct().ToList();

            sorting = new Sorting(_videoStore.Movies.Count(), 10, "none", "none");

            var movies = _videoStore.Movies.OrderBy(order => order.FilmID).Take(sorting.PageSize).ToList();

            prevButton.IsEnabled = false;
            nextButton.IsEnabled = true;
            pageTextBox.Text = sorting.CurrentPage.ToString();

            for (int i = 0; i < movies.Count; i++)
            {
                moviesList.Items.Add(new MovieListItem(movies[i].FilmID,
                    movies[i].FilmName,
                    movies[i].ShortDescription,
                    movies[i].ReleaseYear,
                    movies[i].Genres.GenreName,
                    movies[i].Mark,
                    movies[i].Duration,
                    movies[i].FilmImage));
            }         
        }

        private void moviesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void moviesList_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _getIDHandler((moviesList.SelectedItem as MovieListItem).ID);
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

                var movies = _videoStore.Movies.AsEnumerable();

                if(sorting.GenresSort != "none")
                {
                    movies = movies.Where(w => w.Genres.GenreName == sorting.GenresSort);
                }

                if (sorting.CountrySort != "none")
                {
                    movies = movies.Where(w => w.Country == sorting.CountrySort);
                }

                var moviesI = movies.OrderBy(order => order.FilmID).Skip((sorting.CurrentPage - 1) * sorting.PageSize).Take(sorting.PageSize).ToList();

                for (int i = 0; i < moviesI.Count; i++)
                {

                    moviesList.Items.Add(new MovieListItem(moviesI[i].FilmID,
                        moviesI[i].FilmName,
                        moviesI[i].ShortDescription,
                        moviesI[i].ReleaseYear,
                        moviesI[i].Genres.GenreName,
                        moviesI[i].Mark,
                        moviesI[i].Duration,
                        moviesI[i].FilmImage));
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

        /// <summary>
        /// Сортировка по критериям
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                string gS = "none";
                string cS = "none";

                DialogWindowSorting.IsOpen = false;
                var sortedMovies = _videoStore.Movies.AsEnumerable();

                if (genresComboBox.SelectedIndex != -1)
                {
                    sortedMovies = sortedMovies.Where(w => w.Genres.GenreName == genresComboBox.SelectedItem.ToString());
                    gS = genresComboBox.SelectedItem.ToString();
                }

                if (countiesComboBox.SelectedIndex != -1)
                {
                    sortedMovies = sortedMovies.Where(w => w.Country == countiesComboBox.SelectedItem.ToString());
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

                    moviesList.Items.Add(new MovieListItem(sortedMoviesI[i].FilmID,
                        sortedMoviesI[i].FilmName,
                        sortedMoviesI[i].ShortDescription,
                        sortedMoviesI[i].ReleaseYear,
                        sortedMoviesI[i].Genres.GenreName,
                        sortedMoviesI[i].Mark,
                        sortedMoviesI[i].Duration,
                        sortedMoviesI[i].FilmImage));
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

            //genresComboBox.SelectedItem = -1;
            //countiesComboBox.SelectedItem = -1;

            var movies = _videoStore.Movies.OrderBy(order => order.FilmID).Take(sorting.PageSize).ToList();

            prevButton.IsEnabled = false;
            nextButton.IsEnabled = true;
            pageTextBox.Text = sorting.CurrentPage.ToString();

            for (int i = 0; i < movies.Count; i++)
            {
                moviesList.Items.Add(new MovieListItem(movies[i].FilmID,
                    movies[i].FilmName,
                    movies[i].ShortDescription,
                    movies[i].ReleaseYear,
                    movies[i].Genres.GenreName,
                    movies[i].Mark,
                    movies[i].Duration,
                    movies[i].FilmImage));
            }
        }
    }
}
