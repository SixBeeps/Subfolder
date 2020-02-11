using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>Interface <c>IFolderable</c> represents a data type that
/// may be put inside a <c>folder</c> object</summary>
public interface IFolderable {
	string name { get; set; }
	string parent { get; }
	string GetPath();
}

/// <summary>Class <c>Folder</c> can contain multiple <c>IFolderable</c>
/// objects</summary>
public class Folder : IFolderable {
	public string name { get; set; }
	public string parent { get; }
	public List<IFolderable> folderContents = new List<IFolderable>();
	
	/// <summary>Returns the path to this folder</summary>
	public string GetPath() {
		return (parent != null) ? parent + "/" + name : "base";
	}

	/// <summary>Returns a list of <c>IFolderable</c> objects in 
	/// this folder, including the ones inside them (if applicable)</summary>
	public List<IFolderable> GetContentsIncludingChildren() {
		List<IFolderable> ret = new List<IFolderable>(folderContents);
		foreach(Folder fld in folderContents.OfType<Folder>()) {
			foreach(IFolderable i in fld.GetContentsIncludingChildren()) ret.Add(i);
		}
		return ret;
	}

	/// <summary>Searches this folder for a file given a name</summary>
	/// <param name="n">The name for the file to search for</param>
	/// <returns>The <c>File</c> object, or <c>null</c> if the file
	/// wasn't found</returns>
	public File FindFileByName(String n) {
		foreach(File f in folderContents.OfType<File>()) {
			if (f.name == n) return f;
		}
		return null;
	}

	/// <summary>Creates a folder with a name and a parent folder</summary>
	/// <param name="n">The name for the folder</param>
	/// <param name="p">A parent folder</param>
	public Folder(String n, Folder p) {
		parent = p.GetPath();
		name = n;
		foreach(Folder f in p.folderContents.OfType<Folder>()) {
			if (f.name == n) throw new SubfolderFileException("A folder with the name " + n + " already exists in the folder");
		}
		p.folderContents.Add(this);
	}
	public Folder() {}
}

/// <summary>Class <c>File</c> derives from <c>IFolderable</c> and
/// contains file data</summary>
public class File : IFolderable {
	public string name { get; set; }
	public string parent { get; }
	public string fileContents;

	/// <summary>Returns the path to this file</summary>
	public string GetPath() {
		return (parent != null) ? parent + "/" + name : "base";
	}

	/// <summary>Creates an empty file </summary>
	/// <param name="n">The name for the folder</param>
	/// <param name="p">A parent folder</param>
	public File(String n, Folder p) {
		parent = p.GetPath();
		name = n;
		foreach(File f in p.folderContents.OfType<File>()) {
			if (f.name == n) throw new SubfolderFileException("A file with the name " + n + " already exists in the folder");
		}
		p.folderContents.Add(this);
	}

	/// <summary>Creates an file with some data</summary>
	/// <param name="n">The name for the folder</param>
	/// <param name="d">The data contents of the file</param>
	/// <param name="p">A parent folder</param>
	public File(String n, String d, Folder p) {
		parent = p.GetPath();
		fileContents = d;
		name = n;
		foreach(File f in p.folderContents.OfType<File>()) {
			if (f.name == n) throw new SubfolderFileException("A file with the name " + n + " already exists in the folder");
		}
		p.folderContents.Add(this);
	}
	public File() {}
}

/// <summary>A general-use exception for Subfolder errors</summary>
public class SubfolderFileException : Exception {
  public SubfolderFileException(string message) : base(message) {}
}