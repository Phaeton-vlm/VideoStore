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
    /// Логика взаимодействия для LibraryMovieDescription.xaml
    /// </summary>
    public partial class LibraryMovieDescription : Page
    {
        VideoStoreEntities _videoStore;
        MovieModel movieModel;
        public LibraryMovieDescription(int id, VideoStoreEntities videoStore)
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
        }

        private void TextBlock_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }

    }
}
