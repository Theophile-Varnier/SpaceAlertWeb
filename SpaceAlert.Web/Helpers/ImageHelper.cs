using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace SpaceAlert.Web.Helpers
{
    public static class ImageHelper
    {
        /// <summary>
        /// To the base64.
        /// </summary>
        /// <param name="imgUri">The img URI.</param>
        /// <returns></returns>
        public static string ToBase64(string imgUri)
        {
            using (Image image = Image.FromFile(imgUri))
            {
                using (MemoryStream m = new MemoryStream())
                {
                    image.Save(m, image.RawFormat);
                    byte[] imageBytes = m.ToArray();

                    // Convert byte[] to Base64 String
                    return Convert.ToBase64String(imageBytes);
                }
            }
        }
    }
}