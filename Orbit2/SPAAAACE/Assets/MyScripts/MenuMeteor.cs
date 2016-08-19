using UnityEngine;
using System.Collections;

public class MenuMeteor : MonoBehaviour {
    public GameObject planet;
    public Rigidbody rb;
	// Use this for initialization
	void Start () {
        rb = this.GetComponent<Rigidbody>();
        rb.angularVelocity = Random.insideUnitSphere * 5;
	}
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(planet.transform.position, Vector3.up, Time.deltaTime*40);
	}
}
