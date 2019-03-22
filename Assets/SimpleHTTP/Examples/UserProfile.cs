using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserProfile {
	public int id;
	public string type;
	public Groups groups;

	public UserProfile(int id, string type, string group1, string group2) {
		this.id = id;
		this.type = type;
		this.groups = new Groups(group1, group2);
	}
}
	
[System.Serializable]
public class Groups {
	public string identifier1;
	public string identifier2;

	public Groups(string identifier1, string identifier2) {
		this.identifier1 = identifier1;
		this.identifier2 = identifier2;
	}
}