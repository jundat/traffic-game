using UnityEngine;
using System.Collections;

public class ModelUserNetwork  {

	public string username;
	public string name;
	public int level;
	public string info;

	public ModelUser ToModelUser () {
		ModelUser u = new ModelUser ();
		u.username = this.username;
		u.name = this.name;
		u.level = this.level;
		u.info = this.info;

		return u;
	}
}
