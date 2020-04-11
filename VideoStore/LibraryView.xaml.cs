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
    /// Логика взаимодействия для LibraryView.xaml
    /// </summary>
    public partial class LibraryView : Page
    {

        public delegate void GetID(int id);
        event GetID getIDHandler;

        LibraryList libraryList;

        VideoStoreEntities _videoStore;
        public LibraryView(VideoStoreEntities videoStore)
        {
            InitializeComponent();

            _videoStore = videoStore;
            Description_Button.Visibility = Visibility.Collapsed;

            getIDHandler += ShowDecription;

            libraryList = new LibraryList(_videoStore, getIDHandler);
            movieViewFrame.Navigate(libraryList);
        }

        private void Description_Button_Click(object sender, RoutedEventArgs e)
        {
            if (libraryMovieDescription != null)
            {
                movieViewFrame.Navigate(libraryMovieDescription);
            }
        }

        /// <summary>
        /// Открывает список купленных фильмов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MovieList_Button_Click(object sender, RoutedEventArgs e)
        {
            movieViewFrame.Navigate(libraryList);
        }

        LibraryMovieDescription libraryMovieDescription;

        /// <summary>
        /// Показывает окно с описанием фильма
        /// </summary>
        /// <param name="id">id фильма</param>
        void ShowDecription(int id)
        {
            libraryMovieDescription = new LibraryMovieDescription(id, _videoStore);
            movieViewFrame.Navigate(libraryMovieDescription);
            Description_Button.Visibility = Visibility.Visible;
            Description_Button.Content = (_videoStore.Movies.Where(i => i.FilmID == id).Select(s => s.FilmName).ToList())[0].ToString();
        }
    }
}
