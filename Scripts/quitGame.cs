using UnityEngine;
using System.Collections;
using UnityEditor;

public class quitGame : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))  // Quits the player when 'q' is pressed 
        {
            #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        }
    }
}