using System;
using System.ComponentModel;

namespace Delivery.Library
{
	/// <summary>
	/// Describes the settings required for publishing a solution
	/// </summary>
	public class PublishSettings
	{
		[Category("Testing")]
		public bool RequirePassingTests { get; set; }

		[Category("Testing")]
		public string TestProject { get; set; }

		public Type InstallerType { get; set; }

		public Installer Installer { get; set; }		
	}
}