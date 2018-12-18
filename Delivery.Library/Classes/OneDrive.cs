﻿using Microsoft.Win32;
using System.IO;

namespace Delivery.Library.Classes
{
	public static class OneDrive
	{
		public static string Path
		{
			get
			{
				string[] regKeys = new string[]
				{
					"HKEY_CURRENT_USER\\Software\\Microsoft\\SkyDrive",
					"HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\SkyDrive",
					"HKEY_CURRENT_USER\\Software\\Microsoft\\OneDrive"
				};

				foreach (var regKey in regKeys)
				{
					var folder = Registry.GetValue(regKey, "UserFolder", null);
					if (folder != null && Directory.Exists(folder.ToString())) return folder.ToString();
				}

				return null;
			}
		}

		public static string ResolvePath(string fileName)
		{
			return fileName.Replace("%OneDrive%", Path);
		}
	}
}