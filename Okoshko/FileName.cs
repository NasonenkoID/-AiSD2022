﻿using System;
using System.Windows.Forms;



class MainForm : Form
{
    private Button startButton;
    private Label resultLabel;
    private Button speedButton;
    private Button angleButton;

    public MainForm()
    {
        Text = "Angry Birds";
        Size = new System.Drawing.Size(300, 200);

        startButton = new Button();
        startButton.Text = "Начать движение";
        startButton.Location = new System.Drawing.Point(50, 50);
        startButton.Click += StartButton_Click;
        Controls.Add(startButton);

        resultLabel = new Label();
        resultLabel.Text = "";
        resultLabel.Location = new System.Drawing.Point(50, 100);
        Controls.Add(resultLabel);

        speedButton = new Button();
        speedButton.Text = "Скорость";
        speedButton.Location = new System.Drawing.Point(150, 50);
        speedButton.Click += SpeedButton_Click;
        Controls.Add(speedButton);

        angleButton = new Button();
        angleButton.Text = "Угол";
        angleButton.Location = new System.Drawing.Point(150, 80);
        angleButton.Click += AngleButton_Click;
        Controls.Add(angleButton);
    }

    private void StartButton_Click(object sender, EventArgs e)
    {
        double angle = 45;
        double initialVelocity = 20;
        double obstaclePositionX = 100;
        double airResistance = 0.1;

        ProjectileMotion.Collision += HandleCollision;
        ProjectileMotion.CalculateMotion(angle, initialVelocity, obstaclePositionX, airResistance, "output.txt");
    }

    private void SpeedButton_Click(object sender, EventArgs e)
    {
        double initialVelocity;
        if (Double.TryParse(Microsoft.VisualBasic.Interaction.InputBox("Введите начальную скорость (в м/с):"), out initialVelocity))
        {
            MessageBox.Show("Введенная скорость: " + initialVelocity.ToString() + " м/с");
        }
    }

    private void AngleButton_Click(object sender, EventArgs e)
    {
        double angle;
        if (Double.TryParse(Microsoft.VisualBasic.Interaction.InputBox("Введите угол (в градусах):"), out angle))
        {
            MessageBox.Show("Введенный угол: " + angle.ToString() + " градусов");
        }
    }

    private void HandleCollision(object sender, CollisionEventArgs e)
    {
        resultLabel.Text = "Вы выиграли";
    }
}

class Program
{
    [STAThread]
    static void Main()
    {
        Application.Run(new MainForm());
    }
}