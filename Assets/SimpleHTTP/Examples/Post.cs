using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Post {
	private string title;
	private string body;
	private int userId;

	public Post(string title, string body, int userId) {
		this.title = title;
		this.body = body;
		this.userId = userId;
	}
}
