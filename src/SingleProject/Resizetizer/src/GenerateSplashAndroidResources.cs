﻿using System.IO;
using System.Xml;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using SkiaSharp;

namespace Microsoft.Maui.Resizetizer
{
	/// <summary>
	/// Generates Resources/values/maui_colors.xml and Resources/drawable/maui_splash_image.xml for Android splash screens
	/// </summary>
	public class GenerateSplashAndroidResources : Task
	{
		[Required]
		public string ColorsFile { get; set; }

		[Required]
		public string DrawableFile { get; set; }

		[Required]
		public ITaskItem[] MauiSplashScreen { get; set; }

		public override bool Execute()
		{
			var splash = MauiSplashScreen[0];

			var info = ResizeImageInfo.Parse(splash);

			WriteColors(info);
			WriteDrawable(info);

			return !Log.HasLoggedErrors;
		}

		static readonly XmlWriterSettings Settings = new XmlWriterSettings { Indent = true };
		const string Namespace = "http://schemas.android.com/apk/res/android";
		const string Comment = "This file was auto-generated by .NET MAUI.";

		void WriteColors(ResizeImageInfo splash)
		{
			Directory.CreateDirectory(Path.GetDirectoryName(ColorsFile));

			using var writer = XmlWriter.Create(ColorsFile, Settings);
			writer.WriteComment(Comment);
			writer.WriteStartElement("resources");

			if (splash.Color is not null)
			{
				writer.WriteStartElement("color");
				writer.WriteAttributeString("name", "maui_splash_color");
				writer.WriteString(splash.Color.ToString());
				writer.WriteEndElement();
			}

			writer.WriteEndDocument();
		}

		void WriteDrawable(ResizeImageInfo splash)
		{
			Directory.CreateDirectory(Path.GetDirectoryName(DrawableFile));

			using var writer = XmlWriter.Create(DrawableFile, Settings);
			writer.WriteComment(Comment);
			writer.WriteStartElement("layer-list");
			writer.WriteAttributeString("xmlns", "android", ns: null, value: Namespace);

			writer.WriteStartElement("item");
			writer.WriteAttributeString("android", "drawable", Namespace, "@drawable/" + splash.OutputName);

			writer.WriteEndDocument();
		}
	}
}
