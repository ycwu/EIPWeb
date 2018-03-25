using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace EIPApp.Models
{
    public enum 對話類型
    {
        他人,
        自己
    }
    public class ChatContent : BindableBase
    {
        #region ViewModel Property (用於在 View 中作為綁定之用)
        public string 姓名 { get; set; }
        public Color 姓名文字顏色 { get; set; }
        public string 對話人圖片 { get; set; }
        public string 對話內容 { get; set; }
        public 對話類型 對話類型 { get; set; }
        public Color 對話文字顏色 { get; set; }
        #endregion

        #region Constructor 建構式
        public ChatContent()
        {
        }
        #endregion
    }
}
