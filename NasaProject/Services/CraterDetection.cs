using NasaProject.DTOs;
using OpenCvSharp;
using System.Collections.Generic;

public class CraterDetection
{

    //  public static List<(int x, int y, int radius)> FindCraters(string imagePath)
    public static List<CraterDTO> FindCraters(string imagePath)
    {
        // 1. Read image
        using var src = Cv2.ImRead(imagePath);

        // 2. Convert to grayscale
        using var gray = new Mat();
        Cv2.CvtColor(src, gray, ColorConversionCodes.BGR2GRAY);

        // 3. Blur to reduce noise
        Cv2.MedianBlur(gray, gray, 5);

        // 4. Detect circles (similar parameters as Python)
        CircleSegment[] circles = Cv2.HoughCircles(
            image: gray,
            method: HoughModes.Gradient,
            dp: 1.4,
            minDist: 175,
            param1: 70,
            param2: 44,
            minRadius: 8,
            maxRadius: 100
        );



        var results = new List<CraterDTO>();
        if (circles != null)
        {
            foreach (var c in circles)
            {
                results.Add(new CraterDTO
                {
                    X = (int)c.Center.X,
                    Y = (int)c.Center.Y,
                    Radius = (int)c.Radius
                });
            }
        }
        return results;


        // 5. Prepare results
        //var results = new List<(int x, int y, int radius)>();
        //if (circles != null)
        //{
        //    foreach (var c in circles)
        //    {
        //        results.Add(((int)c.Center.X, (int)c.Center.Y, (int)c.Radius));
        //    }
        //}

        //return results;
    }
}
