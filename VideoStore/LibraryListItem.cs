using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace VideoStore
{
    class LibraryListItem : MovieListItem
    {
        public LibraryListItem(int movieId, string filmName, string shortDescription, string releaseYear, string genre, string mark, string duration, string movieImage) : base(movieId, filmName, shortDescription, releaseYear, genre, mark, duration, movieImage)
        {
            
        }

        protected override void CreateItem()
        {
            this.Width = 960;
            this.Height = 260;
            this.Padding = new System.Windows.Thickness(1, 1, 1, 1);
            this.Cursor = Cursors.Hand;
            this.Background = new SolidColorBrush(Color.FromRgb(200, 200, 200));

            //1 Уровень
            StackPanel stackPanel = new StackPanel();
            stackPanel.Children.Add(new StackPanel());

            Separator separator = new Separator();
            separator.Margin = new System.Windows.Thickness(0, -5, 0, 0);
            separator.Height = 2;
            separator.Background = (SolidColorBrush)this.TryFindResource("PrimaryHueDarkBrush");// "{DynamicResource PrimaryHueDarkBrush}"

            stackPanel.Children.Add(separator);
            //Конец 1 уровня

            //2 Уровень
            StackPanel secStacPanel = (StackPanel)stackPanel.Children[0];
            secStacPanel.Orientation = Orientation.Horizontal;

            secStacPanel.Children.Add(new Image());

            Image image = (Image)secStacPanel.Children[0];
            image.Width = 180;
            image.Height = 260;
            image.Source = new BitmapImage(new Uri(_movieImage));

            StackPanel thrStackPanel = new StackPanel();
            thrStackPanel.Margin = new System.Windows.Thickness(10, 0, 10, 0);
            secStacPanel.Children.Add(thrStackPanel);
            //Конец 2 уровня

            //Начало 3 уровня
            StackPanel thrSP = (StackPanel)secStacPanel.Children[1];

            TextBlock fName = new TextBlock();
            fName.Height = 30;
            fName.Width = 760;
            fName.FontSize = 24;
            fName.Text = this._filmName;
            fName.Foreground = (SolidColorBrush)this.TryFindResource("PrimaryHueDarkBrush");
            thrSP.Children.Add(fName);


            TextBlock shrtDescr = new TextBlock();
            shrtDescr.Height = 145;
            shrtDescr.Width = 760;
            shrtDescr.TextWrapping = System.Windows.TextWrapping.Wrap;
            shrtDescr.Foreground = new SolidColorBrush(Colors.Black);// (SolidColorBrush)this.TryFindResource("PrimaryHueMidBrush");//new SolidColorBrush(Color.FromRgb(149, 149, 149));
            shrtDescr.Text = this._shortDescription;
            thrSP.Children.Add(shrtDescr);

            StackPanel fothStackPanel = new StackPanel();
            fothStackPanel.Margin = new System.Windows.Thickness(0, -2, 0, 0);
            thrSP.Children.Add(fothStackPanel);
            //Конец 3 уровня

            //Начало 4 уровня
            StackPanel ftSP = (StackPanel)thrSP.Children[2];

            TextBlock relYear = new TextBlock();
            relYear.Text = $"Год выхода: {this._releaseYear}";
            relYear.Foreground = (SolidColorBrush)this.TryFindResource("PrimaryHueDarkBrush");
            ftSP.Children.Add(relYear);

            TextBlock genre = new TextBlock();
            genre.Text = $"Жанр: {this._genre}";
            genre.Foreground = (SolidColorBrush)this.TryFindResource("PrimaryHueDarkBrush");
            ftSP.Children.Add(genre);

            TextBlock mark = new TextBlock();
            mark.Text = $"Оценка: {this._mark}";
            mark.Foreground = (SolidColorBrush)this.TryFindResource("PrimaryHueDarkBrush");
            ftSP.Children.Add(mark);

            TextBlock dur = new TextBlock();
            dur.Text = $"Время: {this._duration} мин.";
            dur.Foreground = (SolidColorBrush)this.TryFindResource("PrimaryHueDarkBrush");
            ftSP.Children.Add(dur);
            //Конец 4 уровня

            base.AddChild(stackPanel);

        }
    }
}
