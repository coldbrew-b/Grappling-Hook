using UnityEngine;
using System.Collections;

public class HookFixed : MonoBehaviour {

	public float lifetime = 1f;

	// Use this for initialization
	void Start () {
		Destroy (gameObject, lifetime);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
