namespace Minate.Controllers
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;

    using System.Web.Mvc;

    using Extensions;

    [HandleError]
    public class SecurityController : Controller
    {
        private const int Width = 250;
        private const int Height = 75;
        private const string Font = "Calibri";
        private readonly static Brush Foreground = Brushes.Teal;
        private static readonly Color Background = Color.DarkMagenta;

        public void Captcha(string challengeGuid)
        {
            var key = HtmlHelperExtensions.SessionKeyPrefix + challengeGuid;
            var solution = (string) HttpContext.Session[key];

            if (solution == null) 
                return;

            using (var bmp = new Bitmap(Width, Height))
            using (var g = Graphics.FromImage(bmp))
            using (var font = new Font(Font, 1f))
            {
                g.Clear(Background);

                SizeF finalSize;
                var testSize = g.MeasureString(solution, font);
                var bestFontSize = Math.Min(Width/testSize.Width, Height/testSize.Height)*0.95f;

                using (var finalFont = new Font(Font, bestFontSize))
                    finalSize = g.MeasureString(solution, finalFont);

                g.PageUnit = GraphicsUnit.Point;
                var textTopLeft = new PointF((Width - finalSize.Width)/2, (Height - finalSize.Height)/2);
                using(var path = new GraphicsPath())
                {
                    path.AddString(solution, new FontFamily(Font), 0, bestFontSize, textTopLeft, StringFormat.GenericDefault);
                    g.SmoothingMode = SmoothingMode.HighSpeed;
                    g.FillPath(Foreground, path);
                    g.Flush();

                    Response.ContentType = "image/gif";
                    bmp.Save(Response.OutputStream, ImageFormat.Gif);
                }
            }
        }
    }
}
