using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace VideoStore
{
    class SubscriptionListItem: ListViewItem
    {
        int _subID;
        bool isActive;
        public bool IsActive
        {
            get
            {
                return isActive;
            }
        }
        public int ID
        {
            get
            {
                return _subID;
            }
            set
            {
                this._subID = value;
            }
        }

        string _subName;
        string _subDesc;
        int _day;
        double _price;
        public double Price
        {
            get
            {
                return _price;
            }
        }

        public SubscriptionListItem(int id, string name, string descr, int day, double price)
        {
            _subID = id;
            _subName = name;
            _subDesc = descr;
            _day = day;
            _price = price;
            isActive = false;
            CreateItem();
        }

        private void CreateItem()
        {
            this.Padding = new System.Windows.Thickness(1, 1, 1, 1);
            this.Width = 960;

            StackPanel stackPanel = new StackPanel();

            TextBlock SName = new TextBlock();
            SName.Width = 960;
            SName.Height = 40;
            SName.FontSize = 30;
            SName.Text = _subName;
            stackPanel.Children.Add(SName);

            TextBlock SDescr = new TextBlock();
            SDescr.TextWrapping = System.Windows.TextWrapping.Wrap;
            SDescr.Width = 930;
            SDescr.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            SDescr.FontSize = 20;
            SDescr.Foreground = new SolidColorBrush(Color.FromRgb(200, 200, 200));
            SDescr.Text = _subDesc;
            stackPanel.Children.Add(SDescr);

            TextBlock SPrice = new TextBlock();
            SPrice.Margin = new System.Windows.Thickness(0, 5, 0, 0);
            SPrice.Width = 960;
            SPrice.FontSize = 20;
            if(_price <= 0)
            {
                SPrice.Text = $"Цена (руб.): бесплатно";
            }
            else
            {
                SPrice.Text = $"Цена (руб.): {_price}";
            }      
            stackPanel.Children.Add(SPrice);

            TextBlock SDay = new TextBlock();
            SDay.Margin = new System.Windows.Thickness(0, 5, 0, 0);
            SDay.Width = 960;
            SDay.FontSize = 20;
            if (_price <= 0)
            {
                SDay.Text = $"Срок (дней): не ограничено"; ;
            }
            else
            {
                SDay.Text = $"Срок (дней): {_day}"; ;
            }   
            stackPanel.Children.Add(SDay);

            TextBlock subInfo = new TextBlock();
            subInfo.Padding = new System.Windows.Thickness(3);
            subInfo.Margin = new System.Windows.Thickness(0, 10, 15, 0);
            subInfo.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            subInfo.Width = 110;
            subInfo.TextAlignment = System.Windows.TextAlignment.Center;
            subInfo.FontSize = 16;
            subInfo.Background = (SolidColorBrush)this.TryFindResource("PrimaryHueLightBrush");
            subInfo.Text = "Подключить";
            subInfo.Foreground = new SolidColorBrush(Colors.Black);
            stackPanel.Children.Add(subInfo);

            Border border = new Border();
            border.Height = 1;
            border.Background = (SolidColorBrush)this.TryFindResource("PrimaryHueLightBrush");
            stackPanel.Children.Add(border);

            base.AddChild(stackPanel);
        }

        public void ActiveItem()
        {
            StackPanel stackPanel = (StackPanel)base.Content;

            TextBlock subInfoCh = (TextBlock)stackPanel.Children[4];
            subInfoCh.Text = "Подключено";
            subInfoCh.Background = (SolidColorBrush)this.TryFindResource("PrimaryHueDarkBrush");

            Border borderCh = (Border)stackPanel.Children[5];
            borderCh.Background = (SolidColorBrush)this.TryFindResource("PrimaryHueDarkBrush");

            isActive = true;
        }

        public void ResetItem()
        {
            isActive = false;

            StackPanel stackPanel = (StackPanel)base.Content;

            TextBlock subInfoCh = (TextBlock)stackPanel.Children[4];
            subInfoCh.Text = "Подключить";
            subInfoCh.Background = (SolidColorBrush)this.TryFindResource("PrimaryHueLightBrush");

            Border borderCh = (Border)stackPanel.Children[5];
            borderCh.Background = (SolidColorBrush)this.TryFindResource("PrimaryHueLightBrush");
        }
    }
}
