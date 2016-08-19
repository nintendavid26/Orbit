using UnityEngine;
using System.Collections;

public class PoisonBullet : BulletShot {
    public int PoisonDamage;
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Planet")
        {
            Destroy(this.gameObject);
        }
        if (col.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }

  
}
