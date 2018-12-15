##Beginning##
Hello future maintainer (assuming) this branch of the LBO datapack contains generators I used to properly package the pack for pushing to the github repo.
There is a README.txt file, this contains basic instructions for installing the pack as well as version number, to keep version number sync place this in the same folder as last_block_org (or whatever the project is called). 
The idea is to copy these out to somewhere else, switch branches, then use the programs

##File structure##
.\saves\New World-\datapacks\ should look something like this before running the programs:
	last_block_org	| File folder
	README.txt	| Text Document

##The generators##
There are two generators: The first one is called "LastBlockOrg_releaseFilePrepareStation" and the second one called "FunctionFileGenerator" (however the .sln file is named "GiveTakeFunctionFileGenerator" but I'll refer to it as its folder name in this)

The generators themselves are built in C# the first one additionally using the Json.NET library (Newtonsoft.Json) to read the .mcmeta file.
The first program prepares the datapack for repo pushing while the second one generates two .mcfunction files for all custom recipes, a 'give' varient and a 'take' varient.

!! I RECOMMEND RUNNING ""FunctionFileGenerator" FIRST BEFORE RUNNING "LastBlockOrg_releaseFilePrepareStation" !!
It's probably obvious but it should be said anyway.

Both generators source code have comments to explain the process a bit but I admit one of them has better commenting than the other.
 
##A general rundown of the two programs functionality##
FunctionFileGenerator:
This program grabs each recipe and makes an .mcfunction file for both taking each custom recipe and giving each custom recipe. Simple really.
This program does not copy the project to a location but modifies it, obviously but should be noted.

LastBlockOrg_releaseFilePrepareStation:
This program automates the package creation process.
It first copies the entire datapack to a temp location.
Then copies and modifies the README.txt.
Then deletes git related files (.git and README.md)
Then creates a zip and moves it to the output location.
Finally it cleans up by deleting the temp location entirely.

##End##
If you want you could combine these two programs into a proper package system.


Thats it.
	~Commodoreprime

