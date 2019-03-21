using UnityEngine;
using System.Collections;

public class Energy_Bar : MonoBehaviour
{
    public float barDisplay; //current progress
    public Vector2 pos = new Vector2(20, 40);
    public Vector2 size = new Vector2(60, 20);
    public Texture emptyTex;
    public Texture fullTex;

    private void Start()
    {
        size.x = Screen.width / 4;
    }
    void OnGUI()
    {
        if (pos.x < 0)
        {
            pos.x = Screen.width + pos.x - size.x;
        }
        //draw the background:
        //GUI.BeginGroup(new Rect(pos.x, pos.y, size.x, size.y));
        GUI.DrawTexture(new Rect(pos.x, pos.y, size.x, size.y), emptyTex, ScaleMode.ScaleAndCrop);

        //draw the filled-in part:
        //GUI.BeginGroup(new Rect(0, 0, size.x * barDisplay, size.y));
        GUI.DrawTexture(new Rect(pos.x, pos.y, size.x * barDisplay, size.y), fullTex, ScaleMode.ScaleAndCrop);
        //GUI.EndGroup();
        //GUI.EndGroup();
    }

    void Update()
    {
        //for this example, the bar display is linked to the current time,
        //however you would set this value based on your desired display
        //eg, the loading progress, the player's health, or whatever.
        //barDisplay = Time.time * 0.05f;
        //        barDisplay = MyControlScript.staticHealth;
    }
}