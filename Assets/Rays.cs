using UnityEngine;
using System.Collections;

public class Rays : MonoBehaviour {

	public GameObject [] rootPairs = new GameObject[10];


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		for(int i = 0; i < 5; i++)
		{
			if(rootPairs[i ] != null)
			{
			Vector3 forward = rootPairs[i + 1].transform.TransformDirection(Vector3.forward) * 10;
			Debug.DrawRay (rootPairs[i].transform.position, forward, Color.green);

			i++;
			}
		}


	}
}
