using Kaptcha.Model;
using Kaptcha.Utility;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Linq;

namespace Kaptcha.Service
{
    public class KaptchaService
    {
        private bool UseNumber { get; set; } = true;
        private bool persianNumber { get; set; } = false;
        private int firstNumberStartRange { get; set; } = 10;
        private int firstNumberEndRange { get; set; } = 90;
        private int secondNumberStartRange { get; set; } = 0;
        private int secondNumberEndRange { get; set; } = 9;
        private int height { get; set; } = 64;
        private int width { get; set; } = 306;
        private string fontPath { get; set; }
        private string fontFolderPath { get; set; }
        private int fontSize { get; set; } = 36;
        private int hardDegree { get; set; } = 1;
        private bool isNoisy { get; set; } = false;

        internal void UseCustomNumbers(int firstNumberStartRange, int firstNumberEndRange, int secondNumberStartRange, int secondNumberEndRange)
        {
            UseNumber = true;

            this.firstNumberStartRange = firstNumberStartRange;
            this.firstNumberEndRange = firstNumberEndRange;

            this.secondNumberStartRange = secondNumberStartRange;
            this.secondNumberEndRange = secondNumberEndRange;
        }

        internal void UseCustomizeSize(int height, int width)
        {
            this.height = height;
            this.width = width;
        }

        internal void UseFonthPath(string fontPath)
        {
            this.fontPath = fontPath;
        }

        internal void UseFontFolderPath(string fontFolderPath)

        {
            this.fontFolderPath = fontFolderPath;
        }

        internal void UseCustomizeFontSize(int fontSize)
        {
            this.fontSize = fontSize;
        }

        internal void UsePersianNumbers()
        {
            this.persianNumber = true;
        }

        internal void UseNoisy()
        {
            this.isNoisy = true;
        }

        internal void Mode(Enums.Mode mode)
        {
            this.hardDegree = (int)mode;
        }

        internal KaptchaResult GenerateKaptcha()
        {
            try
            {
                var rand = new Random((int)DateTime.Now.Ticks);

                string a = rand.Next(firstNumberStartRange, firstNumberEndRange).ToString();
                string b = rand.Next(secondNumberStartRange, secondNumberEndRange).ToString();


                string captcha = "";

                if (persianNumber)
                {
                    captcha = string.Format("{0} + {1} = ?", Util.PersianToEnglish(a), Util.PersianToEnglish(b));
                }
                else
                {
                    captcha = string.Format("{0} + {1} = ?", a, b);
                }

                KaptchaFile kaptchaFile = new KaptchaFile();

                using (var mem = new MemoryStream())
                using (var bmp = new Bitmap(width, height))
                using (var gfx = Graphics.FromImage((Image)bmp))
                {
                    gfx.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                    gfx.SmoothingMode = SmoothingMode.AntiAlias;
                    gfx.FillRectangle(Brushes.White, new Rectangle(0, 0, bmp.Width, bmp.Height));

                    //add noise
                    if (isNoisy)
                    {
                        int i, r, x, y;
                        int j = 10;
                        var pen = new Pen(Color.Yellow);

                        switch (hardDegree)
                        {
                            case 1:
                                j = 5;
                                break;
                            case 2:
                                j = 10;
                                break;
                            case 3:
                                j = 20;
                                break;
                            case 4:
                                j = 50;
                                break;
                            default:
                                j = 11;
                                break;
                        }

                        for (i = 1; i < j; i++)
                        {
                            pen.Color = Color.FromArgb(
                            (rand.Next(0, 255)),
                            (rand.Next(0, 255)),
                            (rand.Next(0, 255)));

                            r = rand.Next(0, (width / 2));
                            x = rand.Next(0, width);
                            y = rand.Next(50, height);

                            gfx.DrawEllipse(pen, x - r, y - r, r, r);

                            gfx.DrawEllipse(pen, 10, 10, 10, 10);

                            gfx.DrawEllipse(pen, new Rectangle(0, 0, bmp.Width / 2, bmp.Height / 2));

                            gfx.DrawLine(pen, rand.Next(0, 30), rand.Next(10, 306), rand.Next(50, 3006), rand.Next(200, 306));
                            gfx.DrawLine(pen, rand.Next(0, 30), rand.Next(10, 306), rand.Next(50, 3006), rand.Next(200, 306));
                            gfx.DrawLine(pen, rand.Next(0, 30), rand.Next(10, 306), rand.Next(50, 3006), rand.Next(200, 306));
                            gfx.DrawLine(pen, rand.Next(0, 30), rand.Next(10, 306), rand.Next(50, 3006), rand.Next(200, 306));

                        }
                    }

                    Font font = null;
                    PrivateFontCollection collection = new PrivateFontCollection();
                    if (fontFolderPath != null)
                    {
                        // Read Fonts And Pick Random One !

                        Random rnd = new Random();
                        string[] filePaths = Directory.GetFiles(fontFolderPath, "*.ttf",
                                             SearchOption.TopDirectoryOnly);

                        int filePathId = rnd.Next(filePaths.Length);

                        collection.AddFontFile(filePaths[filePathId]);

                        font = new Font(collection.Families.First(), fontSize);

                    }
                    else if (fontPath != null)
                    {
                        // Read User Defiend Fonts

                        collection.AddFontFile(fontPath);

                        font = new Font(collection.Families.First(), fontSize);
                    }
                    else
                    {
                        // No Fonts !

                        // TODO : Read From Custom PreDefiend Fonts !
                    }



                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Center;
                    stringFormat.LineAlignment = StringAlignment.Center;

                    if(font != null)
                    {
                        gfx.DrawString(captcha, font, Brushes.Gray, new Rectangle(0, 0, bmp.Width, bmp.Height), stringFormat);
                    }
                    else
                    {
                        gfx.DrawString(captcha, new Font(FontFamily.GenericSansSerif, 24, FontStyle.Regular), Brushes.Gray, new Rectangle(0, 0, bmp.Width, bmp.Height), stringFormat);
                    }
                  

                    bmp.Save(mem, System.Drawing.Imaging.ImageFormat.Jpeg);

                    kaptchaFile.FileContents = mem.GetBuffer();
                    kaptchaFile.ContentType = "image/Jpeg";
                    kaptchaFile.FileName = "Captcha.jpg";
                }

                return new KaptchaResult
                {
                    KaptchaSumResult = Int32.Parse(a) + Int32.Parse(b),
                    ImageFile = kaptchaFile,
                    Message="Kaptcha Generated Succsessfully !"
                };
            }
            catch (Exception ex)
            {
                return new KaptchaResult
                {
                    KaptchaSumResult = -1,
                    ImageFile = null,
                    Message = ex.Message
                };
            }

        }
    }
}
