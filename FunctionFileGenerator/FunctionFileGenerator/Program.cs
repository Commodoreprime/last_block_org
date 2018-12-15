using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace GiveTakeFunctionFileGenerator
{//I freaking love this thing
	class Program
	{
		//Starting path
		public static string lbo_path = 
			"";
		//Namespace path (ex: .\saves\New World-\datapacks\last_block_org\data\lastblockorg)

		static void Main(string[] args)
		{
			Console.WindowWidth = 130; 
			Console.WriteLine(" _     ______  _____         _               ___        _           __ _ _                                        _             ");
			Console.WriteLine("| |    | ___ \\|  _  |       (_)             / / |      | |         / _(_) |                                      | |            ");
			Console.WriteLine("| |    | |_/ /| | | |   __ _ ___   _____   / /| |_ __ _| | _____  | |_ _| | ___    __ _  ___ _ __   ___ _ __ __ _| |_ ___  _ __ ");
			Console.WriteLine("| |    | ___ \\| | | |  / _` | \\ \\ / / _ \\ / / | __/ _` | |/ / _ \\ |  _| | |/ _ \\  / _` |/ _ \\ '_ \\ / _ \\ '__/ _` | __/ _ \\| '__|");
			Console.WriteLine("| |____| |_/ /\\ \\_/ / | (_| | |\\ V /  __// /  | || (_| |   <  __/ | | | | |  __/ | (_| |  __/ | | |  __/ | | (_| | || (_) | |   ");
			Console.WriteLine("\\_____/\\____/  \\___/   \\__, |_| \\_/ \\___/_/    \\__\\__,_|_|\\_\\___| |_| |_|_|\\___|  \\__, |\\___|_| |_|\\___|_|  \\__,_|\\__\\___/|_|   ");
			Console.WriteLine("                        __/ |                                                      __/ |                                        ");
			Console.WriteLine("                       |___/                                                      |___/                                         ");
			Console.WriteLine("");
			Console.WriteLine("Press any key to start the file write process...");
			Console.ReadKey();
			GenerateFile(lbo_path, "give");
			Console.WriteLine("'give' file generated. Press any key to start generating the 'take' file...");
			Console.ReadKey();
			GenerateFile(lbo_path, "take");
		}

		public static void GenerateFile(string StartingDir, string mode)
		{
			string entryDir = Path.Combine(StartingDir, "recipes");//Generates path to start finding files
			string OutputFile = Path.Combine(StartingDir, "functions", "lastblockorg_" + mode + ".mcfunction");//Generates output file path
			Console.WriteLine(entryDir);
			Console.WriteLine(OutputFile);

			//Makes list of all files found in the directory with the '.json' file type
			string[] getFilesRAW = Directory.GetFiles(entryDir, "*.json", SearchOption.AllDirectories);
			List<string> Files = new List<string>();//List that stores the paths that have been filterd to remove the path
			List<string> prepareFiles = new List<string>();//List that stores the paths without the '.json' filetype on the end

			Console.WriteLine("Raw in\n=======================================");
			foreach (string file in getFilesRAW)
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
				string commandpre = "recipe " + mode + " @s lastblockorg:" + file;
				string command = commandpre.Replace("\\", "/");
				Console.WriteLine(command);
				outputCommands.Add(command);
			}
			File.WriteAllLines(OutputFile, outputCommands);
			Console.WriteLine("\nDone!");
		}
	}
}
