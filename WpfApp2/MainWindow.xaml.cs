using Microsoft.Win32;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace MediaPlayerApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer = new DispatcherTimer();
        private DispatcherTimer timeOfHeddin;
        private DispatcherTimer MLeftBClick;
        private Point winLocation;
        private bool isPlaying = false;
        private bool isStop = false;
        private int time;
        private string filename = null;
        private bool fileIsDrop = false;
        private MediaHistory mediafileCash = new MediaHistory();

        /// <summary>
        /// Main Window
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            this.AddHandler
            (
                   Slider.MouseLeftButtonDownEvent,
                   new MouseButtonEventHandler(slider_seek_MouseLeftButtonDown),
                   true
            );

            timer.Tick += new EventHandler(timer_Tick);

            timeOfHeddin = new DispatcherTimer();
            timeOfHeddin.Interval = new TimeSpan(0, 0, 1);
            timeOfHeddin.Tick += new EventHandler(dispatcherTimer_Tick);
            timeOfHeddin.Start();
        }

        /// <summary>
        /// Остановка/воспроизведение медиафайла
        /// </summary>
        private void buttom_play_Click(object sender, EventArgs e)
        {
            button_animation_size(buttom_play);

            if (filename != null)
            {
                if (isStop)
                    img_LikeOrDislike.Visibility = Visibility.Hidden;

                Image buttonImg = new Image();
                if (isPlaying == false)
                {
                    MediaPlayer.Play();
                    buttonImg.Source = new BitmapImage(new Uri("/png/pause.png", UriKind.Relative));
                    buttom_play.Content = buttonImg;
                    buttom_play.ToolTip = "Pause";
                    isPlaying = true;
                }
                else
                {
                    MediaPlayer.Pause();
                    buttonImg.Source = new BitmapImage(new Uri("/png/play.png", UriKind.Relative));
                    buttom_play.Content = buttonImg;
                    buttom_play.ToolTip = "Play";
                    isPlaying = false;
                }
            }
            else
            {
                if (fileIsDrop != true)
                    buttom_menu_fileOpen_Click(sender, e);
            }
        }

        /// <summary>
        /// Стоп воспроизведения медиафайла
        /// </summary>
        private void buttom_stop_Click(object sender, RoutedEventArgs e)
        {
            button_animation_size(buttom_stop);
            isPlaying = false;

            if (filename != null)
            {
                MediaPlayer.Stop();
                Image buttonImg = new Image();
                buttonImg.Source = new BitmapImage(new Uri("/png/play.png", UriKind.Relative));
                buttom_play.Content = buttonImg;
                isPlaying = false;
                MediaPlayer.Position = TimeSpan.Zero;
                MediaPlayer.Play();
                Thread.Sleep(40);
                MediaPlayer.Pause();
                isStop = true;
            }
            else
            {
                if (fileIsDrop != true)
                    buttom_menu_fileOpen_Click(sender, e);
            }
        }

        /// <summary>
        /// Управление слайдером воспроизведения
        /// </summary>
        private void slider_seek_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            double RelativePos = e.GetPosition(slider_seek as IInputElement).X / slider_seek.ActualWidth;
            if (RelativePos >= 0 && RelativePos <= 1 && e.GetPosition(slider_seek as IInputElement).Y >= 0 && e.GetPosition(slider_seek as IInputElement).Y <= slider_seek.ActualHeight)
            {
                TimeSpan ts = new TimeSpan(0, 0, 0, 0, (int)(RelativePos * (slider_seek.Maximum - slider_seek.Minimum)));
                MediaPlayer.Position = ts;
            }
        }

        /// <summary>
        /// Управление слайдером воспроизведения (начало перетаскивания ползунка)
        /// </summary>
        private void slider_seek_DragStarted(object sender, DragStartedEventArgs e)
        {
            timer.Stop();
            if (isPlaying)
            {
                MediaPlayer.Pause();
            }
        }

        /// <summary>
        /// Управление слайдером воспроизведения (конец перетаскивания ползунка)
        /// </summary>
        private void slider_seek_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            timer.Start();
            if (isPlaying)
            {
                MediaPlayer.Play();
            }
            TimeSpan ts = new TimeSpan(0, 0, 0, 0, (int)slider_seek.Value);
            MediaPlayer.Position = ts;
        }

        /// <summary>
        /// Открытие ползунка ргулировки громкости
        /// </summary>
        private void buttom_volume_Click(object sender, RoutedEventArgs e)
        {
            buttom_volume.Visibility = Visibility.Hidden;
            slider_volume.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Изменение громкости медифайла
        /// </summary>
        private void slider_volume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MediaPlayer.Volume = (double)slider_volume.Value;
        }

        /// <summary>
        /// Скрытие слайдера громкости после изменения
        /// </summary>
        private void slider_volume_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            slider_volume.Visibility = Visibility.Hidden;
            buttom_volume.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Переход в полноэкранный режим и наоборот
        /// </summary>
        private void buttom_fullsize_Click(object sender, RoutedEventArgs e)
        {
            Image buttonImg = new Image();
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;

                buttonImg.Source = new BitmapImage(new Uri("/png/full_screen.png", UriKind.Relative));
                buttom_fullsize.Content = buttonImg;
                buttom_fullsize.Width = 25;
                buttom_fullsize.Height = 25;
                buttom_fullsize.Margin = new Thickness(0, 0, 5, 10);
                buttom_fullsize.ToolTip = "Full screen";
            }
            else
            {
                this.WindowState = WindowState.Maximized;
                buttonImg.Source = new BitmapImage(new Uri("/png/normal_screen.png", UriKind.Relative));
                buttom_fullsize.Content = buttonImg;
                buttom_fullsize.Width = 33;
                buttom_fullsize.Height = 33;
                buttom_fullsize.Margin = new Thickness(0, 0, 5, 6);
                buttom_fullsize.ToolTip = "Normal screen";
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            slider_seek.Value = MediaPlayer.Position.TotalMilliseconds;
        }

        /// <summary>
        /// Действия при открытии файла
        /// </summary>
        private void MediaPlayer_MediaOpened(object sender, RoutedEventArgs e)
        {
            panel_like.Visibility = Visibility.Hidden;
            img_LikeOrDislike.Visibility = Visibility.Hidden;
            img_LikeOrDislike.Opacity = 1;
            img_LikeOrDislike.Width = 64;
            img_LikeOrDislike.Height = 64;
            dockPael_MediaElement.Opacity = 1;
            panel_like.Opacity = 1;

            try
            {
                MediaPlayer.Volume = (double)slider_volume.Value;
                slider_seek.Maximum = MediaPlayer.NaturalDuration.TimeSpan.TotalMilliseconds;

                timer.Interval = new TimeSpan(0, 0, 1);
                timer.Start();

                MediaHistory mediafile = new MediaHistory();
                mediafile.Name = System.IO.Path.GetFileName(filename);
                TimeSpan timeSpan = TimeSpan.FromSeconds(Convert.ToInt32(MediaPlayer.NaturalDuration.TimeSpan.TotalSeconds));
                mediafile.Duration = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
                mediafile.Date = DateTime.Now;
                mediafile.Favorite = false;
                mediafile.Path = filename;
                mediafileCash = mediafile;

                using (MediaHistoryContext db = new MediaHistoryContext())
                {
                    db.Media.Add(mediafile);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка воспроизведения файла!" + ex.Message, "MediaPlayerApp", MessageBoxButton.OK, MessageBoxImage.Error);
                fileIsDrop = false;
                filename = null;
                buttom_menu_fileOpen_Click(sender, e);
            }
        }

        /// <summary>
        /// Действия при прекращении воспроизведения файла
        /// </summary>
        private void MediaPlayer_MediaEnded(object sender, RoutedEventArgs e)
        {
            buttom_stop_Click(sender, e);

            panel_like.Visibility = Visibility.Visible;
            img_LikeOrDislike.Visibility = Visibility.Hidden;

            if (!MediaPlayer.HasVideo)
                dockPael_MediaElement.Opacity = 0;

            DoubleAnimation visible = new DoubleAnimation();
            visible.From = 0;
            visible.To = 1;
            visible.Duration = new Duration(TimeSpan.FromSeconds(0.4));

            panel_like.BeginAnimation(OpacityProperty, visible);
        }

        /// <summary>
        /// Таймер на (Перетаскивание окна + остановка воспросизведения файла принажатии левой кнопкой мыши)
        /// </summary>
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if(MLeftBClick == null)
                {
                    MLeftBClick = new DispatcherTimer();
                    MLeftBClick.Interval = new TimeSpan(0, 0, 0, 0, 200);
                    MLeftBClick.Tick += new EventHandler(MouseLeftBClick);
                    MLeftBClick.Start();
                }

                winLocation = PointToScreen(new Point(0, 0));
                this.DragMove();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Функция Window_MouseLeftButtonDown: " + ex.Message);
            }
        }

        /// <summary>
        /// Перетаскивание окна + остановка воспросизведения файла принажатии левой кнопкой мыши
        /// </summary>
        private void MouseLeftBClick(object sender, EventArgs e)
        {
            if(MLeftBClick != null)
                MLeftBClick.Stop();
            MLeftBClick = null;

            if (winLocation == PointToScreen(new Point(0, 0)))
                buttom_play_Click(sender, e);
        }

        /// <summary>
        /// Переход в полноэкранный режим и наоборот при двойном нажатии левой кнопкой мыши
        /// </summary>
        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (MLeftBClick != null)
                MLeftBClick.Stop();
            MLeftBClick = null;
            buttom_fullsize_Click(sender, e);
        }

        /// <summary>
        /// Горячие клавиши
        /// </summary>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.ToString() == "Down" && MediaPlayer.Volume > 0)
            {
                MediaPlayer.Volume -= 0.1;
                slider_volume.Value -= 0.1;
            }
            if (e.Key.ToString() == "Up" && MediaPlayer.Volume < 1)
            {
                MediaPlayer.Volume += 0.1;
                slider_volume.Value += 0.1;
            }
            if (e.Key.ToString() == "Escape")
            {
                if (this.WindowState == WindowState.Maximized)
                {
                    Image buttonImg = new Image();
                    this.WindowState = WindowState.Normal;
                    buttonImg.Source = new BitmapImage(new Uri("/png/full_screen.png", UriKind.Relative));
                    buttom_fullsize.Content = buttonImg;
                    buttom_fullsize.Width = 25;
                    buttom_fullsize.Height = 25;
                    buttom_fullsize.Margin = new Thickness(0, 0, 5, 10);
                }
            }
            if (e.Key.ToString() == "Right")
            {
                TimeSpan ts = new TimeSpan(0, 0, 0, 10);
                MediaPlayer.Position += ts;
            }
            if (e.Key.ToString() == "Left")
            {
                TimeSpan ts = new TimeSpan(0, 0, 0, 10);
                MediaPlayer.Position -= ts;
            }

            if (e.Key.ToString() == "Space")
                buttom_play_Click(sender, e);
        }

        /// <summary>
        /// Действие по истичению 5 секунд воспроизведения медиафайла
        /// </summary>
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            time += 1;
            if (time == 5 && isPlaying)
            {
                slider_volume.Visibility = Visibility.Hidden;
                buttom_volume.Visibility = Visibility.Visible;

                media_panel.Visibility = Visibility.Hidden;

                Image buttonImg = new Image();
                buttonImg.Source = new BitmapImage(new Uri("/png/menu.png", UriKind.Relative));
                buttom_menu.Content = buttonImg;
                menu_panel.Visibility = Visibility.Hidden;

                this.Cursor = Cursors.None;
            }
        }

        /// <summary>
        /// Действаия при движении курсора мыши
        /// </summary>
        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            media_panel.Visibility = Visibility.Visible;
            this.Cursor = Cursors.Arrow;
            time = 0;
        }

        /// <summary>
        /// Закрытие приложения
        /// </summary>
        private void buttom_close_Click(object sender, RoutedEventArgs e)
        {
            isPlaying = false;
            this.Close();
        }

        /// <summary>
        /// Меню приложения
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
        /// Drag'n'Drop файла
        /// </summary>
        private void Window_Drop(object sender, DragEventArgs e)
        {
            filename = (string)((DataObject)e.Data).GetFileDropList()[0];
            MediaPlayer.Source = new Uri(filename);

            this.Background = new SolidColorBrush(Colors.Black);

            MediaPlayer.Volume = (double)slider_volume.Value;
            fileIsDrop = true;
            buttom_play_Click(sender, e);
        }

        /// <summary>
        /// Выбор файла для воспроизведения из меню
        /// </summary>
        public void buttom_menu_fileOpen_Click(object sender, EventArgs e)
        {
            button_animation_size(buttom_menu_fileOpen);

            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Filter = "Видео файлы (*.avi;*.mkv;*.mp4;*.flv;*.mov)|*.avi;*.mkv;*.mp4;*.flv;*.mov|Аудио файлы (*.mp3;*.wav)|*.mp3;*.wav|В се файлы (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;

            if (openFileDialog1.ShowDialog() != null)
            {
                try
                {
                    filename = openFileDialog1.FileName.ToString();
                    if (filename != null)
                    {
                        MediaPlayer.Source = new Uri((string)openFileDialog1.FileName.ToString());

                        this.Background = new SolidColorBrush(Colors.Black);

                        MediaPlayer.Volume = (double)slider_volume.Value;
                        buttom_play_Click(sender, e);
                    }
                }
                catch
                {
                    MessageBox.Show("Файл не выбран!", "MediaPlayerApp", MessageBoxButton.OK, MessageBoxImage.Warning);
                    if (fileIsDrop != true)
                        filename = null;
                }
            }
        }

        /// <summary>
        /// Открыть историю просмотров
        /// </summary>
        private void buttom_menu_history_Click(object sender, RoutedEventArgs e)
        {
            MediaPlayer.Pause();
            button_animation_size(buttom_menu_history);
            History history = new History();
            this.Close();
            history.Show();
        }

        // <summary>
        /// Начальное окно
        /// </summary>
        private void buttom_menu_home_Click(object sender, RoutedEventArgs e)
        {
            MediaPlayer.Stop();
            button_animation_size(buttom_menu_home);
            MainWindow winPlayer = new MainWindow();
            this.Close();
            winPlayer.Show();
        }

        /// <summary>
        /// Открыть избранные медиафайлы
        /// </summary>
        private void buttom_menu_favorite_Click(object sender, RoutedEventArgs e)
        {
            MediaPlayer.Pause();
            button_animation_size(buttom_menu_likes);
            History favorite = new History(true);
            this.Close();
            favorite.Show();
        }

        /// <summary>
        /// Отмечаем медифайл понравшимся и добавляем в избранное
        /// </summary>
        private void buttom_media_like_Click(object sender, RoutedEventArgs e)
        {
            panel_like.Visibility = Visibility.Hidden;
            img_LikeOrDislike.Source = new BitmapImage(new Uri("/png/like_heart.png", UriKind.Relative));
            img_LikeOrDislike.Visibility = Visibility.Visible;

            using (MediaHistoryContext db = new MediaHistoryContext())
            {
                foreach (var mediaFile in db.Media)
                {
                    if (mediaFile.Date.ToString() == mediafileCash.Date.ToString())
                        mediaFile.Favorite = true;
                }
                db.SaveChanges();
            }
            object_animation_size(sender, e);
        }

        /// <summary>
        /// Отмечаем медифайл не понравивееся
        /// </summary>
        private void buttom_media_dislike_Click(object sender, RoutedEventArgs e)
        {
            panel_like.Visibility = Visibility.Hidden;
            img_LikeOrDislike.Source = new BitmapImage(new Uri("/png/dislike_heart.png", UriKind.Relative));
            img_LikeOrDislike.Visibility = Visibility.Visible;

            object_animation_size(sender, e);
        }

        /// <summary>
        /// Анимация нажатия кнопки
        /// </summary>
        public void button_animation_size(Button x)
        {
            DoubleAnimation widthAnimation = new DoubleAnimation();
            widthAnimation.From = x.Width;
            widthAnimation.To = x.Width - 3;
            widthAnimation.Duration = TimeSpan.FromSeconds(0.08);
            widthAnimation.AutoReverse = true;

            DoubleAnimation heightAnimation = new DoubleAnimation();
            widthAnimation.From = x.Height;
            widthAnimation.To = x.Height - 3;
            heightAnimation.Duration = TimeSpan.FromSeconds(0.08);
            heightAnimation.AutoReverse = true;

            x.BeginAnimation(WidthProperty, widthAnimation);
            x.BeginAnimation(HeightProperty, heightAnimation);
        }

        /// <summary>
        /// Выскакивает лайк или дизлайк
        /// </summary>
        private void object_animation_size(object sender, EventArgs e)
        {
            DoubleAnimation widthAnimation = new DoubleAnimation();
            widthAnimation.From = 0;
            widthAnimation.To = 80;
            widthAnimation.Duration = TimeSpan.FromSeconds(0.2);

            DoubleAnimation heightAnimation = new DoubleAnimation();
            heightAnimation.From = 0;
            heightAnimation.To = 80;
            heightAnimation.Duration = TimeSpan.FromSeconds(0.2);
            heightAnimation.Completed += object_animation_size_Completed;

            img_LikeOrDislike.BeginAnimation(WidthProperty, widthAnimation);
            img_LikeOrDislike.BeginAnimation(HeightProperty, heightAnimation);
        }

        private void object_animation_size_Completed(object sender, EventArgs e)
        {
            DoubleAnimation widthAnimation = new DoubleAnimation();
            widthAnimation.From = 80;
            widthAnimation.To = 64;
            widthAnimation.Duration = TimeSpan.FromSeconds(0.2);

            DoubleAnimation heightAnimation = new DoubleAnimation();
            heightAnimation.From = 80;
            heightAnimation.To = 64;
            heightAnimation.Duration = TimeSpan.FromSeconds(0.2);
            heightAnimation.Completed += object_animation_size_Completed_End;

            img_LikeOrDislike.BeginAnimation(WidthProperty, widthAnimation);
            img_LikeOrDislike.BeginAnimation(HeightProperty, heightAnimation);
        }                                                                                                                                                                                                                                                                  //грязные хуи   

        private void object_animation_size_Completed_End(object sender, EventArgs e)
        {
            DoubleAnimation hidden = new DoubleAnimation();
            hidden.From = 1;
            hidden.To = 0;
            hidden.Duration = new Duration(TimeSpan.FromSeconds(0.4));
            hidden.AutoReverse = true;

            img_LikeOrDislike.BeginAnimation(OpacityProperty, hidden);
        }

        public void historyMediaOpen(object sender, RoutedEventArgs e, string path)
        {
            filename = path;
            MediaPlayer.Source = new Uri((string)path);

            this.Background = new SolidColorBrush(Colors.Black);

            MediaPlayer.Volume = (double)slider_volume.Value;
            buttom_play_Click(sender, e);
        }



    }
}