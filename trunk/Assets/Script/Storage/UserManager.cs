using UnityEngine;
using System.Collections;

public class UserManager : Singleton<UserManager> {

	public ModelUser user = new ModelUser ();

	public string password;

	public string token;

}
