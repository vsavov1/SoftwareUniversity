using System;

namespace DistanceCalculatorSOAPService
{
    public class Service1 : IServiceDistanceCalculator
    {
        public float CalcDistance(Point startPoint, Point endpPoint)
        {
            return (float)Math.Sqrt((startPoint.X - endpPoint.X) * (startPoint.X - endpPoint.X) +
                   (startPoint.Y - endpPoint.Y) * (startPoint.Y - endpPoint.Y));
        }
    }
}
