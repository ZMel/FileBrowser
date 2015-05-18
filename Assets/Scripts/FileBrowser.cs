using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class FileBrowser : MonoBehaviour
{

	public Transform rootFolder;
	public Transform file;
	public Transform folder;
	public Transform back;

	GameObject rootNode;
	GameObject backNode;
	string rootPath;
	string currentDirectory;
	ArrayList nodes;
	char slash;

	void Start (){
	
		nodes = new ArrayList();

		// Using windows
		rootPath = "C:\\Users\\Zac";
		currentDirectory = "C:\\Users\\Zac\\Dropbox";
		slash = '\\';

		if (!System.IO.Directory.Exists (rootPath)) {

			// Using unix
			rootPath = "/Users/Zac";
			currentDirectory = "/Users/Zac/Dropbox";
			slash = '/';
		}
	
		backNode = (GameObject)Instantiate(back.gameObject, new Vector3(-20, 0, 0), Quaternion.identity);
		backNode.GetComponent<TreeNode>().setId(rootPath);
		backNode.GetComponent<TreeNode>().setPath(rootPath);
		backNode.GetComponent<TreeNode>().setFileType(TreeNode.FileType.Back);


		rootNode = (GameObject)Instantiate(rootFolder.gameObject, new Vector3(0, 0, 0), Quaternion.identity);
		rootNode.GetComponent<TreeNode>().setId(currentDirectory);
		rootNode.GetComponent<TreeNode>().setPath(currentDirectory);
		rootNode.GetComponent<TreeNode>().setFileType(TreeNode.FileType.RootNode);
		//rootNode.GetComponentInChildren<TextMesh> ().text = "Desktop";
		
		TraverseTree (currentDirectory);
	}

	public void reload(TreeNode newNode)
	{
		deleteAll ();
		int cutoffPoint = 0;

		if (newNode.getType () == TreeNode.FileType.Folder) {
			cutoffPoint = 1;
		} else {
			cutoffPoint = 2;
		}

		// Reset the back node
		rootPath = "";
		string [] newPath = newNode.path.Split(slash);
		for(int i = 0; i < newPath.Length - cutoffPoint; i++)
		{
			rootPath += newPath[i] + slash.ToString();
		}

		backNode = (GameObject)Instantiate(back.gameObject, new Vector3(-20, 0, 0), Quaternion.identity);
		backNode.GetComponent<TreeNode>().setId(rootPath);
		backNode.GetComponent<TreeNode>().setPath(rootPath);
		backNode.GetComponent<TreeNode>().setFileType(TreeNode.FileType.Back);

		// Reset the root node
		currentDirectory = newNode.path;
		rootNode = (GameObject)Instantiate(rootFolder.gameObject, new Vector3(0, 0, 0), Quaternion.identity);
		rootNode.GetComponent<TreeNode> ().setId (newNode.ID);
		rootNode.GetComponent<TreeNode>().setPath(currentDirectory);
		rootNode.GetComponent<TreeNode>().setFileType(TreeNode.FileType.RootNode);
		//rootNode.GetComponentInChildren<TextMesh> ().text = newNode.ID;

		TraverseTree (currentDirectory);
	}

	public void deleteAll()
	{
		// Delete all the child nodes
		foreach(GameObject currentNode in nodes)
		{
			Destroy (currentNode);
		}

		// Lastly delete the root and back node
		Destroy (rootNode);
		Destroy (backNode);
	}

	public void TraverseTree (string root){

		try {

			Stack<string> dirs = new Stack<string> ();
		
			if (!System.IO.Directory.Exists (root)) {
				Debug.Log ("Root doesnt exist");
				throw new ArgumentException ();
			}
			dirs.Push (root);
		
			while (dirs.Count > 0) {

				// Get the file and folders (Sub-directories)
				string[] subDirs;
				string[] files = null;
				string currentDir = dirs.Pop ();

				subDirs = System.IO.Directory.GetDirectories (currentDir);
				files = System.IO.Directory.GetFiles (currentDir);

				// Iterate through the folders
				foreach (string str in subDirs) {
					string [] path = str.Split(slash);

					GameObject folderNode = (GameObject)Instantiate(folder.gameObject, new Vector3(0, 0, -5), Quaternion.identity);
					folderNode.GetComponent<TreeNode>().setId(path[path.Length - 1]);
					folderNode.GetComponent<TreeNode>().setPath(str);
					folderNode.GetComponent<TreeNode>().setFileType(TreeNode.FileType.Folder);
					//folderNode.GetComponentInChildren<TextMesh> ().text = path[path.Length - 1];
					
					rootNode.GetComponent<TreeNode>().addChild (folderNode);
					nodes.Add(folderNode);
				}

				// Iterate through the files
				foreach (string fileName in files) {

					System.IO.FileInfo fi = new System.IO.FileInfo (fileName);

					GameObject fileNode = (GameObject)Instantiate(file.gameObject, new Vector3(0, 0, -10), Quaternion.identity);
					fileNode.GetComponent<TreeNode>().setId(fi.Name);
					fileNode.GetComponent<TreeNode>().setPath(currentDirectory + fi.Name);
					fileNode.GetComponent<TreeNode>().setFileType(TreeNode.FileType.File);
					//fileNode.GetComponentInChildren<TextMesh> ().text = fi.Name;
					
					rootNode.GetComponent<TreeNode>().addChild (fileNode);
					nodes.Add(fileNode);
				}
			}

		} catch (Exception e) {

			if (e is UnauthorizedAccessException || e is System.IO.DirectoryNotFoundException) {
				Debug.Log ("An error has occured: " + e.Message);
			}else{
				Debug.Log ("A general error has occured: " + e.Message);
			}
		}


	}
}

