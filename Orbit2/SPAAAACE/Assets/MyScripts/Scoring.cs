using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class Scoring : MonoBehaviour {
    public bool testing;
    public Enemy enemy;
    public BossEnemy Boss;
    public float PlanetRadius;
    public static int WaveHp;
    public static int WaveNumber;
    static public int score;
    public Text Score;
    public static string Mode;
    public Text Counter;
    public int GameLength;
    public float StartTime;
	// Use this for initialization
	void Start () {
        if (!testing)
        {
            StartTime = Time.time;
            Mode = PlayerPrefs.GetString("Mode");
            if (Mode == "Time Attack") { }
            else if (Mode == "Survival") { }
            else if (Mode == "Boss Rush") { }
            WaveNumber = 0;
            score = 0;
            WaveHp = 10;
            InvokeRepeating("MakeEnemyWave", 5f, 10f);
        }
        else { WaveHp = 100; MakeEnemy(); }

	}
	
	// Update is called once per frame
	void Update () {

        Score.text = "Score: "+score+"\nWave: "+WaveNumber;
        float time = Time.time;
        Counter.text = CounterText();
        if (Enemy.NumberOfEnemies == 0&&Mode=="Time Attack") { MakeEnemyWave(); }
        if (180 - Time.time+StartTime<=0){GameOver();}
        
	}
    void MakeEnemy()
    {
        //gets a random point on the sphere
        float r = PlanetRadius;
        float x=Random.Range(-r,r);
        float y = Random.Range(-Mathf.Sqrt(r * r - x * x), Mathf.Sqrt(r * r - x * x));
        float z=Mathf.Sqrt(r*r-y*y-x*x);
        //Debug.Log("x="+x+"y="+y+"z="+z);
        Vector3 RandomPointOnPlanet=new Vector3(x,y,z);
        Instantiate(enemy,RandomPointOnPlanet,enemy.transform.rotation);
    }
    void MakeBoss()
    {
        float r = PlanetRadius;
        float x = Random.Range(-r, r);
        float y = Random.Range(-Mathf.Sqrt(r * r - x * x), Mathf.Sqrt(r * r - x * x));
        float z = Mathf.Sqrt(r * r - y * y - x * x);
        //Debug.Log("x="+x+"y="+y+"z="+z);
        Vector3 RandomPointOnPlanet = new Vector3(x, y, z);
        Instantiate(enemy, RandomPointOnPlanet, enemy.transform.rotation);
    }
    void MakeEnemyWave()
    {
            int x = Random.Range(1, 4);
        for (int n = 0; n < 4; n++)
        {
            MakeEnemy();
        }
        if(WaveNumber%3==0){WaveHp+=10;}
        WaveNumber++;
    }
    string CounterText()
    {
        if (Mode == "Time Attack") {
            string Text;
            Text = (180 - Time.time+StartTime).ToString("F2");
            return Text; 
        }
        else if (Mode == "Survival") { return "Survival"; }
        else if (Mode == "Boss Rush") { return "Boss Rush"; }
        else { return "No Mode Selected"; }
    }
    public static void GameOver()
    {
        if (Mode == "Time Attack") { PlayerPrefs.SetInt("Score", score); }
        SceneManager.LoadScene(4);
    }
}
