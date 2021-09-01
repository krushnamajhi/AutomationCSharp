using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public class ImageUtil
    {

        private Image OpenImage(string ImageFilePath)
        {
            FileStream fs = new FileStream(ImageFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            return Image.FromStream(fs);
        }


        public String ConvertImageToBase64String(String Path)
        {
            using (Image image = Image.FromFile(Path))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();

                    // Convert byte[] to Base64 String
                    string base64String = Convert.ToBase64String(imageBytes);
                    return base64String;
                }
            }
        }


        public String ReduceImageSize_ConvertToBase64(String Path, double reducedTo)
        {
            try
            {
                using (Image source = OpenImage(Path))
                {
                    var AbortCallback = new Image.GetThumbnailImageAbort(ThumbnailCallback);
                    Image thumbnail = source.GetThumbnailImage((int)(source.Width * reducedTo), (int)(source.Height * reducedTo), AbortCallback, IntPtr.Zero);
                    using (var memory = new MemoryStream())
                    {
                        thumbnail.Save(memory, source.RawFormat);

                        string base64String = Convert.ToBase64String(memory.ToArray());
                        return base64String;
                    }
                }
            }
            catch (Exception ex)
            {
                //logger.Error("Error on Convertinng File : " + Path);
                //logger.Error("Error:" + ex);
            }
            return "";
        }


        public List<Rectangle> SplitScreenToRectangles(int totalHeight, int viewportHeight, int totalWidth, int viewportWidth)
        {
            var rectangles = new List<System.Drawing.Rectangle>();
            // Loop until the totalHeight is reached
            for (var y = 0; y < totalHeight; y += viewportHeight)
            {
                try
                {
                    var newHeight = viewportHeight;
                    // Fix if the height of the element is too big
                    if (y + viewportHeight > totalHeight)
                    {
                        newHeight = totalHeight - y;
                    }
                    // Loop until the totalWidth is reached
                    for (var x = 0; x < totalWidth; x += viewportWidth)
                    {
                        var newWidth = viewportWidth;
                        // Fix if the Width of the Element is too big
                        if (x + viewportWidth > totalWidth)
                        {
                            newWidth = totalWidth - x;
                        }
                        // Create and add the Rectangle
                        var currRect = new System.Drawing.Rectangle(x, y, newWidth, newHeight);
                        rectangles.Add(currRect);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message); 
                    //logger.Error(e.Message);
                    //logger.Error(e.StackTrace);
                    //logger.Error(e.InnerException);
                    throw e;
                }
            }
            return rectangles;
        }


        public void SaveImage(Image image, String ScreenshotPath, ImageFormat imageFormat)
        {
            Bitmap NewImage = new Bitmap(image);
            try
            {
                using (MemoryStream memory = new MemoryStream())
                {
                    NewImage.Save(memory, ImageFormat.Png);
                    using (FileStream imageStream = File.Create(ScreenshotPath))
                    {
                        memory.WriteTo(imageStream);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                image.Dispose();
                NewImage.Dispose();
            }
        }

        private static bool ThumbnailCallback()
        {
            return false;
        }

    }
}
