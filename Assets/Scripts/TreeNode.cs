﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TreeNode : MonoBehaviour
{
	public string ID;
	public string path;
	GameObject thisObject;
	
	FileType fileType;
	public enum FileType {RootNode, Folder, File, Back};
	public int objectPosition = 0;
	public TreeNode Parent { get; private set; }
	private Dictionary<string, TreeNode> _children = new Dictionary<string, TreeNode>();
	public GameObject parent;
	public bool visited = false;

	void Start()
	{
		if(fileType == FileType.RootNode && parent != null)
		{
			Vector3 position = parent.transform.position;
			//position.z += 5;

			transform.GetComponent<LineRenderer>().SetPosition(0, position);
			transform.GetComponent<LineRenderer>().sortingOrder = -2;

			position = transform.position;
			//position.z += 5;

			transform.GetComponent<LineRenderer>().SetPosition(1, position);
			transform.GetComponent<LineRenderer>().sortingOrder = -2;
		}
	}

	public void drawLine(GameObject _parent)
	{
		parent = _parent;

		Vector3 position = parent.transform.position;
		//position.z += 5;
		
		transform.GetComponent<LineRenderer>().SetPosition(0, position);
		transform.GetComponent<LineRenderer>().sortingOrder = -2;

		position = transform.position;
		//position.z += 5;
		
		transform.GetComponent<LineRenderer>().SetPosition(1, position);
		transform.GetComponent<LineRenderer>().sortingOrder = -2;
	}

	// Mutators
	public void setId(string id)
	{
		this.ID = id;
	}

	public void setPath(string path)
	{
		this.path = path;
	}

	public void setObjectPosition(int _objectPosition)
	{
		this.objectPosition = _objectPosition;
	}


	public void setFileType(FileType fileType)
	{
		this.fileType = fileType;
	}

	public void setGameObject (GameObject thisObject)
	{
		this.thisObject = thisObject;
	}

	public void addChild(GameObject item)
	{
		if (item.GetComponent<TreeNode>().Parent != null)
		{
			item.GetComponent<TreeNode>().Parent._children.Remove(item.GetComponent<TreeNode>().ID);
		}
		
		item.GetComponent<TreeNode>().Parent = this;
		this._children.Add(item.GetComponent<TreeNode>().path, item.GetComponent<TreeNode>());
	}

	// Getters
	public TreeNode getChild(string id)
	{
		return this._children [id];
	}

	public Dictionary<string, TreeNode> getChildren()
	{
		return _children;
	}

	public int numberOfChildred
	{
		get { return this._children.Count; }
	}

	public FileType getType()
	{
		return fileType;
	}

	
	public int getPosition()
	{
		return objectPosition;
	}
	
	public GameObject getGameObject()
	{
		return thisObject;
	}


	// Actions
	void OnMouseDown(){



		Vector3 position = new Vector3 ( transform.position.x, transform.position.y, transform.position.z - 100f);
		Camera.main.transform.position = position;
		/*else if ((fileType == TreeNode.FileType.Folder) || (fileType == TreeNode.FileType.Back)) {
			//GameObject.Find("Controller").GetComponent<FileBrowser>().reload(this);

		} else if (fileType == TreeNode.FileType.File){
			//Debug.Log("File");
		}*/
	}   
}
