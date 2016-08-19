using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    
    #region Variables
    public GameObject target=null;
	public int number;
	public int number1;
    public int Hp;
    #region Meteors
    public GameObject Meteor1;
    public GameObject Meteor2;
    public GameObject Meteor0;
    public GameObject Meteor;
    Meteor Meteorm;
    #endregion
    public static int NumberOfEnemies;
    public Color[] colors = new Color[3];
    public AudioClip DeathSound,HitSound,SpawnSound,BGM;
    public float speed;
    public enum Status {Normal=1<<0, Poisoned=1<<1 };
    public Status MonsterStatus;
    int PoisonDamageToTake;
    #endregion

    void Start(){
        MonsterStatus = Status.Normal;
        speed = Random.value;
        if (speed < 0.5f) {speed+=0.5f; }
        PlayerShip.PlaySound(SpawnSound,1.0f);
        NumberOfEnemies++;
		number = Random.Range(-150,150);
		number1 = Random.Range(-150,150);
        Hp = Scoring.WaveHp;
        InvokeRepeating("Fire",2,2);
	}

    void Fire()
    {
        int x = Random.Range(0, 3);
        if (x == 0) { Meteor = Meteor0; }
        if (x == 1) { Meteor = Meteor1; }
        if (x == 2) { Meteor = Meteor2; }
        float Distance=Vector3.Distance(transform.position,Camera.main.transform.position);
        if(Distance<=9){//distance from camera to farthest visible point on planet
            Instantiate(Meteor, transform.position, transform.rotation);
            Meteor.GetComponent<Meteor>().TowardsPlayer = true;
        }
        else
        {
            Instantiate(Meteor, transform.position, transform.rotation);
            Meteor.GetComponent<Meteor>().TowardsPlayer = false;
        }
    }

    void OnDestroy() { NumberOfEnemies--; Scoring.score += 10; }


    IEnumerator OnTriggerEnter(Collider col)
    {
       Renderer renderer =this.GetComponent <Renderer>();
        if (col.gameObject.tag=="Bullet"){
         if (col.gameObject.GetComponent<BulletShot>().Name == "Poison") {
             if (MonsterStatus != Status.Poisoned)
             {
                 Debug.Log("Poisoned");
                 PoisonDamageToTake = col.gameObject.GetComponent<PoisonBullet>().PoisonDamage;
                 PlayerShip.PlaySound(HitSound, 0.5f);
                 MonsterStatus = Status.Poisoned;
                 renderer.material.color = colors[2];
                 colors[1] = colors[2];
                 InvokeRepeating("TakePoisonDamage", 0, 1);
             }
        }
         //else if (col.gameObject.GetComponent<BulletShot>().Name == "Mine")
         else
         {
             //Makes the enemy flash when hit
             PlayerShip.PlaySound(HitSound, 0.5f);
             renderer.material.color = colors[0];
             yield return new WaitForSeconds(.2f);
             renderer.material.color = colors[1];
         }
        }
    }
	
	void Update () {
        if (Hp <= 0) {
            PlayerShip.PlaySound(DeathSound,1.0f);
            Destroy(this.gameObject); }
		if (target != null) {
				transform.RotateAround (Vector3.zero, Vector3.up, number * Time.deltaTime*speed);
				transform.RotateAround (Vector3.zero, Vector3.left, number1 * Time.deltaTime*speed);}

}
    void TakePoisonDamage() {
        Hp -= PoisonDamageToTake;
    }
}
