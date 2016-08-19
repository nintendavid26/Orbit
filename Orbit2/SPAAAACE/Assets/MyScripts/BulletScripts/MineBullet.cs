using UnityEngine;
using System.Collections;

public class MineBullet : BulletShot {
    bool attached = false;
    public Light Glow;
    void Start()
    {
        Glow = this.gameObject.AddComponent<Light>();
        Glow.color=Color.red;
        Glow.intensity = 6;
        Glow.enabled = false;

    }
    IEnumerator OnTriggerEnter(Collider col)
    {
        if (!attached)
        {
            if (col.gameObject.tag == "Planet")
            {
                attached = true;
                speed = 0;
                Glow.enabled = true;
                Debug.Log("Mine Hit");
                yield return new WaitForSeconds(3);
                Explode();
            }
            if (col.gameObject.tag == "Enemy")
            {
                attached = true;
                Enemy Enemy = col.GetComponent<Enemy>();
                transform.parent = Enemy.transform;
                yield return new WaitForSeconds(3);
                Enemy.Hp -= Power;
            }
        }
    }
    void Explode() {
        Destroy(this.gameObject);
    }
}
