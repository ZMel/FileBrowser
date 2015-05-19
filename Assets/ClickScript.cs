using UnityEngine;
using System.Collections;

public class ClickScript : MonoBehaviour {

	private RaycastHit hit;
	private Ray ray;
	public GameObject homeFolder;
	public GameObject favourite1;
	public GameObject favourite2;
	public GameObject favourite3;

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
				}
			}
		}
	}
}
