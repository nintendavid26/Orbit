using UnityEngine;
using System.Collections;

public class HomingBullet : BulletShot {
    public GameObject closestEnemy;
    public void Start()
    {
        GameObject[] enemies=GameObject.FindGameObjectsWithTag("Enemy");
            GameObject bestTarget = null;
            float closestDistanceSqr = Mathf.Infinity;
            Vector3 currentPosition = transform.position;
            foreach (GameObject potentialTarget in enemies)
            {
                Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    bestTarget = potentialTarget;
                }
            }

            closestEnemy=bestTarget;
        }

    public override void Update()
    {

        if (speed > 0)
        {
            // transform.Translate(Vector3.forward * Time.deltaTime * speed);
            if (closestEnemy == null) { transform.Translate(Vector3.forward * Time.deltaTime * speed); }
            else { transform.position = Vector3.MoveTowards(transform.position, closestEnemy.transform.position, Time.deltaTime * speed); }
        }
        if (CoolDownTime > 0)
        {

            CoolDownTime -= Time.deltaTime;
        }
        if (CoolDownTime < 0) { CoolDownTime = 0; }
        
        Debug.Log("Move");
        
    }
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
