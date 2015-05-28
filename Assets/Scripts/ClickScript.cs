using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ClickScript : MonoBehaviour {

	private RaycastHit hit;
	private Ray ray;
	public GameObject inputField;

	public GameObject homeFolder;
	public GameObject favourite1;
	public GameObject favourite2;
	public GameObject favourite3;

	public ArrayList nodes;
	public GameObject [] searchFiles = new GameObject[5];

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetMouseButtonDown(0))
		{
			ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			
			if(Physics.Raycast (ray.origin, ray.direction, out hit))
			{

				switch(hit.collider.name){
				case "Restart":
					Application.LoadLevel("Main 1");
					break;
				case "Novice":

					if (!System.IO.Directory.Exists ("C:\\Users\\Zac\\Documents\\file-browser")) {
						
						// Using unix
						PlayerPrefs.SetString("rootPath", "/Users/Zac/Documents/file-browser");
						PlayerPrefs.SetString("currentDirectory", "/Users/Zac/Documents/file-browser/HCI Novice");
					}else{
						PlayerPrefs.SetString("rootPath", "C:\\Users\\Zac\\Documents\\file-browser");
						PlayerPrefs.SetString("currentDirectory", "C:\\Users\\Zac\\Documents\\file-browser\\HCI Novice");
					}
					Application.LoadLevel("Main");

					break;
				case "Casual":
					
					if (!System.IO.Directory.Exists ("C:\\Users\\Zac\\Documents\\file-browser")) {
						
						// Using unix
						PlayerPrefs.SetString("rootPath", "/Users/Zac/Documents/file-browser");
						PlayerPrefs.SetString("currentDirectory", "/Users/Zac/Documents/file-browser/HCI Moderate");
					}else{
						PlayerPrefs.SetString("rootPath", "C:\\Users\\Zac\\Documents\\file-browser");
						PlayerPrefs.SetString("currentDirectory", "C:\\Users\\Zac\\Documents\\file-browser\\HCI Moderate");
					}
					Application.LoadLevel("Main");
					break;
				case "Expert":
					
					if (!System.IO.Directory.Exists ("C:\\Users\\Zac\\Documents\\file-browser")) {
						
						// Using unix
						PlayerPrefs.SetString("rootPath", "/Users/Zac/Documents/file-browser");
						PlayerPrefs.SetString("currentDirectory", "/Users/Zac/Documents/file-browser/HCI Expert");
					}else{
						PlayerPrefs.SetString("rootPath", "C:\\Users\\Zac\\Documents\\file-browser");
						PlayerPrefs.SetString("currentDirectory", "C:\\Users\\Zac\\Documents\\file-browser\\HCI Expert");
					}
					Application.LoadLevel("Main");
					break;
				case "Home":	
					Vector3 position = new Vector3(homeFolder.transform.position.x,homeFolder.transform.position.y,homeFolder.transform.position.z - 75.1f);
					Camera.main.transform.position = position;
					break;
				case "Folder 1":	
					position = new Vector3(favourite1.transform.position.x,favourite1.transform.position.y,favourite1.transform.position.z - 75.1f);
					Camera.main.transform.position = position;
					break;
				case "Folder 2":	
					position = new Vector3(favourite2.transform.position.x,favourite2.transform.position.y,favourite2.transform.position.z - 75.1f);
					Camera.main.transform.position = position;
					break;
				case "Back":	
					Application.LoadLevel("Main");
					break;
				case "Foward":	

					for(int i = 0; i < nodes.Count; i++)
					{
						//Debug.Log (((GameObject)nodes[i]).GetComponent<TreeNode>().ID);
						if(((GameObject)nodes[i]).GetComponent<TreeNode>().ID.ToLower() == "First Year".ToLower())
						{
							Color yellow = new Color(1f, 0.92f, 0.016f, 1f);
							((GameObject)nodes[i]).transform.GetComponent<LineRenderer>().SetColors(yellow,yellow);
							
							if(((GameObject)nodes[i]).GetComponent<TreeNode>().parent != null)
							{
								activateParentLine(((GameObject)nodes[i]).GetComponent<TreeNode>().parent);
							}
						}
					}

					break;
				}
			}
		}

		if (Input.GetKeyUp(KeyCode.Return)) {

			string text = inputField.GetComponent<InputField>().text;
			//search (text);

			for(int i = 0; i < nodes.Count; i++)
			{
				//Debug.Log (((GameObject)nodes[i]).GetComponent<TreeNode>().ID);
				if(((GameObject)nodes[i]).GetComponent<TreeNode>().ID.ToLower() == text.ToLower())
				{
					Color yellow = new Color(1f, 0.92f, 0.016f, 1f);
					((GameObject)nodes[i]).transform.GetComponent<LineRenderer>().SetColors(yellow,yellow);

					if(((GameObject)nodes[i]).GetComponent<TreeNode>().parent != null)
					{
						activateParentLine(((GameObject)nodes[i]).GetComponent<TreeNode>().parent);
					}
				}
			}

//			string text = inputField.GetComponent<InputField>().text;
//
//			if(!text.Equals("Filename here"))
//			{
//				foreach(GameObject currentNode in searchFiles)
//				{
//					if(currentNode != null)
//					{
//						Color yellow = new Color(1f, 0.92f, 0.016f, 1f);
//						currentNode.transform.GetComponent<LineRenderer>().SetColors(yellow,yellow);
//					}
//				}
//			}else
//			{
//				Application.LoadLevel("Main");
//			}


			//Debug.Log (text);
		}
	}

	void activateParentLine(GameObject parent)
	{
		Color yellow = new Color(1f, 0.92f, 0.016f, 1f);
		parent.transform.GetComponent<LineRenderer>().SetColors(yellow,yellow);

		if(parent.GetComponent<TreeNode>().parent != null)
		{
			activateParentLine(parent.GetComponent<TreeNode>().parent);
		}
	}

	void search(string fileName)
	{
		Queue queue = new Queue ();
		homeFolder.GetComponent<TreeNode> ().visited = true;
		queue.Enqueue (homeFolder);
		
		while(queue.Count != 0)
		{
			GameObject currentNode = (GameObject)queue.Dequeue();

			foreach(KeyValuePair<string, TreeNode> entry in currentNode.GetComponent<TreeNode>().getChildren())
			{
				if(entry.Value.ID == fileName)
				{
					Color yellow = new Color(1f, 0.92f, 0.016f, 1f);
					entry.Value.getGameObject().transform.GetComponent<LineRenderer>().SetColors(yellow,yellow);
				}
				
				entry.Value.visited = true;
				queue.Enqueue(entry.Value.getGameObject());
			}
		}
	}

}

