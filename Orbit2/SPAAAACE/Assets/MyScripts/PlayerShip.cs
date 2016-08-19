using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerShip : MonoBehaviour
{
    #region Variables
    public bool AxisTesting;
    public GameObject target;
    public BulletShot CurrentBullet;
    public Vector3 AxisD=Vector3.down;
    public List<BulletShot> TypesOfShots;
    public int ShotNumber=0;
    public int speed;
    public float MaxHealth;
    public float Health;
    public float HealthRefillAmount;
    public Transform camTransform;
    #region Shake
    // How long the object should shake for.
    public float shake = 0;
    public float ShakeTime=1;
    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 1.0f;
    public float decreaseFactor = 2.0f;
	public Vector3 originalPos;
    public int TIME_UNTIL_CAN_MOVE = -2;
    public int TIME_UNTIL_CAN_GET_HIT = -4;
    #endregion
    public Transform sphere;
    public float turnSpeed = 45.0f;
    public float moveSpeed = 45.0f;
    private Transform center;
    public AudioClip ShootSound,CrashSound,ClickSound;
    public static AudioSource source;
    public float volHigh=0.5f;
    public float volLow=1.0f;
    public Image HealthBar;
    public Text HealthNum;
    string infinity = "∞";
    public string WeaponAmmoString;
    public Text WeaponAmmoText;
    public bool Paused=false;
    public GameObject InFront;
    //For SpreadShot
    public GameObject InFrontU;
    public GameObject InFrontR;
    public GameObject InFrontL;
    public GameObject InFrontD;
    public Image CoolDownTimer;
    public enum shakeState {shaking,invincible,normal}
    public shakeState currentShakeState=shakeState.normal;
    #endregion

    public static void PlaySound(AudioClip Sound,float volume) {
       source.PlayOneShot(Sound, volume);} 

    void Start() {
        center = new GameObject().transform;
        center.parent = sphere;
        transform.parent = center;
        CurrentBullet = TypesOfShots[ShotNumber];
        Health = MaxHealth;
        TypesOfShots.ForEach(SetWeaponsToMaxAmmo);
        InvokeRepeating("AmmoRefill", 0, 3);
        InvokeRepeating("HealthRefill",0,5);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Hazard" && currentShakeState==shakeState.normal)
        {
            
            PlaySound(CrashSound, 1.2f);
            Health -= 10;
            currentShakeState = shakeState.shaking;
            Destroy (col.gameObject);
           if(shake<=0){ originalPos = camTransform.localPosition;}
            shake = ShakeTime;
            if (Health <= 0) {
                Scoring.GameOver();
            }
        }

    }

    void Awake()
    {
        source=GetComponent<AudioSource>();
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

	void Update ()
    {
        #region GUI Stuff
        //Gui Stuff
        float HealthPercentage = Health / MaxHealth;
        HealthBar.fillAmount =HealthPercentage;
        HealthNum.text = Health + "/" + MaxHealth;
        UpdateWeaponAmmo();
        #endregion

        #region Shake
        //Shake
        if (shake > -2) { shake -= Time.deltaTime * decreaseFactor; }
        if (shake > 0 && currentShakeState==shakeState.shaking)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

        }
        if (shake <= 0 &&shake > -1 && currentShakeState==shakeState.shaking) {
            camTransform.localPosition = originalPos;
            currentShakeState = shakeState.invincible;
        }
        if (shake < -1) { shake = -1; currentShakeState = shakeState.normal; }
        #endregion

        #region Controls/Movement
        //Controls
        if (Input.GetKeyDown("p")) { }
        if (Input.GetKeyDown("z")) {
            ShotNumber--;
            if (ShotNumber == -1) { ShotNumber = TypesOfShots.Count-1; }
                CurrentBullet = TypesOfShots[ShotNumber];
        }
        if (Input.GetKeyDown("x")) { ShotNumber++;
            if (ShotNumber == TypesOfShots.Count) { ShotNumber = 0; }
            CurrentBullet = TypesOfShots[ShotNumber];
        }
            //Movement
        if (shake <= 0) //Not Shaking
        {
            if (Input.GetKeyDown("space") && CurrentBullet.Ammo > 0)
            {
                Shoot(CurrentBullet);
            }
            else if (Input.GetKeyDown("space") && CurrentBullet.Ammo == 0)
            {
                PlaySound(ClickSound, 1.0f);
            }
            if (!AxisTesting)
            {
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    Vector3 v3Axis = Vector3.Cross(center.position - transform.position, transform.right);
                    center.rotation = Quaternion.AngleAxis(moveSpeed * Time.deltaTime, v3Axis) * center.rotation;
                }
                else if (Input.GetKey(KeyCode.RightArrow))
                {
                    Vector3 v3Axis = -Vector3.Cross(center.position - transform.position, transform.right);
                    center.rotation = Quaternion.AngleAxis(moveSpeed * Time.deltaTime, v3Axis) * center.rotation;
                }
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    Vector3 v3Axis = -Vector3.Cross(center.position - transform.position, transform.forward);
                    center.rotation = Quaternion.AngleAxis(moveSpeed * Time.deltaTime, v3Axis) * center.rotation;
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    Vector3 v3Axis = Vector3.Cross(center.position - transform.position, transform.forward);
                    center.rotation = Quaternion.AngleAxis(moveSpeed * Time.deltaTime, v3Axis) * center.rotation;
                }
            }
            else
            {
                Vector3 v3Axis = Vector3.Cross(center.position - transform.position, transform.forward);
                center.rotation = Quaternion.AngleAxis(-Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime, v3Axis) * center.rotation;
                Vector3 v3Axis2 = Vector3.Cross(center.position - transform.position, transform.right);
                center.rotation = Quaternion.AngleAxis(-Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, v3Axis2) * center.rotation;
                //Debug.Log("Vertical=" + Input.GetAxis("Vertical") + "Horizontal=" + Input.GetAxis("Horizontal"));
            }
        }
  
        #endregion
    }

    void UpdateWeaponAmmo()
    {
        WeaponAmmoString = "";
        for (int i = 0; i < TypesOfShots.Count; i++)
        {
            string space1="";
            string pointer=""; //Note to self make arrow go straight
            if (TypesOfShots[i].Ammo < 100) {  }
            string n = "\n";
            if (i == TypesOfShots.Count - 1) { n = ""; }
            if (i == ShotNumber) { pointer = "-> "; }
            WeaponAmmoString = WeaponAmmoString +pointer+ TypesOfShots[i].Name+" "+space1+TypesOfShots[i].Ammo+"/"+TypesOfShots[i].MaxAmmo+n;   
        }
            WeaponAmmoText.text = WeaponAmmoString;
    }
    void SetWeaponsToMaxAmmo(BulletShot Type)
    {
        Type.Ammo = Type.MaxAmmo;
    }
    void AmmoRefill()
    {
        for (int i = 0; i < TypesOfShots.Count; i++)
        {
            TypesOfShots[i].Ammo += TypesOfShots[i].ReloadAmount;
            if (TypesOfShots[i].Ammo > TypesOfShots[i].MaxAmmo) { TypesOfShots[i].Ammo = TypesOfShots[i].MaxAmmo; }
        }
        
    }
    void HealthRefill()
    {
        Health += HealthRefillAmount;
        if (Health > MaxHealth) { Health = MaxHealth; }
    }
    void OnGuI()
    {

    }
    void Pause() {
        //Pause Game
    }
    void Shoot(BulletShot TypeOfShot) {
        PlaySound(ShootSound, 0.5f);
        // Vector3 InFront = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        if (TypeOfShot.Name != "Normal") { TypeOfShot.Ammo--; }
        if (TypeOfShot.Name == "Spread")
        {
            Instantiate(TypeOfShot, InFrontU.transform.position, transform.rotation);
            Instantiate(TypeOfShot, InFrontR.transform.position, transform.rotation);
            Instantiate(TypeOfShot, InFrontL.transform.position, transform.rotation);
            Instantiate(TypeOfShot, InFrontD.transform.position, transform.rotation);
        }
        else
        {
            Instantiate(TypeOfShot, InFront.transform.position, transform.rotation);
        }
    }
	}
