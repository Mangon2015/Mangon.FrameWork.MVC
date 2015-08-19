using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mangon.FrameWork.Package.Img
{
    /// <summary>
    /// 输出校验码图片
    /// </summary>
    public class ValidateCodeImg
    {
        /// <summary>
        /// 配置
        /// </summary>
        public Config config;
        public void Random(int length)
        {
            string[] charArray = config.allChar.Split(',');
            int temp = -1;
            Random randCode = new Random();
            for (int i = 0; i < length; i++)
            {
                if (temp != -1)
                {
                    int seed = 1;
                    int.TryParse(DateTime.Now.Ticks.ToString().Substring(2, 8), out seed);
                    randCode = new Random(i * temp * seed);
                }
                int t = randCode.Next(charArray.Length);
                if (temp == t)
                {
                    Random(length);
                }
                temp = t;
                config.validateCode += charArray[temp];
            }
            if (config.validateCode.Length > length)
            {
                config.validateCode = config.validateCode.Remove(length);
            }
        }
        private static Random _randoms;
        private int RandomZoom(int baseRatation)
        {
            int code = _randoms.Next();
            code = _randoms.Next(config._zoomMinPersent + baseRatation, config._zoomMaxPersent + baseRatation);
            return code;
        }
        private int RandomRotation(int baseRotation)
        {
            int code = _randoms.Next();
            if (config._rotateAngleMax > 0 || config._RotateAngleMin > 0)
            {
                code = _randoms.Next(config._RotateAngleMin + baseRotation, config._rotateAngleMax + baseRotation);
            }
            return code;
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="config"></param>
        public ValidateCodeImg(Config config)
        {
            this.config = config;
            int seed = 1;
            int.TryParse(DateTime.Now.Ticks.ToString().Substring(2, 8), out seed);
            _randoms = new Random(seed);
        }
        /// <summary>
        /// 提取图片
        /// </summary>
        /// <returns></returns>
        public Bitmap GetImg()
        {
            Bitmap bmp;
            CreateImageBmp(out bmp);
            MiscellaneousBmp(ref bmp);
            return bmp;
        }
        /// <summary>
        /// 生成一帧的BMP图象
        /// </summary>
        /// <param name="imageFrame"></param>
        private void CreateImageBmp(out Bitmap imageFrame)
        {
            //获取验证码字符串
            char[] codeCharArray = config.validateCode.ToCharArray();
            //图像的宽度与验证码的长度成一定比例
            if (config.with == -1)
            {
                config.with = (int)(codeCharArray.Length * config.validateCodeSize.Max() * 1.3 + 4);
            }
            //创建一个长度20，宽iwidth的图像对象
            imageFrame = new Bitmap(config.with, config.height);
            //创建一个新绘图对象
            Graphics imageGraphice = Graphics.FromImage(imageFrame);
            //清除背景色，并填充背景色
            imageGraphice.Clear(Color.White);
            //字体高度
            int fontHeight = (int)Math.Max(config.height - config.validateCodeSize.Max() - 3, 2);
            Random rand = new Random();

            for (int i = 0; i < codeCharArray.Length; i++)
            {
                int[] fontCoordinate = new int[2];
                int findex = rand.Next(config.validateCodeFont.Length);
                int cindex = rand.Next(config.validateCodeColor.Length);
                int iindex = rand.Next(config.ImageBrush.Length);
                int sindex = rand.Next(config.validateCodeSize.Length);

                fontCoordinate[0] = (int)(i * config.validateCodeSize[sindex] + rand.Next(1)) + 3;
                fontCoordinate[1] = rand.Next(fontHeight);
                Point fontDrawPoint = new Point(fontCoordinate[0], fontCoordinate[1]);
                //消除锯齿操作
                if (config.FontTextRenderingHint)
                {
                    imageGraphice.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixel;
                }
                else
                {
                    imageGraphice.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                }

                using (Font codeFont = new Font(config.validateCodeFont[findex], config.validateCodeSize[sindex], FontStyle.Bold))
                {
                    Color c = Color.FromArgb(config.validateCodeAlpha, config.validateCodeColor[cindex].R, config.validateCodeColor[cindex].G, config.validateCodeColor[cindex].B);
                    #region
                    using (Brush ImageBrush = new SolidBrush(c))
                    {
                        //ImageGraphics.ResetTransform();
                        ////格式化刷子属性-用指定的刷子、颜色等在指定的范围内画图
                        ////设置缩放
                        //float zoomX = 0;
                        //float zoomY = 0;
                        //if (config.ZoonMinPersent != 0 || config.ZoomMaxPersent != 0)
                        //{
                        //    zoomX = ((float)RandomZoom(0)) / 100;
                        //    zoomY = ((float)RandomZoom(0)) / 100;
                        //    if (zoomY != 0 && zoomX != 0)
                        //    {
                        //        ImageGraphics.ScaleTransform(zoomX, zoomY, System.Drawing.Drawing2D.MatrixOrder.Prepend);

                        //    }
                        //}

                        ////设置旋转
                        //int role = 0;
                        //if (config.RotateAngleMax != 0 || config.RotateAngleMin != 0)
                        //{

                        //    role = RandomRotation(0);


                        //    ImageGraphics.RotateTransform(role);


                        //}

                        //   if (FontDrawPoint.X > config.width)
                        //  {
                        //     FontDrawPoint.X = FontDrawPoint.X - config.width;
                        // }
                        if (fontDrawPoint.Y > config.height)
                        {
                            fontDrawPoint.Y = fontDrawPoint.Y - config.height;
                        }
                        imageGraphice.DrawString(codeCharArray[i].ToString(), codeFont, ImageBrush, fontDrawPoint.X, fontDrawPoint.Y);
                        //if (config.RotateAngleMax != 0 || config.RotateAngleMin != 0)
                        //{
                        //    ImageGraphics.RotateTransform(-role);
                        //}
                        //if (zoomY != 0 && zoomX != 0)
                        //{
                        //    if (zoomY != 0 && zoomX != 0)
                        //    {
                        //        ImageGraphics.ScaleTransform(-zoomX, -zoomY);

                        //    }

                        //}
                        //ImageGraphics.TranslateTransform(FontDrawPoint.X,FontDrawPoint.Y);

                    }
                    #endregion
                }
            } //end for
            imageGraphice.Dispose();
        }
        /// <summary>
        /// 处理生成的BMP
        /// </summary>
        /// <param name="imageFrame"></param>
        public void MiscellaneousBmp(ref Bitmap imageFrame)
        {
            ///创建绘图对象
            Graphics imageGraphics = Graphics.FromImage(imageFrame);
            //创建随机对象
            Random rand = new Random();
            //创建随机点
            Point[] randPoint = new Point[2];

            if (config.msicType==MiscType.线||config.msicType==MiscType.混合)
            {
                for (int i = 0; i < config.miscCount; i++)
                {
                    randPoint[0] = new Point(rand.Next(imageFrame.Width), rand.Next(imageFrame.Height));
                    randPoint[1] = new Point(rand.Next(imageFrame.Width), rand.Next(imageFrame.Height));
                    int cindex = rand.Next(config.miscColor.Length);
                    Color c = Color.FromArgb(config.miscAlpha, config.miscColor[cindex].R, config.miscColor[cindex].G, config.miscColor[cindex].B);
                    using (var pen=new Pen(c,config.miscWidth))
                    {
                        imageGraphics.DrawLine(pen, randPoint[0], randPoint[1]);
                    }
                }
            } //end if

            if (config.msicType==MiscType.点||config.msicType==MiscType.混合)
            {
                for (int i = 0; i < config.miscCount; i++)
                {
                    randPoint[0] = new Point(rand.Next(imageFrame.Width), rand.Next(imageFrame.Height));
                    int cindex = rand.Next(config.miscColor.Length);
                    Color c = Color.FromArgb(config.miscAlpha, config.miscColor[cindex].R, config.miscColor[cindex].G, config.miscColor[cindex].B);
                    using (var pen=new Pen(c,config.miscWidth))
                    {
                        imageGraphics.DrawRectangle(pen, randPoint[0].X, randPoint[0].Y, config.miscWidth, config.miscWidth);
                    }
                }
            }//end if

            imageGraphics.Dispose();
        }

    }
}
