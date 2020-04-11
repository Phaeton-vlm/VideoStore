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
    /// Логика взаимодействия для MovieView.xaml
    /// </summary>
    public partial class MovieView : Page
    {
        public delegate void GetID(int id);
        event GetID getIDHandler;

        MoviesList moviesList;

        VideoStoreEntities _videoStore;
        public MovieView(VideoStoreEntities videoStore)
        {
            InitializeComponent();

            _videoStore = videoStore;
            Description_Button.Visibility = Visibility.Collapsed;

            getIDHandler += ShowDecription;

            moviesList = new MoviesList(_videoStore, getIDHandler);
            movieViewFrame.Navigate(moviesList);
        }

        private void Description_Button_Click(object sender, RoutedEventArgs e)
        {
            if (movieDescription != null)
            {
                movieViewFrame.Navigate(movieDescription);             
            }
            
        }

        private void MovieList_Button_Click(object sender, RoutedEventArgs e)
        {
            movieViewFrame.Navigate(moviesList);
        }

        MovieDescription movieDescription;
        void ShowDecription(int id) 
        {
            movieDescription = new MovieDescription(id, _videoStore);
            movieViewFrame.Navigate(movieDescription);
            Description_Button.Visibility = Visibility.Visible;
            Description_Button.Content = (_videoStore.Movies.Where(i => i.FilmID == id).Select(s => s.FilmName).ToList())[0].ToString();
        } 
    }
}
