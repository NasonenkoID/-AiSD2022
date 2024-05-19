using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Threading.Tasks;

namespace AngryBirds

{
    public partial class MainWindow : Window
    {
        private Label resultLabel;
        private double obstaclePositionX;
        private double initialVelocity = 0;
        private double angle = 0;
        private double airResistance = 0.1;
        private Canvas canvas;

        public MainWindow()
        {
            InitializeComponent();
        }

        [STAThread]
        public static void Main()
        {
            Application app = new Application();
            MainWindow mainWindow = new MainWindow();
            app.Run(mainWindow);
        }

        private void InitializeComponent()
        {
            StackPanel stackPanel = new StackPanel();

            Button startButton = new Button();
            startButton.Content = "Старт";
            startButton.Click += StartButton_Click;
            startButton.Width = 200;
            startButton.Height = 100;
            startButton.HorizontalAlignment = HorizontalAlignment.Center;
            stackPanel.Children.Add(startButton);

            resultLabel = new Label();
            stackPanel.Children.Add(resultLabel);

            Button speedButton = new Button();
            speedButton.Content = "Скорость";
            speedButton.Click += SpeedButton_Click;
            stackPanel.Children.Add(speedButton);

            Button angleButton = new Button();
            angleButton.Content = "Угол";
            angleButton.Click += AngleButton_Click;
            stackPanel.Children.Add(angleButton);

            Button obstaclePositionXButton = new Button();
            obstaclePositionXButton.Content = "Координата препятствия по X";
            obstaclePositionXButton.Click += ObstaclePositionXButton_Click;
            stackPanel.Children.Add(obstaclePositionXButton);

            resultLabel = new Label();
            stackPanel.Children.Add(resultLabel);

            canvas = new Canvas();
            stackPanel.Children.Add(canvas);

            this.Content = stackPanel;
        }
        private void HandleTrajectoryUpdate(double positionX, double positionY)
        {
            Ellipse point = new Ellipse();
            point.Fill = Brushes.Red;
            point.Width = 5;
            point.Height = 5;
            Canvas.SetLeft(point, positionX * 10);
            Canvas.SetTop(point, canvas.ActualHeight - positionY * 10);
            canvas.Children.Add(point);
        }


        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            ProjectileMotion.TrajectoryUpdate += HandleTrajectoryUpdate;
            ProjectileMotion.Collision += HandleCollision;
            await Task.Run(() => ProjectileMotion.CalculateMotion(angle, initialVelocity, obstaclePositionX, airResistance, "output.txt"));
        }

        private void SpeedButton_Click(object sender, RoutedEventArgs e)
        {
            if (Double.TryParse(Microsoft.VisualBasic.Interaction.InputBox("Введите начальную скорость (в м/с):"), out initialVelocity))
            {
                MessageBox.Show("Введенная скорость: " + initialVelocity.ToString() + " м/с");
            }
        }

        private void AngleButton_Click(object sender, RoutedEventArgs e)
        {
            if (Double.TryParse(Microsoft.VisualBasic.Interaction.InputBox("Введите угол (в градусах):"), out angle))
            {
                MessageBox.Show("Введенный угол: " + angle.ToString() + " градусов");
            }
        }

        private void ObstaclePositionXButton_Click(object sender, RoutedEventArgs e)
        {
            if (Double.TryParse(Microsoft.VisualBasic.Interaction.InputBox("Введите координату препятствия по X (в м):"), out obstaclePositionX))
            {
                MessageBox.Show("Введенная координата препятствия по X: " + obstaclePositionX.ToString() + " м");
            }
        }

        private void HandleCollision(object sender, CollisionEventArgs e)
        {
            if (e.ReachedObstacle)
            {
                resultLabel.Content = "Вы выиграли";
            }
            else
            {
                resultLabel.Content = "Вы проиграли. Тело не достигло препятствия.";
            }
        }

    }
}
       


    