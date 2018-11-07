﻿// Copyright © 2017 Paddy Xu
// 
// This file is part of QuickLook program.
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using QuickLook.Common.Helpers;

namespace QuickLook.Plugin.HtmlViewer
{
    public class WebpagePanel : WpfWebBrowserWrapper
    {
        public WebpagePanel()
        {
            var factor = VisualTreeHelper.GetDpi(this);
            Zoom = (int)(factor.DpiScaleX*100);
        }

        // adjust zoom when DPI changes.
        protected override void OnDpiChanged(DpiScale oldDpi, DpiScale newDpi)
        {
            var ratio = newDpi.DpiScaleX / oldDpi.DpiScaleX;
            Zoom = (int)(Zoom * ratio);
            base.OnDpiChanged(oldDpi, newDpi);
        }

        public void LoadFile(string path)
        {
            if (Path.IsPathRooted(path))
                path = Helper.FilePathToFileUrl(path);

            Dispatcher.Invoke(() => { base.Navigate(path); }, DispatcherPriority.Loaded);
        }

        public void LoadHtml(string html)
        {
            var s = new MemoryStream(Encoding.UTF8.GetBytes(html ?? ""));

            Dispatcher.Invoke(() => { base.Navigate(s); }, DispatcherPriority.Loaded);
        }
    }
}