using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebView.Maui;

namespace Microsoft.AspNetCore.Components.WebView.Maui.Android
{
	public interface IWebViewExCreator
	{
		BlazorWebChromeClient GenWebChromeClient(BlazorWebViewHandler handler);
		WebKitWebViewClient GenWebViewClient(BlazorWebViewHandler handler);
	}
}
