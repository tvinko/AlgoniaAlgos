using Algonia.ML;
using Newtonsoft.Json.Linq;
using System;

namespace ObjectDetectionTest
{
    class Program
    {
        static void Main(string[] args)
        {
            ObjectDetection objectDetection = new ObjectDetection("assets/images","assets/images/output");
            foreach (var detections in JArray.Parse(objectDetection.Detect()))
            {
                Console.WriteLine(detections["FileName"].ToString());
                Console.WriteLine("---------------------------------");
                foreach (var detectedObject in JArray.Parse(detections["DetectedObjects"].ToString()))
                {
                    Console.WriteLine(detectedObject["Label"] + " : " + detectedObject["Confidence"]);
                }
                Console.WriteLine();       
            }
        }
    }
}
