using UnityEngine;
using System.Collections;

public class PlayerAction : MonoBehaviour {

	public GameObject hookMotion;
	public Transform firePoint;
	public float projectileSpeed = 20f;

	public static bool hookBlocked = false;

	void Start () {

		if (hookMotion.Equals (null)) {
			Debug.Log ("hookMotion object is not set");
			Debug.Break ();
		}
	}

	void Update () 
	{
		if (Input.GetButtonDown ("Fire1")) {
			GameObject hookShot = (GameObject)Instantiate (hookMotion, firePoint.position, firePoint.rotation);
			Rigidbody hookRigidbody = hookShot.GetComponent <Rigidbody> ();
			hookRigidbody.velocity = Camera.main.transform.forward * projectileSpeed;
		}
	}
}
