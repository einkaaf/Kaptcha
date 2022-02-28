using Kaptcha.Model;
using Kaptcha.Service;

namespace Kaptcha.Builder
{
    public class KaptchaBuilder
    {
        private KaptchaService kaptchaService = new KaptchaService();

        private KaptchaBuilder()
        {

        }
        public static KaptchaBuilder Init()
        {
            return new KaptchaBuilder();
        }
        public KaptchaResult Build()
        {
            return kaptchaService.GenerateKaptcha();
        }

        public KaptchaBuilder UseCustomNumbers(int firstNumberStartRange, int firstNumberEndRange, int secondNumberStartRange, int secondNumberEndRange)
        {
            kaptchaService.UseCustomNumbers(firstNumberStartRange, firstNumberEndRange, secondNumberStartRange, secondNumberEndRange);
            return this;
        }
        public KaptchaBuilder UseCustomizeSize(int height, int width)
        {
            kaptchaService.UseCustomizeSize(height, width);
            return this;
        }
        public KaptchaBuilder UseCustomFont(string fontPath)
        {
            kaptchaService.UseFonthPath(fontPath);
            return this;
        }
        public KaptchaBuilder UseCustomFontFolder(string folderPath)
        {
            kaptchaService.UseFontFolderPath(folderPath);
            return this;
        }
        public KaptchaBuilder UseCustomizeFontSize(int fontSize)
        {
            kaptchaService.UseCustomizeFontSize(fontSize);
            return this;
        }

        public KaptchaBuilder UsePersianNumbers()
        {
            kaptchaService.UsePersianNumbers();
            return this;
        }
        public KaptchaBuilder UseNoisy()
        {
            kaptchaService.UseNoisy();
            return this;
        }

        public KaptchaBuilder Mode(Enums.Mode mode)
        {
            kaptchaService.Mode(mode);
            return this;
        }
    }
}