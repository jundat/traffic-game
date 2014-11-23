using UnityEngine;
using System.Collections;

public class ModelUserNetwork  {

	public string username;
	public string name;

	public ModelUser ToModelUser () {
		ModelUser u = new ModelUser ();
		u.username = this.username;
		u.name = this.name;

		return u;
	}
}
