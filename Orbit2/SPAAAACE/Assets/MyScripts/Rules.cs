using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Rules : MonoBehaviour {
   // public GUIStyle Style;
    public Text rules;
    public Rect rect;
    public RectTransform rekt;
	// Use this for initialization
	void Start () {
        rekt = rules.GetComponent<RectTransform>();
        rect = rules.GetComponent<Rect>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        rect.height =Screen.height*9/10;
        rect.width = Screen.width;


    }
}
