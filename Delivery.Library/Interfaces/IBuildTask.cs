using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Library.Interfaces
{
	public interface IBuildTask
	{		
		string Version { get; }
		void Run();
	}
}
