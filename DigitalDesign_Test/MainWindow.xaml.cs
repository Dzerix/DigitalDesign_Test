using System;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using DigitalDesign_Test.ViewModels;
using DigitalDesign_Test.ViewModels.Base;
namespace DigitalDesign_Test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal GameViewModel viewModel;
        ImageSource imgSourceHorizontal = new BitmapImage(new Uri("pack://application:,,,/Assets/valve_horizontal.png"));
        ImageSource imgSourceVertical = new BitmapImage(new Uri("pack://application:,,,/Assets/valve.png"));
        public MainWindow()
        {
            viewModel = new GameViewModel();
            DataContext = viewModel;
            InitializeComponent();
            
        }


        //Функция для смены картинки рукоятки
        private void RotateImage(Button button)
        {
            Image img = (Image)button.Content;
            if (img.Source == imgSourceHorizontal)
            {
                img.Source = imgSourceVertical;
            }
            else
            {
                img.Source = imgSourceHorizontal;
            }

            //img.Source = new BitmapImage(new Uri("pack://application:,,,/Assets/valve.png"));
        }


        //Функция для поворота рукоятки, изменяет значения в массиве рукояток
        private void MoveLever(Button button)
        {
            RotateImage(button);
            System.Windows.Point point = (System.Windows.Point)button.Tag;

            for (int x = 0; x < viewModel.N; x++)
            {
                viewModel.HandleArray[x, (int)point.Y] ^= 1;
                RotateImage(gridMain.Children.Cast<Button>().First(e => Grid.GetRow(e) == x && Grid.GetColumn(e) == point.Y));
            }
            for (int y = 0; y < viewModel.N; y++)
            {
                viewModel.HandleArray[(int)point.X, y] ^= 1;
                RotateImage(gridMain.Children.Cast<Button>().First(e => Grid.GetRow(e) == point.X && Grid.GetColumn(e) == y));
            }
            viewModel.HandleArray[(int)point.X, (int)point.Y] ^= 1;
        }

  
        private void HandleButton_Click(object sender, RoutedEventArgs e)
        {
            MoveLever((Button)sender);
            var FirstNumber = viewModel.HandleArray[0,0];
            foreach(var Number in viewModel.HandleArray)
            {
                if(Number != FirstNumber)
                {
                    return;
                }
            }
            gridMain.ColumnDefinitions.Clear();
            gridMain.RowDefinitions.Clear();
            gridMain.Children.Clear();
            gridMain.Children.Add(new TextBlock() { TextAlignment = TextAlignment.Center, VerticalAlignment = VerticalAlignment.Center, FontSize = 60, Text = "Победа! Сейф Открыт!" });
        }


        //Функция запутывающая игровое поле, в качестве параетра принимает уровень сложности
        private void ConfuseTheField(int complexity)
        {
            
            Random random = new Random();
            for (int i = 0; i < complexity; i++)
            {
                var x = random.Next(0, viewModel.N);
                Dispatcher.Invoke(() => MoveLever(gridMain.Children.Cast<Button>().First(e => Grid.GetRow(e) == x && Grid.GetColumn(e) == x)), DispatcherPriority.Render);
            }
        }

        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.HandleArray = new int[viewModel.N, viewModel.N];
            viewModel.HandleArray.Initialize();
            gridMain.ColumnDefinitions.Clear();
            gridMain.RowDefinitions.Clear();
            gridMain.Children.Clear();
            int count = 1;
            int N = (int)StartGameButton.Tag;

            for (int i = 0; i < N; i++)
                gridMain.ColumnDefinitions.Add(
                    new ColumnDefinition() );
            
            for (int i = 0; i < N; i++)
                gridMain.RowDefinitions.Add(
                    new RowDefinition() );

            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    Button MyControl1 = new Button();
                    MyControl1.Content = new Image
                    {
                        Source = imgSourceHorizontal,
                        VerticalAlignment = VerticalAlignment.Center,
                        RenderTransform = null
                    };
                    Grid.SetColumn(MyControl1, j);
                    Grid.SetRow(MyControl1, i);
                    MyControl1.Name = "Button" + count.ToString();
                    MyControl1.Click += HandleButton_Click;
                    MyControl1.Tag = new System.Windows.Point(i,j);
                    gridMain.Children.Add(MyControl1);
                    count++;
                }
                
            }
            ConfuseTheField(8); 
        }



    }

    

}
