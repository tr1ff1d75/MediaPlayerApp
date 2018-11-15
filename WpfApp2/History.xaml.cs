using System;
using System.Windows;
using System.Data.Entity;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;

namespace MediaPlayerApp
{

    public partial class History : Window
    {
        private MediaHistoryContext db;
        private bool favoriteOpen = false;

        public History()
        {
            InitializeComponent();
            try
            {
                favoriteOpen = false;
                db = new MediaHistoryContext();
                db.Media.Load();
                HistoryGrid.ItemsSource = db.Media.Local.ToBindingList(); // устанавливаем привязку к кэшу

                this.Closing += History_Closing;
            }
            catch
            {
                MessageBox.Show("Не удалось подклюситься к базе данных!", "MediaPlayerApp", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public History(bool filterOnFavorite)
        {
            InitializeComponent();
            try
            {
                favoriteOpen = true;
                db = new MediaHistoryContext();
                db.Media.Load();
                var BindingList = db.Media.Local.ToBindingList();
                HistoryGrid.ItemsSource = from a in BindingList where (a as MediaHistory).Favorite == filterOnFavorite select a;
                this.Closing += History_Closing;
            }
            catch
            {
                MessageBox.Show("Не удалось подклюситься к базе данных!", "MediaPlayerApp", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void History_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            db.Dispose();
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            button_animation_size(updateButton);
            db.SaveChanges();
        }

        /// <summary>
        /// Удаление фрагментов из избранного и истории
        /// </summary>
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            button_animation_size(deleteButton);
            if (HistoryGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < HistoryGrid.SelectedItems.Count; i++)
                {
                    MediaHistory mHisory = HistoryGrid.SelectedItems[i] as MediaHistory;
                    if (mHisory != null)
                    {
                        db.Media.Remove(mHisory);
                    }
                }
            }
            db.SaveChanges();
            if(favoriteOpen)
            {
                db = new MediaHistoryContext();
                db.Media.Load();
                var BindingList = db.Media.Local.ToBindingList();
                HistoryGrid.ItemsSource = from a in BindingList where (a as MediaHistory).Favorite == true select a;
                this.Closing += History_Closing;
            }
        }

        /// <summary>
        /// Открытие файла из истории
        /// </summary>
        private void openButton_Click(object sender, RoutedEventArgs e)
        {
            button_animation_size(openFileButton);

            if (HistoryGrid.SelectedItems.Count > 0)
            {
                    MediaHistory mHisory = HistoryGrid.SelectedItem as MediaHistory;

                    MainWindow winPlayer = new MainWindow();
                    winPlayer.Show();
                    winPlayer.historyMediaOpen(sender, e, mHisory.Path);
                    this.Close();
            }
        }

        /// <summary>
        /// Открытие и зыкрытин меню
        /// </summary>
        private void buttom_menu_Click(object sender, RoutedEventArgs e)
        {
            button_animation_size(buttom_menu);

            menu_panel.Visibility = Visibility.Visible;
            Image buttonImg = new Image();
            if (menu_panel.Opacity == 0)
            {
                DoubleAnimation visible = new DoubleAnimation();
                visible.From = 0;
                visible.To = 1;
                visible.Duration = new Duration(TimeSpan.FromSeconds(0.2));
                menu_panel.BeginAnimation(OpacityProperty, visible);

                buttonImg.Source = new BitmapImage(new Uri("/png/forward.png", UriKind.Relative));
                buttom_menu.Content = buttonImg;
                buttom_menu.ToolTip = "Close menu";
            }
            else
            {
                DoubleAnimation hidden = new DoubleAnimation();
                hidden.From = 1;
                hidden.To = 0;
                hidden.Duration = new Duration(TimeSpan.FromSeconds(0.2));
                menu_panel.BeginAnimation(OpacityProperty, hidden);

                buttonImg.Source = new BitmapImage(new Uri("/png/menu.png", UriKind.Relative));
                buttom_menu.Content = buttonImg;
                buttom_menu.ToolTip = "Open menu";
            }
        }

        /// <summary>
        /// Закрытие окна
        /// </summary>
        private void buttom_close_Click(object sender, RoutedEventArgs e)
        {
            MainWindow winPlayer = new MainWindow();
            this.Close();
            winPlayer.Show();
        }

        /// <summary>
        /// Воспроизвести медиафайл
        /// </summary>
        private void buttom_menu_fileOpen_Click(object sender, RoutedEventArgs e)
        {
            button_animation_size(buttom_menu_fileOpen);
            MainWindow winPlayer = new MainWindow();
            this.Close();
            winPlayer.Show();
            winPlayer.buttom_menu_fileOpen_Click(sender, e);
        }

        /// <summary>
        /// История просмотров
        /// </summary>
        private void buttom_menu_history_Click(object sender, RoutedEventArgs e)
        {
            button_animation_size(buttom_menu_history);
            History history = new History();
            this.Close();
            history.Show();
        }

        /// <summary>
        /// Открыть избранные медиафайлы
        /// </summary>
        private void buttom_menu_favorite_Click(object sender, RoutedEventArgs e)
        {
            button_animation_size(buttom_menu_likes);
            History history = new History(true);
            this.Close();
            history.Show();
        }

        /// <summary>
        /// Вернуться в домашнее окно (
        /// </summary>
        private void buttom_menu_home_Click(object sender, RoutedEventArgs e)
        {
            button_animation_size(buttom_menu_home);
            MainWindow winPlayer = new MainWindow();
            this.Close();
            winPlayer.Show();
        }

        /// <summary>
        /// Перетаскивание окна
        /// </summary>
        private void Window_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        /// <summary>
        /// Анимация нажатия кнопки
        /// </summary>
        public void button_animation_size(Button x)
        {
            DoubleAnimation widthAnimation = new DoubleAnimation();
            widthAnimation.By = -3;
            widthAnimation.Duration = TimeSpan.FromSeconds(0.08);
            widthAnimation.AutoReverse = true;

            DoubleAnimation heightAnimation = new DoubleAnimation();
            heightAnimation.By = -3;
            heightAnimation.Duration = TimeSpan.FromSeconds(0.08);
            heightAnimation.AutoReverse = true;

            x.BeginAnimation(WidthProperty, widthAnimation);
            x.BeginAnimation(HeightProperty, heightAnimation);
        }


    }
}