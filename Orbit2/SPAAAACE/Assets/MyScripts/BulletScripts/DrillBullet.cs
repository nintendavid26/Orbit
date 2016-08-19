using UnityEngine;
using System.Collections;

public class DrillBullet : BulletShot {
    public enum State { OnPlanet, InSpace, MovingForward, MovingBackwards };
    public State state;
    public Vector3 ContactPoint;
    public Vector3 AdjustedPoint;
    public bool OnPlanet;
    public int newspeed;
    public int number;
    public int number1;
	// Use this for initialization
    void Start()
    {
        state = State.InSpace;
        Destroy(this.gameObject, 10);
    }
	// Update is called once per frame
    void OnTriggerEnter(Collider col)
    {
        if (!OnPlanet) { 
        if (col.gameObject.tag == "Planet")
        {
           // ContactPoint = transform.position;
            //AdjustedPoint = new Vector3(ContactPoint.x, ContactPoint.y, Mathf.Sqrt(3.5f * 3.5f - ContactPoint.x * ContactPoint.x - ContactPoint.y * ContactPoint.y));
            //transform.position = AdjustedPoint;
            //transform.position = ContactPoint;
            number = Random.Range(-150, 150);
            number1 = Random.Range(-150, 150);
            state = State.OnPlanet;
        }
    }
        if (col.gameObject.tag == "Enemy")
        {
            Enemy Enemy = col.GetComponent<Enemy>();
            Enemy.Hp -= Power;
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
       
        if (state==State.OnPlanet)
        {
            transform.LookAt(new Vector3(0, 0, 0));
            transform.RotateAround(Vector3.zero, Vector3.up, number * Time.deltaTime * newspeed);
            transform.RotateAround(Vector3.zero, Vector3.left, number1 * Time.deltaTime * newspeed);
        }
        /*else if (state == State.MovingForward) { 
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            if (Vector3.Distance(transform.position, new Vector3(0, 0, 0)) >= 3.49){ state = State.MovingBackwards; }
            else if (Vector3.Distance(transform.position, new Vector3(0, 0, 0)) <= 3.51){state=State.OnPlanet;}
        }
        else if(state==State.MovingBackwards ){
            transform.Translate(Vector3.back * Time.deltaTime * speed);
            if (Vector3.Distance(transform.position, new Vector3(0, 0, 0)) >= 3.49) { state = State.OnPlanet; }
            else if (Vector3.Distance(transform.position, new Vector3(0, 0, 0)) <= 3.51) { state = State.MovingForward; }
        }*/
        else if(state==State.InSpace) { 
        if (speed > 0)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        if (CoolDownTime > 0)
        {

            CoolDownTime -= Time.deltaTime;
        }
        if (CoolDownTime < 0) { CoolDownTime = 0; }
        }
    }
}
