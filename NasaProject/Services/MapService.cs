using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Drawing;

namespace NasaProject.Services
{
    public class MapService
    {
        private readonly string _mapPath;
        private readonly string _tilesOutputPath;

        public MapService(IConfiguration configuration)
        {
            _mapPath = configuration["MapSettings:BaseMapPath"];
            _tilesOutputPath = configuration["MapSettings:TilesOutputPath"];
        }



        public void GenerateTiles()
        {
            var psi = new ProcessStartInfo
            {
                FileName = "python",
                Arguments = $"\"C:/Program Files/GDAL/gdal2tiles.py\" \"{_mapPath}\" \"{_tilesOutputPath}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };




            using var process = new Process { StartInfo = psi };
            process.Start();

            string output = process.StandardOutput.ReadToEnd();
            string errors = process.StandardError.ReadToEnd();

            process.WaitForExit();

            Console.WriteLine("Output: " + output);
            Console.WriteLine("Errors: " + errors);

        }





            public string GetMapPath()
        {
            return _mapPath;
        }

        public Bitmap ClipMap(Rectangle clipArea)
        {
            using var baseMap = new Bitmap(_mapPath);
            var clipped = baseMap.Clone(clipArea, baseMap.PixelFormat);
            return clipped;
        }
    }
    }

