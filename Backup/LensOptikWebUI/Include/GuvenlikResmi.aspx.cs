using System;
using System.Collections;
using System.Web;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;

public partial class include_guvenlik_resmi : System.Web.UI.Page
{
    protected void Page_Load( object sender , EventArgs e )
    {


        Bitmap objBMP =new System.Drawing.Bitmap(85,23);
        Graphics objGraphics = System.Drawing.Graphics.FromImage(objBMP);
        objGraphics.Clear(Color.LightGray);

        objGraphics.TextRenderingHint = TextRenderingHint.AntiAlias;

        //' Configure font to use for text

        Font objFont = new  Font("Verdana", 11, FontStyle.Bold);
        string randomStr="";
        int[] myIntArray = new int[5] ;
        int x;

        //That is to create the random # and add it to our string

        Random autoRand = new Random();

        for (x=0;x<5;x++)
        {
            myIntArray[x] =  System.Convert.ToInt32 (autoRand.Next(0,9));
            randomStr+= (myIntArray[x].ToString ());
        }

        //This is to add the string to session cookie, to be compared later

        Session.Add("randomStr",randomStr);

        //' Write out the text

        objGraphics.DrawString(randomStr, objFont, Brushes.Gray,  10, 3);

        //' Set the content type and return the image

        Response.ContentType = "image/GIF";
        objBMP.Save(Response.OutputStream, ImageFormat.Gif);

        objFont.Dispose();
        objGraphics.Dispose();
        objBMP.Dispose();
  

    }
}