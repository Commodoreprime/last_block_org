using System;
using System.IO;
using System.IO.Compression;
using Newtonsoft.Json;

namespace LastBlockOrg_releaseFilePrepareStation
{
	class Program
	{
		public static string src_path =
			""; //Path for the project (the folder of the datapack_root_folderName ex: .\saves\New World-\datapacks\)

		public static string out_path =
			""; //The out path. I would not recommend putting this in the same folder as the project but instead somewhere else then copy the finished zip to the repo folder

		public static string datapack_root_folderName =
			"last_block_org";//Name of the datapack, in this case its the github repo name

		public static string zip_name =
			"lastblockorg v";//This string is what the finished zip will be (ex: lastblockorg v0.1)

		//Generates datapack path
		public static string src_datapack_path = 
			Path.Combine(src_path, datapack_root_folderName);

		static void Main(string[] args)
		{
			Console.WindowWidth = 150;
			string versionNumber;

			Console.WriteLine("LASTBLOCKORG: RELEASE FILE PREPERATION\n");// \n Adds whitespace
			Console.WriteLine("Grabbing version from 'pack.mcmeta' file...");
			RootObject pack_meta = JsonConvert.DeserializeObject<RootObject>(File.ReadAllText(Path.Combine(src_datapack_path, "pack.mcmeta")));
			string packDescription = pack_meta.pack.description;//Gets the raw description in from json file
			versionNumber = packDescription.Replace("Version ", "");
			Console.WriteLine("Retrieved: " + versionNumber + " from file.\n"
				+ "If this is not the correct version: exit the program, modify 'pack.mcmeta' with correct information, then relaunch this program");
			WaitToCont();

			MakeFile(src_path, versionNumber, out_path);
		}
		public static void DirCopy(string sourceDirName, string destDirName, bool copySubDirs)
		{
			//Get subdirectories for the specified directory
			DirectoryInfo dir = new DirectoryInfo(sourceDirName);

			if (!dir.Exists)
			{
				throw new DirectoryNotFoundException(
					"Source directory does not exist or could not be found: "
					+ sourceDirName);
			}

			DirectoryInfo[] dirs = dir.GetDirectories();

			//If destination directory does not exist, create it
			if (!Directory.Exists(destDirName))
			{
				Directory.CreateDirectory(destDirName);
			}

			//Get the files in the directory and copy them to the new location
			FileInfo[] files = dir.GetFiles();
			foreach (FileInfo file in files)
			{
				string temppath = Path.Combine(destDirName, file.Name);
				file.CopyTo(temppath, false);
			}

			//If copying subdirectories, copy them and their contents to new location
			if (copySubDirs)
			{
				foreach (DirectoryInfo subdir in dirs)
				{
					string temppath = Path.Combine(destDirName, subdir.Name);
					DirCopy(subdir.FullName, temppath, copySubDirs);
				}
			}
		}

		public static void LineTextChanger(string newText, string fileName, int line)
		{
			string[] textLines = File.ReadAllLines(fileName);
			textLines[line] = newText;
			File.WriteAllLines(fileName, textLines);
		}

		public static void WaitToCont()
		{//Waits for user input to continue
			Console.Write("Press any key to continue... ");
			Console.ReadKey();
			Console.Write("\n");
		}

		public static void DelDir(string targetDir)
		{
			File.SetAttributes(targetDir, FileAttributes.Normal);

			string[] files = Directory.GetFiles(targetDir);
			string[] dirs = Directory.GetDirectories(targetDir);

			foreach(string file in files)
			{
				File.SetAttributes(file, FileAttributes.Normal);
				File.Delete(file);
			}

			foreach(string dir in dirs)
			{
				DelDir(dir);
			}

			Directory.Delete(targetDir, false);
 		}

		// bulk method
		public static void MakeFile(string srcPath, string relVer, string outPath)
		{
			//Generates temp file store location
			string tempLoc = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ".lbo_appdata", "relfileprepare");
			//Verify information
			Console.WriteLine("\nTemp location: " + tempLoc);
			Console.WriteLine("Source Path: " + srcPath);
			Console.WriteLine("Version to write for: " + relVer);
			Console.WriteLine("Output Path: " + outPath);
			WaitToCont();

			if (!Directory.Exists(tempLoc))//Creates temp directory 
				Directory.CreateDirectory(tempLoc);

			string temp_datapack_path = Path.Combine(tempLoc, datapack_root_folderName);

			//Copy from source
			Console.WriteLine("Copying datapack... Please wait...");
			DirCopy(src_datapack_path, Path.Combine(tempLoc,datapack_root_folderName), true);//Copies datapack to temp location
			Console.WriteLine("Done copying datapack. \nModifying README.txt...");
			string src_README_path = Path.Combine(srcPath, "README.txt");
			LineTextChanger("v" + relVer, src_README_path, 0);//Modifies line 0 to have accurate version number
			Console.WriteLine("Copying README.txt...");
			File.Copy(src_README_path, Path.Combine(tempLoc, "README.txt"), true);//Copies README.txt to temp location
			Console.WriteLine("Done with README.txt");

			//Delete unnessisary for release files (.git, README.md) (IN TEMP DIRECTORY)
			Console.WriteLine("Deleting '.git'...");
			DelDir(Path.Combine(temp_datapack_path, ".git"));//Delete .git
			Console.WriteLine("Deleting 'README.md'...");
			File.Delete(Path.Combine(temp_datapack_path, "README.md"));//Delete README.md
			Console.WriteLine("Done deleting unnessisary files");

			string zipFileName = zip_name + relVer + ".zip";
			//Zip README.txt and the datapack and also renames it
			Console.WriteLine("Creating zip...");
			ZipFile.CreateFromDirectory(tempLoc, zipFileName);
			Console.WriteLine("Done creating zip. \nMoving to final location...");
			File.Move(Path.Combine(Environment.CurrentDirectory, zipFileName), Path.Combine(out_path, zipFileName));
			Console.WriteLine("Done moving zip");

			//Cleaning up
			Console.WriteLine("Cleaning up...");
			Directory.Delete(tempLoc, true);
			Console.WriteLine("Done cleaning up");

			Console.WriteLine("Done!");
		}
	}
}
