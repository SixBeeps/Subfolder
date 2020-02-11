/*
		This is an example of how to use Subfolder, written by SixBeeps.
*/
using System;

class MainClass {
  public static void Main (string[] args) {
    Folder rootDirectory = new Folder();
		for (int i = 0; i < 6; i++) {
			File newFile = new File("TestFile" + i + ".txt", "Hello world!", rootDirectory);
			Console.WriteLine("New file created at " + newFile.GetPath());
		}
		Console.WriteLine("Files in the root directory: " + rootDirectory.GetContentsIncludingChildren().Count);

		File tf0 = rootDirectory.FindFileByName("TestFile0.txt");
		Console.WriteLine("Contents of " + tf0.GetPath() + ": " + tf0.fileContents);

		
		try {
			File badFile = new File("TestFile0.txt", "This file will cause a SubfolderFileException", rootDirectory);
		} catch (SubfolderFileException e) {
			Console.WriteLine(e);
		}
  }
}