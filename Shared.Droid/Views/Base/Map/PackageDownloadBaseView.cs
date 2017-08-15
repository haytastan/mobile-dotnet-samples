﻿
using System;
using Android.Content;

namespace Shared.Droid
{
    public class PackageDownloadBaseView : DownloadBaseView
    {
        public ActionButton Packagebutton { get; private set; }

        public PackageDownloadBaseView(Context context, int backIcon, int closeIcon, int globalIcon, int wifiOnIcon, int wifiOffIcon) 
            : base(context, backIcon, closeIcon, wifiOnIcon, wifiOffIcon)
        {
            Packagebutton = new ActionButton(context, globalIcon);
            AddButton(Packagebutton);
        }
    }
}
