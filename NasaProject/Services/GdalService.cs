using OSGeo.GDAL;

namespace NasaProject.Services
{
    public class GdalService
    {
        public GdalService()
        {
            // لازم مرة واحدة في البداية
            Gdal.AllRegister();
        }




    }
}
