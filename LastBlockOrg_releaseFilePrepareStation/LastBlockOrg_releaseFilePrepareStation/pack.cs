using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastBlockOrg_releaseFilePrepareStation
{
	public class Pack
	{
		public int pack_format { get; set; }
		public string description { get; set; }
	}

	public class RootObject
	{
		public Pack pack { get; set; }
	}
}
