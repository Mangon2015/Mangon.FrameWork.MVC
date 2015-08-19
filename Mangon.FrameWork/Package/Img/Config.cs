using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Mangon.FrameWork.Package.Img
{
    /// <summary>
    /// 默认的校验图设置
    /// </summary>
    [XmlInclude(typeof(Config))]
   public class Config
    {
        public int _zoomMaxPersent = 0;
        public int _zoomMinPersent = 0;
        public int _rotateAngleMax = 0;
        public int _RotateAngleMin = 0;
        public bool IsContinuousRotation = false;
        /// <summary>
        /// 宽 如果-1 则是自动
        /// </summary>
        public int with = -1;
        /// <summary>
        /// 高
        /// </summary>
        public int height = 23;
        /// <summary>
        /// 动画
        /// </summary>
        public bool IsAnimation = false;
        /// <summary>
        /// 压缩比率
        /// </summary>
        public int Compression = 75;
        /// <summary>
        /// 使用颜色
        /// </summary>
        public Color[] color = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };
        /// <summary>
        /// 混淆颜色
        /// </summary>
        public Color[] miscColor = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };
        /// <summary>
        /// 使用颜色
        /// </summary>
        public Color[] validateCodeColor = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };
        /// <summary>
        /// 混淆的透明度
        /// </summary>
        public int miscAlpha = 100;
        /// <summary>
        /// 混淆的宽度
        /// </summary>
        public float miscWidth = 0.4f;
        /// <summary>
        /// 混淆数量
        /// </summary>
        public int miscCount = 5;
        /// <summary>
        /// 混淆类型 0线 1点 2混合
        /// </summary>
        public MiscType msicType = MiscType.线;
        /// <summary>
        /// 验证码
        /// </summary>
        public string validateCode = string.Empty;
        /// <summary>
        /// 反锯齿
        /// </summary>
        public bool FontTextRenderingHint = false;
        /// <summary>
        /// 笔刷
        /// </summary>
        public int[] ImageBrush = { 1, 2, 3 };
        /// <summary>
        /// 验证字体
        /// </summary>
        public string[] validateCodeFont = { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial" };
        /// <summary>
        /// 验证码字形型号（像素）
        /// </summary>
        public float[] validateCodeSize = { 13, 10, 15 };
        /// <summary>
        /// 验证码字体样式
        /// </summary>
        public FontStyle[] validateCodeStyle = { FontStyle.Bold, FontStyle.Italic, FontStyle.Regular, FontStyle.Strikeout, FontStyle.Underline };
        /// <summary>
        /// 验证码文字的透明度
        /// </summary>
        public int validateCodeAlpha = 255;
        /// <summary>
        /// 验证码中所有的字符
        /// </summary>
        public string allChar = "1,2,3,4,5,6,7,8,9,0,A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,W,X,Y,Z";


    }

    public enum MiscType
    { 
        线=0,
        点=1,
        混合=2
        
    }
}
