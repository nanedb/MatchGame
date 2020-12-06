using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace MatchGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int tenthOfSecondsElapsed;
        int matchesFound;
        Image lastImageClicked;
        bool findingMatch = false;


        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;

            SetUpGame();
        }

        private void SetUpGame()
        {
            tenthOfSecondsElapsed = 0;
            matchesFound = 0;
            YesBtn.Visibility = Visibility.Hidden;
            NoBtn.Visibility = Visibility.Hidden;
            Random random = new Random();

            List<string> animals = new List<string>()
            {
                "\\Animals\\blowfish.png", "\\Animals\\blowfish.png",
                "\\Animals\\bug.png", "\\Animals\\bug.png",
                "\\Animals\\crocodile.png", "\\Animals\\crocodile.png",
                "\\Animals\\elephant.png", "\\Animals\\elephant.png",
                "\\Animals\\hedgehog.png", "\\Animals\\hedgehog.png",
                "\\Animals\\lady-beetle.png", "\\Animals\\lady-beetle.png",
                "\\Animals\\snail.png", "\\Animals\\snail.png",
                "\\Animals\\tiger.png", "\\Animals\\tiger.png",
                "\\Animals\\rhinoceros.png", "\\Animals\\rhinoceros.png",
                "\\Animals\\snake.png", "\\Animals\\snake.png"
            };

            foreach (Image image in mainGrid.Children.OfType<Image>())
            {
                int index = random.Next(animals.Count);
                string nextAnimal = animals[index];
                image.Source = new BitmapImage(new Uri(nextAnimal, UriKind.Relative));
                image.Visibility = Visibility.Visible;
                animals.RemoveAt(index);
            }

            timer.Start();
        }

        private void Image_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Image image = sender as Image;
            if (findingMatch == false)
            {
                image.Visibility = Visibility.Hidden;
                lastImageClicked = image;
                findingMatch = true;
            }

            else if (image.Source.ToString() == lastImageClicked.Source.ToString())
            {
                matchesFound++;
                image.Visibility = Visibility.Hidden;
                findingMatch = false;
            }

            else
            {
                lastImageClicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthOfSecondsElapsed++;
            timeTextBlock.Text = (tenthOfSecondsElapsed / 10F).ToString("0.0 s");
            if (matchesFound == 10)
            {
                timer.Stop();
                timeTextBlock.Text += " - Play again?";
                YesBtn.Visibility = Visibility.Visible;
                NoBtn.Visibility = Visibility.Visible;
            }
        }

        private void YesBtn_Click(object sender, RoutedEventArgs e)
        {
            SetUpGame();
        }

        private void NoBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
