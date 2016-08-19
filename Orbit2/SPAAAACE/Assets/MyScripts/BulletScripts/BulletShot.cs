using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//Base Bullet Class
public class BulletShot : MonoBehaviour
{
    #region Variables
    public static Camera main;
    public int speed;
	GameObject Bullet;
    public int Power;
    public int Ammo;
    public int MaxAmmo;
    public string Name;
    public string Description;
    public int ReloadAmount;
    public float WeaponCoolDown;
    public float CoolDownTime;
    #endregion

    // Update is called once per frame

	public virtual void Update () {
        if (speed > 0)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        if (CoolDownTime  > 0)
        {

            CoolDownTime -= Time.deltaTime;
        }
        if (CoolDownTime < 0) { CoolDownTime = 0; }
	}
}
