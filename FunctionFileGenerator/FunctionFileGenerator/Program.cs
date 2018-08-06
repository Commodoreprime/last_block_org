using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace GiveTakeFunctionFileGenerator
{
	class Program
	{
		//Starting path
		public static string StartingDir = Path.Combine("C:\\Users",Environment.UserName,"AppData\\Roaming\\.minecraft\\versions\\snapshot_folder\\saves\\New World-\\datapacks\\last_block_org\\data\\lastblockorg");

		static void Main(string[] args)
		{
			string entryDir = Path.Combine(StartingDir, "recipes");//Generates path to start finding files
			string OutputFile = Path.Combine(StartingDir, "functions", "lastblockorg_give.mcfunction");//Generates output file path
			Console.WriteLine(entryDir);
			Console.WriteLine(OutputFile);

			//Makes list of all files found in the directory with the '.json' file type
			string[] getFilesRAW = Directory.GetFiles(entryDir, "*.json", SearchOption.AllDirectories);
			List<string> Files = new List<string>();//List that stores the paths that have been filterd to remove the path
			List<string> prepareFiles = new List<string>();//List that stores the paths without the '.json' filetype on the end

			Console.WriteLine("Raw in\n=======================================");
			foreach(string file in getFilesRAW)
			{//Removes the path then stores into Files
				Console.WriteLine(file);
				Files.Add(file.Replace(entryDir + "\\", ""));
			}
			Console.WriteLine("\nfound: " + getFilesRAW.Count() + " files");
			Console.WriteLine("=======================================");

			foreach (string file in Files)
			{//Removes the '.json' filetype then stores into prepareFiles
				Console.WriteLine(file);
				prepareFiles.Add(file.Replace(".json", ""));
			}
			Console.WriteLine("=======================================");

			List<string> outputCommands = new List<string>();
			Console.WriteLine(OutputFile);

			foreach (string file in prepareFiles)
			{//Generates command string with the file path then stores the string into outputCommands to put outputted into a file
				string command = "recipe give @s " + file;
				Console.WriteLine(command);
				outputCommands.Add(command);
			}
			File.WriteAllLines(OutputFile, outputCommands);
			Console.WriteLine("\nDone!");
		}
	}
}
