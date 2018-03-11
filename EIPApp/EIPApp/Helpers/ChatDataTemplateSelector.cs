﻿using EIPApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace EIPApp.Helpers
{
    public class ChatDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate OtherTemplate { get; set; }

        // 這個物件值，將會在 XAML 中來定義
        public DataTemplate OwnerTemplate { get; set; }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            // 根據當時物件的值，決定要使用哪個資料樣板
            return ((ChatContent)item).對話類型 == 對話類型.他人 ? OtherTemplate : OwnerTemplate;
        }
    }
}
