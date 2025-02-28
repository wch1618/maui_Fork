using System;
using ElmSharp;
using ELayout = ElmSharp.Layout;
using ESize = ElmSharp.Size;

namespace Microsoft.Maui.Controls.Compatibility.Platform.Tizen.Native
{
	/// <summary>
	/// The Text Helper contains functions that assist in working with text-able objects.
	/// </summary>
	public static class TextHelper
	{
		/// <summary>
		/// Gets the size of raw text block.
		/// </summary>
		/// <param name="textable">The <see cref="EvasObject"/> with text part.</param>
		/// <returns>Returns the size of raw text block.</returns>
		public static ESize GetRawTextBlockSize(EvasObject textable)
		{
			return GetElmTextPart(textable)?.TextBlockNativeSize ?? new ESize(0, 0);
		}

		/// <summary>
		/// Gets the size of formatted text block.
		/// </summary>
		/// <param name="textable">The <see cref="ElmSharp.EvasObject"/> with text part.</param>
		/// <returns>Returns the size of formatted text block.</returns>
		public static ESize GetFormattedTextBlockSize(EvasObject textable)
		{
			return GetElmTextPart(textable)?.TextBlockFormattedSize ?? new ESize(0, 0);
		}

		/// <summary>
		/// Gets the ELM text part of evas object.
		/// </summary>
		/// <param name="textable">The <see cref="ElmSharp.EvasObject"/> with text part.</param>
		/// <returns>Requested <see cref="ElmSharp.EdjeTextPartObject"/> instance.</returns>
		/// <exception cref="ArgumentException">Throws exception when parameter <paramref name="textable" /> isn't text-able object or doesn't have ELM text part.</exception>
		static EdjeTextPartObject GetElmTextPart(EvasObject textable)
		{
			ELayout widget = textable as ELayout;
			if (widget == null)
			{
				Log.Error("textable should be ElmSharp.Layout");
			}
			EdjeTextPartObject textPart = widget?.EdjeObject[ThemeConstants.Common.Parts.Text];
			if (textPart == null)
			{
				Log.Error("There is no elm.text part");
			}
			return textPart;
		}
	}
}
