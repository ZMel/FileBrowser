using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ClickScript : MonoBehaviour {

	private RaycastHit hit;
	private Ray ray;
	public GameObject inputField;

	public GameObject homeFolder;
	public GameObject favourite1;
	public GameObject favourite2;
	public GameObject favourite3;

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
					string text = inputField.GetComponent<InputField>().text;
					
					if(!text.Equals("Filename here"))
					{
						Application.LoadLevel("Main");
					}

					break;
				case "Foward":	
					text = inputField.GetComponent<InputField>().text;
					
					if(text.Equals("Filename here"))
					{
						foreach(GameObject currentNode in searchFiles)
						{
							if(currentNode != null)
							{
								Color yellow = new Color(1f, 0.92f, 0.016f, 1f);
								currentNode.transform.GetComponent<LineRenderer>().SetColors(yellow,yellow);
							}
						}

						inputField.GetComponent<InputField>().text = "Folder 3";
					}

					break;
				}
			}
		}

		if (Input.GetKeyUp(KeyCode.Return)) {

			string text = inputField.GetComponent<InputField>().text;

			if(!text.Equals("Filename here"))
			{
				foreach(GameObject currentNode in searchFiles)
				{
					if(currentNode != null)
					{
						Color yellow = new Color(1f, 0.92f, 0.016f, 1f);
						currentNode.transform.GetComponent<LineRenderer>().SetColors(yellow,yellow);
					}
				}
			}else
			{
				Application.LoadLevel("Main");
			}


			//Debug.Log (text);
		}
}
}

