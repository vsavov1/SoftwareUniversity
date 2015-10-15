using System.Runtime.Serialization;
using System.ServiceModel;

namespace DistanceCalculatorSOAPService
{
    [ServiceContract]
    public interface IServiceDistanceCalculator
    {

        [OperationContract]
        float CalcDistance(Point startPoint, Point endpPoint);

    }

    [DataContract]
    public class Point
    {
        [DataMember]
        public int X { get; set; }

        [DataMember]
        public int Y { get; set; }
    }
}
