using System;
using System.IO;

class ProjectileMotion
{
    public delegate void CollisionEventHandler(object sender, CollisionEventArgs e);
    public static event CollisionEventHandler Collision;



    public static void CalculateMotion(double angle, double initialVelocity, double obstaclePositionX, double airResistance, string outputFileName)
    {
        double angleInRadians = angle * Math.PI / 180;
        double velocityX = initialVelocity * Math.Cos(angleInRadians);
        double velocityY = initialVelocity * Math.Sin(angleInRadians);

        double time = 0;
        double positionX = 0;
        double positionY = 0;

        double timeStep = 0.01;

        using (StreamWriter writer = new StreamWriter(outputFileName))
        {
            while (positionX <= obstaclePositionX && positionY >= 0)
            {
                double accelerationX = -airResistance * velocityX / initialVelocity;
                double accelerationY = -9.81 - airResistance * velocityY / initialVelocity;

                double velocityXNew = velocityX + accelerationX * timeStep;
                double velocityYNew = velocityY + accelerationY * timeStep;

                positionX = positionX + velocityX * timeStep + 0.5 * accelerationX * timeStep * timeStep;
                positionY = positionY + velocityY * timeStep + 0.5 * accelerationY * timeStep * timeStep;

                velocityX = velocityXNew;
                velocityY = velocityYNew;

                if (positionX <= obstaclePositionX && positionY >= 0)
                {
                    writer.WriteLine($"Время: {time:F2} сек, Координата X: {positionX:F2} м, Координата Y: {positionY:F2} м, Скорость: {initialVelocity:F2} м/с");
                }
                else if (positionX > obstaclePositionX)
                {
                    writer.WriteLine($"Препятствие достигнуто на координате X: {positionX:F2} м.");
                    Collision?.Invoke(null, new CollisionEventArgs(positionX, positionY));
                    break;
                }
                else if (positionY < 0)
                {
                    writer.WriteLine("Тело упало на землю.");
                    break;
                }

                time += timeStep;


            }
        }
    }
}



public class CollisionEventArgs : EventArgs
{

    public bool ReachedObstacle { get; set; }
    public CollisionEventArgs(bool reachedObstacle)
    {
        ReachedObstacle = reachedObstacle;
    }

    public double PositionX { get; }
    public double PositionY { get; }

    public CollisionEventArgs(double x, double y)
    {
        PositionX = x;
        PositionY = y;
    }
}
