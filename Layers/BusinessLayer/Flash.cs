using System;

namespace BusinessLayer
{
    public class Flash
    {
        public static string swfTool(string swfLink, int width, int height)
        {
            string flasObje = string.Empty;
            try
            {

            flasObje = string.Format( "<object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/...version=5,0,0,0\" width=\"{1}\" height=\"{2}\"  >" +
         "<param name=\"wmode\" value=\"transparent\" />" +
         "<param name=\"movie\" value=\"{0}\">" +
         "<param name=\"quality\" value=\"high\" />" +
         "<embed src=\"{0}\" quality=\"high\" pluginspage=\"http://www.macromedia.com/shockwave...=ShockwaveFlash\" " +
         "type=\"application/x-shockwave-flash\" width=\"{1}\" height=\"{2}\" wmode=\"transparent\"  >" +
         "</embed>" +
         "</object>", swfLink,width, height);

            }
            catch (Exception)
            {
                throw;
            }
            return flasObje;
        
        }
    }
}
