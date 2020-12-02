using Algonia.ML;
using System;

namespace ObjectDetectionTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ObjectDetection objectDetection = new ObjectDetection("assets/images","assets/images/output");
            objectDetection.Detect();
        }
    }
}
