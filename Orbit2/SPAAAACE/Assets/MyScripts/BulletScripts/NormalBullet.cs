using UnityEngine;
using System.Collections;

public class NormalBullet : BulletShot {
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Planet")
        {
            Destroy(this.gameObject);
        }
        if (col.gameObject.tag == "Enemy")
        {
            Enemy Enemy = col.GetComponent<Enemy>();
            Enemy.Hp -= Power;
        }
    }
}
