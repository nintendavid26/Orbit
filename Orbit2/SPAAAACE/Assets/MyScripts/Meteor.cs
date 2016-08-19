using UnityEngine;
using System.Collections;

public class Meteor : MonoBehaviour {

	 public float tumble;
     public float speed;
     public Rigidbody rb;
     public bool TowardsPlayer;
     Vector3 target;
     Vector3 Direction;
    void Start ()
    {
        if (TowardsPlayer)
        {
            //Vector3 RandomPoint = Random.insideUnitSphere;
            transform.LookAt(Camera.main.transform.position);
            Direction = transform.forward * speed;
            rb = this.GetComponent<Rigidbody>();
            rb.angularVelocity = Random.insideUnitSphere * tumble;
        }
        else
        {
            Direction = transform.forward * speed;
            rb = this.GetComponent<Rigidbody>();
            rb.angularVelocity = Random.insideUnitSphere * tumble;
        }
       /* target = Camera.main.transform.position-transform.position;
        target = target - transform.position;
        target = transform.position + target.normalized * 300.0f;
        Direction = transform.position - Camera.main.transform.position;
        Direction.Normalize();  */
        Destroy(this.gameObject, 10);
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Direction * Time.deltaTime, Space.World);
	}
}
