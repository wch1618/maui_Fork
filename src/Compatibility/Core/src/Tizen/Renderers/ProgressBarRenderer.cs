using System.ComponentModel;
using Microsoft.Maui.Controls.Platform;
using EColor = ElmSharp.Color;
using EProgressBar = ElmSharp.ProgressBar;
using Specific = Microsoft.Maui.Controls.PlatformConfiguration.TizenSpecific.ProgressBar;
using SpecificVE = Microsoft.Maui.Controls.PlatformConfiguration.TizenSpecific.VisualElement;

namespace Microsoft.Maui.Controls.Compatibility.Platform.Tizen
{
	[System.Obsolete(Compatibility.Hosting.MauiAppBuilderExtensions.UseMapperInstead)]
	public class ProgressBarRenderer : ViewRenderer<ProgressBar, EProgressBar>
	{
		static readonly EColor s_defaultColor = ThemeConstants.ProgressBar.ColorClass.Default;

		public ProgressBarRenderer()
		{
			RegisterPropertyHandler(ProgressBar.ProgressColorProperty, UpdateProgressColor);
		}

		protected override void OnElementChanged(ElementChangedEventArgs<ProgressBar> e)
		{
			if (Control == null)
			{
				SetNativeControl(new EProgressBar(Forms.NativeParent));
			}

			if (e.NewElement != null)
			{
				if (e.NewElement.MinimumWidthRequest == -1 &&
				e.NewElement.MinimumHeightRequest == -1 &&
				e.NewElement.WidthRequest == -1 &&
				e.NewElement.HeightRequest == -1)
				{
					Log.Warn("Need to size request");
				}

				UpdateAll();
			}

			base.OnElementChanged(e);
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);
			if (e.PropertyName == ProgressBar.ProgressProperty.PropertyName)
			{
				UpdateProgress();
			}
			else if (e.PropertyName == Specific.ProgressBarPulsingStatusProperty.PropertyName)
			{
				UpdatePulsingStatus();
			}
		}

		protected override void UpdateThemeStyle()
		{
			var themeStyle = SpecificVE.GetStyle(Element);
			if (!string.IsNullOrEmpty(themeStyle))
			{
				Control.Style = themeStyle;
				UpdateBackgroundColor(false);
				UpdateProgressColor(false);
			}
		}

		void UpdateAll()
		{
			UpdateProgress();
			UpdatePulsingStatus();
		}

		protected virtual void UpdateProgressColor(bool initialize)
		{
			if (initialize && Element.ProgressColor.IsDefault())
				return;

			Control.Color = Element.ProgressColor.IsDefault() ? s_defaultColor : Element.ProgressColor.ToPlatformEFL();
		}

		void UpdateProgress()
		{
			Control.Value = Element.Progress;
		}

		void UpdatePulsingStatus()
		{
			bool isPulsing = Specific.GetPulsingStatus(Element);
			if (isPulsing)
			{
				Control.PlayPulse();
			}
			else
			{
				Control.StopPulse();
			}
		}
	}
}

