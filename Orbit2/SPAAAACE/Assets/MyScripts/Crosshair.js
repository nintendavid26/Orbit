#pragma strict

var crosshairTexture : Texture2D;
 var position : Rect;
 static var OriginalOn = true;
 var constant=25;
 function Start()
 {
    // print 
     position = Rect((Screen.width - constant) / 2, (Screen.height - 
        constant) /2, constant, constant);
    // position=Rect(Screen.width/2,100,100,100);
 }
 
 function OnGUI()
 {
     if(OriginalOn == true)
     {
         GUI.DrawTexture(position, crosshairTexture);
     }
 }
 
 