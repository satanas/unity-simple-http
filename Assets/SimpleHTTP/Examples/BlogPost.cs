using System;

[Serializable]
public class BlogPost {
	public string title;
	public string body;
	public int userId;

	public BlogPost(string title, string body, int userId) {
		this.title = title;
		this.body = body;
		this.userId = userId;
	}
}
