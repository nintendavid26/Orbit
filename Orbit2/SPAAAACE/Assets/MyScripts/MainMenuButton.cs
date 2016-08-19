using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour {

    public AudioClip MenuSong;
    public AudioSource Source;
    public enum Modes {BossRush,Survival,TimeAttack };
    public Modes mode;
    public string LevelToGoTo;

    #region Scenes
    int Start_Menu = 0;
    int Rules = 1;
    int Main_Game = 2;
    int Mode_Select = 3;
    int Game_Over = 4;
    int Weapon_Info1 = 5;
    int Weapon_Info2 = 6;
    #endregion
    public void GoToScene()
    {
        SceneManager.LoadScene(LevelToGoTo);
    }
   public void GoToRules()
    {
        SceneManager.LoadScene(1);
    }
   public void GoToModeSelect() { SceneManager.LoadScene(3); }
   public void GoToMainMenu()
   {
       SceneManager.LoadScene(0);
   }
   public void GoToGame(string Mode)
    {
        PlayerPrefs.SetString("Mode", Mode);
        SceneManager.LoadScene(2);
    }
   public void GoToGame()
   {
       SceneManager.LoadScene(2);
   }
   public void GoToWeaponScreen()
   {
       SceneManager.LoadScene(5);
   }
   public void DisplayGameOverScore() { }
}
