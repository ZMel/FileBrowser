using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
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
	public Vector3[]positions = new Vector3[9];
	public Vector3[]folderPositions = new Vector3[20];
	public Vector3[]filePositions = new Vector3[20];
	public GameObject[] folderParents = new GameObject[3];
	public Rays rays;
	int folderCount = 0;
	int fileCount = 0;

	void Start ()
	{
	
		nodes = new ArrayList ();
		rays = Camera.main.GetComponent<Rays> ();

		// Using windows
		rootPath = "C:\\Users\\Zac\\Documents\\file-browser";
		currentDirectory = "C:\\Users\\Zac\\Documents\\file-browser\\HCI Test Moderate";
		slash = '\\';

		if (!System.IO.Directory.Exists (rootPath)) {

			// Using unix
			rootPath = "/Users/Zac/Documents/file-browser";
			currentDirectory = "/Users/Zac/Documents/file-browser/HCI Test Moderate";
			slash = '/';
		}

		rootPath = PlayerPrefs.GetString ("rootPath");
		currentDirectory = PlayerPrefs.GetString ("currentDirectory");

		rootNode = (GameObject)Instantiate (rootFolder.gameObject, new Vector3 (0, 0, -5), Quaternion.identity);
		rootNode.GetComponent<TreeNode> ().setId (currentDirectory);
		rootNode.GetComponent<TreeNode> ().setPath (currentDirectory);
		rootNode.GetComponent<TreeNode> ().setFileType (TreeNode.FileType.RootNode);
		homeNode = rootNode;
		Camera.main.transform.GetComponent<ClickScript> ().homeFolder = homeNode;
		Camera.main.transform.GetComponent<ClickScript> ().searchFiles [0] = rootNode;

		TraverseTree (currentDirectory);

		Camera.main.transform.GetComponent<ClickScript> ().homeFolder = rootNode;
		Camera.main.transform.GetComponent<ClickScript> ().nodes = nodes;
	}

	public void reload (TreeNode newNode)
	{
		deleteAll ();
		int cutoffPoint = 0;

		if (newNode.getType () == TreeNode.FileType.Folder) {
			cutoffPoint = 1;
		} else {
			cutoffPoint = 2;
		}

		// Reset the root node

		currentDirectory = newNode.path;
		rootNode = (GameObject)Instantiate (rootFolder.gameObject, new Vector3 (0, 0, -5), Quaternion.identity);
		rootNode.GetComponent<TreeNode> ().setId (newNode.ID);
		rootNode.GetComponent<TreeNode> ().setPath (currentDirectory);
		rootNode.GetComponent<TreeNode> ().setFileType (TreeNode.FileType.RootNode);
		//rootNode.GetComponentInChildren<TextMesh> ().text = newNode.ID;

		TraverseTree (currentDirectory);
	}

	public void deleteAll ()
	{
		// Delete all the child nodes
		foreach (GameObject currentNode in nodes) {
			Destroy (currentNode);
		}

		// Lastly delete the root and back node
		Destroy (rootNode);
		Destroy (backNode);
	}

	public void TraverseTree (string root)
	{

		int directoryCount = 0;
		int objectCount = 1;
		GameObject currentNode = rootNode;

		Stack<GameObject> dirs = new Stack<GameObject> ();
		
		if (!System.IO.Directory.Exists (root)) {
			Debug.Log ("Root doesnt exist");
			throw new ArgumentException ();
		}

		dirs.Push (rootNode);
		
		while (dirs.Count > 0) {

			// Get the file and folders (Sub-directories)
			string[] subDirs;
			string[] files = null;
			GameObject parent = dirs.Pop ();
			string currentDir = parent.GetComponent<TreeNode> ().path;
		
			subDirs = System.IO.Directory.GetDirectories (currentDir);
			files = System.IO.Directory.GetFiles (currentDir);

			if(subDirs.Length != 0)
			{
				// Instantiate the root	
				if (directoryCount != 0) {
					
					Vector3 position = parent.transform.position;
					
					if (parent.GetComponent<TreeNode>().objectPosition == 1) {
						position.x += (0 );
					} else if (parent.GetComponent<TreeNode>().objectPosition == 2) {
						position.x += (160 );
					} else if (parent.GetComponent<TreeNode>().objectPosition == 3) {
						position.x += (240 );
					} else if (parent.GetComponent<TreeNode>().objectPosition == 4) {
						position.x += (160 );
					} else if (parent.GetComponent<TreeNode>().objectPosition == 5) {
						position.x += (0 );
					} else if (parent.GetComponent<TreeNode>().objectPosition == 6) {
						position.x -= (160);
					} else if (parent.GetComponent<TreeNode>().objectPosition == 7) {
						position.x -= (240 );
					} else if (parent.GetComponent<TreeNode>().objectPosition == 8) {
						position.x -= (160 );
					}
					
					position.z += 100;
					
					if (parent.GetComponent<TreeNode>().objectPosition == 1) {
						position.y += (120);
					} else if (parent.GetComponent<TreeNode>().objectPosition == 2) {
						position.y += (80);
					} else if (parent.GetComponent<TreeNode>().objectPosition == 3) {
						position.y += (0 );
					} else if (parent.GetComponent<TreeNode>().objectPosition == 4) {
						position.y -= (80 );
					} else if (parent.GetComponent<TreeNode>().objectPosition == 5) {
						position.y -= (120 );
					} else if (parent.GetComponent<TreeNode>().objectPosition == 6) {
						position.y -= (80 );
					} else if (parent.GetComponent<TreeNode>().objectPosition == 7) {
						position.y += (0 );
					} else if (parent.GetComponent<TreeNode>().objectPosition == 8) {
						position.y += (80 );
					}
					
					currentNode = (GameObject)Instantiate (rootFolder.gameObject, position, Quaternion.identity);
					currentNode.GetComponent<TreeNode> ().parent = parent;
					currentNode.GetComponent<TreeNode> ().drawLine (parent);
					currentNode.GetComponent<TreeNode> ().setId (currentDirectory);
					currentNode.GetComponent<TreeNode> ().setPath (currentDirectory);
					currentNode.GetComponent<TreeNode> ().setFileType (TreeNode.FileType.RootNode);
					currentNode.GetComponentInChildren<TextMesh> ().GetComponent<MeshRenderer> ().sortingOrder = 1;
					nodes.Add (currentNode);
				}

				// Iterate through the folders
				foreach (string str in subDirs) {
					
					string [] path = str.Split (slash);
					
					//Vector3 position = folderPositions[folderCount];
					Vector3 position = positions[objectCount];
					Vector3 parentPosition = currentNode.transform.position;

					if (objectCount == 1) {
						position.x += (0 + parentPosition.x);
					} else if (objectCount == 2) {
						position.x += (10 + parentPosition.x);
					} else if (objectCount == 3) {
						position.x += (15 + parentPosition.x);
					} else if (objectCount == 4) {
						position.x += (10 + parentPosition.x);
					} else if (objectCount == 5) {
						position.x += (0 + parentPosition.x);
					} else if (objectCount == 6) {
						position.x = (parentPosition.x - 10);
					} else if (objectCount == 7) {
						position.x = (parentPosition.x - 15);
					} else if (objectCount == 8) {
						position.x = (parentPosition.x - 10);
					}
					
					position.z += parentPosition.z;
					
					if (objectCount == 1) {
						position.y += (15 + parentPosition.y);
					} else if (objectCount == 2) {
						position.y += (10 + parentPosition.y);
					} else if (objectCount == 3) {
						position.y += (0 + parentPosition.y);
					} else if (objectCount == 4) {
						position.y += (parentPosition.y - 10);
					} else if (objectCount == 5) {
						position.y += (parentPosition.y - 15);
					} else if (objectCount == 6) {
						position.y += (parentPosition.y - 10);
					} else if (objectCount == 7) {
						position.y += (0 + parentPosition.y);
					} else if (objectCount == 8) {
						position.y += (10 + parentPosition.y);
					}
					
					GameObject folderNode = (GameObject)Instantiate (folder.gameObject, position, Quaternion.identity);
					folderNode.GetComponent<TreeNode> ().parent = currentNode;
					folderNode.GetComponent<TreeNode> ().drawLine (currentNode);
					folderNode.GetComponent<TreeNode> ().setId (path [path.Length - 1]);
					folderNode.GetComponent<TreeNode> ().setPath (str);
					folderNode.GetComponent<TreeNode> ().setFileType (TreeNode.FileType.Folder);
					folderNode.GetComponent<TreeNode> ().setObjectPosition (objectCount);
					folderNode.GetComponentInChildren<TextMesh> ().text = path [path.Length - 1];
					folderNode.GetComponentInChildren<TextMesh> ().GetComponent<MeshRenderer> ().sortingOrder = 1;


					currentNode.GetComponent<TreeNode> ().addChild (folderNode);
					nodes.Add (folderNode);
					dirs.Push (folderNode);
					folderCount++;
					objectCount++;
				}
			
				// Iterate through the files
				foreach (string fileName in files) {
					
					string [] path = fileName.Split (slash);
					
					//Vector3 position = folderPositions[folderCount];
					Vector3 position = positions[objectCount];

					Vector3 parentPosition = currentNode.transform.position;

					if (objectCount == 1) {
						position.x += (0 + parentPosition.x);
					} else if (objectCount == 2) {
						position.x += (10 + parentPosition.x);
					} else if (objectCount == 3) {
						position.x += (15 + parentPosition.x);
					} else if (objectCount == 4) {
						position.x += (10 + parentPosition.x);
					} else if (objectCount == 5) {
						position.x += (0 + parentPosition.x);
					} else if (objectCount == 6) {
						position.x = (parentPosition.x - 20);
					} else if (objectCount == 7) {
						position.x = (parentPosition.x - 25);
					} else if (objectCount == 8) {
						position.x = (parentPosition.x - 20);
					}
					
					position.z += parentPosition.z;
					
					if (objectCount == 1) {
						position.y += (15 + parentPosition.y);
					} else if (objectCount == 2) {
						position.y += (10 + parentPosition.y);
					} else if (objectCount == 3) {
						position.y += (0 + parentPosition.y);
					} else if (objectCount == 4) {
						position.y += (parentPosition.y - 10);
					} else if (objectCount == 5) {
						position.y += (parentPosition.y - 15);
					} else if (objectCount == 6) {
						position.y += (parentPosition.y - 10);
					} else if (objectCount == 7) {
						position.y += (0 + parentPosition.y);
					} else if (objectCount == 8) {
						position.y += (10 + parentPosition.y);
					}
					
					GameObject folderNode = (GameObject)Instantiate (file.gameObject, position, Quaternion.identity);
					folderNode.GetComponent<TreeNode> ().parent = currentNode;
					folderNode.GetComponent<TreeNode> ().drawLine (currentNode);
					folderNode.GetComponent<TreeNode> ().setId (path [path.Length - 1]);
					folderNode.GetComponent<TreeNode> ().setPath (fileName);
					folderNode.GetComponent<TreeNode> ().setFileType (TreeNode.FileType.Folder);
					folderNode.GetComponent<TreeNode> ().setObjectPosition (objectCount);
					folderNode.GetComponentInChildren<TextMesh> ().text = path [path.Length - 1];
					folderNode.GetComponentInChildren<TextMesh> ().GetComponent<MeshRenderer> ().sortingOrder = 1;

					currentNode.GetComponent<TreeNode> ().addChild (folderNode);
					nodes.Add (folderNode);
					fileCount++;
					objectCount++;
				}
			}

			// Move to the next directory 
			directoryCount++;
			objectCount = 1;
		}
	}
}

