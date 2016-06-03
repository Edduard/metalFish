#pragma strict

var buttonWidth:int = 200;
var buttonHeight:int = 50;
var spacing:int = 25;
var areaHeight:int = 400;

function Start () {

}



function OnGUI()
{
/*
	if(GUI.Button (Rect(0, 0, 200, 100), "New Game"))
	{
		Application.LoadLevel("TappyPlane");
	}
*/

	GUILayout.BeginArea(Rect(Screen.width/2 - buttonWidth/2, Screen.height/2 - areaHeight/2, buttonWidth, areaHeight));
	
		if(GUILayout.Button("New Game", GUILayout.Height(buttonHeight)))
		{
			Application.LoadLevel("TappyPlane");
		}
		
		GUILayout.Space(spacing);
		
		if(GUILayout.Button("Continue", GUILayout.Height(buttonHeight)))
		{
			Application.LoadLevel("TappyPlane");
		}
		
		GUILayout.Space(spacing);
		
		if(GUILayout.Button("Exit", GUILayout.Height(buttonHeight)))
		{
			
		}
		
	GUILayout.EndArea();
			

}
function Update () {
}