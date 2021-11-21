using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.SessionState;
using Wordz.Web.Helpers;
using Wordz.Lng;

namespace Wordz.Web.Handlers.Captha
{
    public class GetCaptchaHandler : IHttpHandler, IRequiresSessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            context.NoCache();

            int width = 0;
            int height = 0;

            if (!int.TryParse(context.Request.Params["width"], out width)
                || !int.TryParse(context.Request.Params["height"], out height))
            {
				context.WriteErrorAndExit(CurrentLanguage.WrongWidthAndHeight);
            }
            
            context.Response.ContentType = "image/png";
            var text = generateText();
            var md5 = MD5.Create();
            var hash = CaptchaHelper.GetMd5Hash(md5, text);
            var image = generateCaptcha(text, width, height);
            context.Response.BinaryWrite(imageToBytePng(image));
            context.Session["captch_hash"] = hash;
        }

        public bool IsReusable
        {
            get { return false; }
        }

        private static byte[] imageToBytePng(Image img)
        {
            var byteArray = new byte[0];
            using (var stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                stream.Close();

                byteArray = stream.ToArray();
            }
            return byteArray;
        }

        private string generateText()
        {
            Random rnd = new Random();

            //Сгенерируем текст
            var text = String.Empty;
            string ALF = "12345678901234567890";
            for (int i = 0; i < 5; ++i)
                text += ALF[rnd.Next(ALF.Length)];
            
            return text;
        }

        private Bitmap generateCaptcha(string text, int width, int height)
        {
            var bitmap = new Bitmap(width, height);
            Random rnd = new Random();
            
            //Добавим различные цвета
            Brush[] colors = { Brushes.Black,
                     Brushes.Red,
                     Brushes.RoyalBlue,
                     Brushes.Green };

            Font font = new Font("Arial", 20, GraphicsUnit.Pixel);

            var g = Graphics.FromImage(bitmap);

            var textSize = g.MeasureString(text, font);

            //Вычислим позицию текста
            int Xpos = rnd.Next(0, width - (int)Math.Ceiling(textSize.Width));
            int Ypos = rnd.Next(15, height - (int)Math.Ceiling(textSize.Height));
            
            //Пусть фон картинки будет серым
            g.Clear(Color.White);

            //Нарисуем сгенирируемый текст
            g.DrawString(text,
                         font,
                         colors[rnd.Next(colors.Length)],
                         new PointF(Xpos, Ypos));

            //Добавим немного помех
            //Линии из углов
            g.DrawLine(Pens.Black,
                       new Point(0, 0),
                       new Point(width - 1, height - 1));
            g.DrawLine(Pens.Black,
                       new Point(0, height - 1),
                       new Point(width - 1, 0));
            ////Белые точки
            for (int i = 0; i < width; ++i)
                for (int j = 0; j < height; ++j)
                    if (rnd.Next() % 20 == 0)
                        bitmap.SetPixel(i, j, Color.White);
            
            return bitmap;
        }
    }
}