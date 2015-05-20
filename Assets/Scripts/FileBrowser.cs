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
	GameObject homeNode;
	string rootPath;
	string currentDirectory;
	ArrayList nodes;
	char slash;
	public Vector3 []folderPositions = new Vector3[20];
	public Vector3 []filePositions = new Vector3[20];
	public GameObject [] folderParents = new GameObject[3];
	public Rays rays;

	int folderCount = 0;
	int fileCount = 0;


	void Start (){
	
		nodes = new ArrayList();
		rays = Camera.main.GetComponent<Rays> ();

		// Using windows
		rootPath = "C:\\Users\\Zac\\Documents\\file-browser";
		currentDirectory = "C:\\Users\\Zac\\Documents\\file-browser\\HCI Test";
		slash = '\\';

		if (!System.IO.Directory.Exists (rootPath)) {

			// Using unix
			rootPath = "/Users/Zac/Desktop";
			currentDirectory = "/Users/Zac/Desktop/HCI Test";
			slash = '/';
		}
	
		/*
		backNode = (GameObject)Instantiate(back.gameObject, new Vector3(-20, 0, 0), Quaternion.identity);
		backNode.GetComponent<TreeNode>().setId(rootPath);
		backNode.GetComponent<TreeNode>().setPath(rootPath);
		backNode.GetComponent<TreeNode>().setFileType(TreeNode.FileType.Back);
		*/

		rootNode = (GameObject)Instantiate(rootFolder.gameObject, new Vector3(0, 0, -5), Quaternion.identity);
		rootNode.GetComponent<TreeNode>().setId(currentDirectory);
		rootNode.GetComponent<TreeNode>().setPath(currentDirectory);
		rootNode.GetComponent<TreeNode>().setFileType(TreeNode.FileType.RootNode);
		homeNode = rootNode;
		Camera.main.transform.GetComponent<ClickScript> ().homeFolder = homeNode;
		Camera.main.transform.GetComponent<ClickScript> ().searchFiles[0] = rootNode;

		TraverseTree (currentDirectory);
		int count = 0;
		int count2 = 0;

		// Children
		GameObject thisNode;
		GameObject currentRoot = (GameObject)Instantiate (rootNode, new Vector3 (rootNode.transform.position.x + 100f, rootNode.transform.position.y + 50F, 100), Quaternion.identity);
		currentRoot.transform.GetComponent<TreeNode>().parent = (GameObject)nodes[0];
		Camera.main.transform.GetComponent<ClickScript> ().favourite1 = currentRoot;
		
		Camera.main.transform.GetComponent<ClickScript> ().searchFiles[0] = (GameObject)nodes[0];
		Camera.main.transform.GetComponent<ClickScript> ().searchFiles[2] = currentRoot;

		foreach(GameObject currentNode in nodes)
		{

			if(count == 0 || count == 1 || count == 2)
			{
				folderParents[count2] = (GameObject)Instantiate(currentNode, new Vector3(currentNode.transform.position.x +100f, currentNode.transform.position.y +50F, 100), Quaternion.identity);
				folderParents[count2].transform.GetComponent<TreeNode>().parent = currentRoot;


				if(count == 1)
				{
					Camera.main.transform.GetComponent<ClickScript> ().searchFiles[3] = folderParents[count2];
				}
				

				count2++;
			}else{
				thisNode =  (GameObject)Instantiate(currentNode, new Vector3(currentNode.transform.position.x +100f, currentNode.transform.position.y +50F, 100), Quaternion.identity);
				thisNode.transform.GetComponent<TreeNode>().parent = currentRoot;
			}

			count++;
		}

		currentRoot = (GameObject)Instantiate(rootNode, new Vector3(rootNode.transform.position.x + 100f, rootNode.transform.position.y -50F,100), Quaternion.identity);
		currentRoot.transform.GetComponent<TreeNode>().parent = (GameObject)nodes[3];

		foreach(GameObject currentNode in nodes)
		{
			thisNode = (GameObject)Instantiate(currentNode, new Vector3(currentNode.transform.position.x + 100f, currentNode.transform.position.y - 50f, 100), Quaternion.identity);
			thisNode.transform.GetComponent<TreeNode>().parent = currentRoot;
		}


		// Child of child
		currentRoot = (GameObject)Instantiate(rootNode, new Vector3(rootNode.transform.position.x + 300f, rootNode.transform.position.y +120f, 200), Quaternion.identity);
		currentRoot.transform.GetComponent<TreeNode>().parent = (GameObject)folderParents[0];

		foreach(GameObject currentNode in nodes)
		{


			thisNode = (GameObject)Instantiate(currentNode, new Vector3(currentNode.transform.position.x + 300f, currentNode.transform.position.y+ 120f, 200), Quaternion.identity);
			thisNode.transform.GetComponent<TreeNode>().parent = currentRoot;
		}

		currentRoot = (GameObject)Instantiate (rootNode, new Vector3 (rootNode.transform.position.x + 300f, rootNode.transform.position.y + 60f, 200), Quaternion.identity);
		currentRoot.transform.GetComponent<TreeNode>().parent = (GameObject)folderParents[1];
		Camera.main.transform.GetComponent<ClickScript> ().favourite2 = currentRoot;
		Camera.main.transform.GetComponent<ClickScript> ().searchFiles[5] = currentRoot;

		count = 0;
		foreach(GameObject currentNode in nodes)
		{
			thisNode = (GameObject)Instantiate(currentNode, new Vector3(currentNode.transform.position.x + 300f, currentNode.transform.position.y + 60f, 200), Quaternion.identity);
			thisNode.transform.GetComponent<TreeNode>().parent = currentRoot;

			if(count == 2)
			{
				Camera.main.transform.GetComponent<ClickScript> ().searchFiles[4] = thisNode;
			}

			count++;
		}

		currentRoot = (GameObject)Instantiate (rootNode, new Vector3 (rootNode.transform.position.x + 300f, rootNode.transform.position.y + 0f, 200), Quaternion.identity);
		currentRoot.transform.GetComponent<TreeNode>().parent = (GameObject)folderParents[2];

		foreach(GameObject currentNode in nodes)
		{
			thisNode = (GameObject)Instantiate(currentNode, new Vector3(currentNode.transform.position.x + 300f, currentNode.transform.position.y+ 0f, 200), Quaternion.identity);
			thisNode.transform.GetComponent<TreeNode>().parent = currentRoot;
		}
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

		/*
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
		*/

		// Reset the root node

		currentDirectory = newNode.path;
		rootNode = (GameObject)Instantiate(rootFolder.gameObject, new Vector3(0, 0, -5), Quaternion.identity);
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

					
					Vector3 position = folderPositions[folderCount];
					GameObject folderNode = (GameObject)Instantiate(folder.gameObject, position, Quaternion.identity);
					folderNode.GetComponent<TreeNode>().drawLine(rootNode);
					folderNode.GetComponent<TreeNode>().setId(path[path.Length - 1]);
					folderNode.GetComponent<TreeNode>().setPath(str);
					folderNode.GetComponent<TreeNode>().setFileType(TreeNode.FileType.Folder);
					folderNode.GetComponentInChildren<TextMesh> ().text = path[path.Length - 1];

					rootNode.GetComponent<TreeNode>().addChild (folderNode);
					nodes.Add(folderNode);
					folderCount++;
				}



				// Iterate through the files
				foreach (string fileName in files) {

					System.IO.FileInfo fi = new System.IO.FileInfo (fileName);
					Vector3 position = filePositions[fileCount];

					GameObject fileNode = (GameObject)Instantiate(file.gameObject, position, Quaternion.identity);
					fileNode.GetComponent<TreeNode>().drawLine(rootNode);
					fileNode.GetComponent<TreeNode>().setId(fi.Name);
					fileNode.GetComponent<TreeNode>().setPath(currentDirectory + fi.Name);
					fileNode.GetComponent<TreeNode>().setFileType(TreeNode.FileType.File);
					fileNode.GetComponentInChildren<TextMesh> ().text = fi.Name;



					rootNode.GetComponent<TreeNode>().addChild (fileNode);
					nodes.Add(fileNode);
					fileCount++;
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

