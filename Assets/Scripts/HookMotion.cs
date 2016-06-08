using UnityEngine;
using System.Collections;

public class HookMotion : MonoBehaviour {

	public GameObject hookFixed;
	public float lifetime = 1f;

	private Rigidbody hookRigidbody;
	
	void Start () {

		if (hookFixed.Equals (null)) {
			Debug.Log ("hookFixed object is not set");
			Debug.Break ();
		}

		hookRigidbody = GetComponent <Rigidbody> ();
		Destroy (gameObject, lifetime);
	}
		
	void OnCollisionEnter (Collision col) {
		hookRigidbody.velocity = Vector3.zero;
		GameObject anchor = (GameObject)Instantiate (hookFixed, transform.position, transform.rotation);
		Destroy (gameObject);
	}
}
