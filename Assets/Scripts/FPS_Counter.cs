using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPS_Counter : MonoBehaviour
{
    public Text fpsCounter;
    int fps = 0;
    float elapsed = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fps++;
        elapsed += Time.deltaTime;
        if(elapsed >= 1)
        {
            elapsed -= 1;
            UpdateFPS();
        }
    }

    /// <summary>
    /// Updates FPS display.
    /// </summary>
    void UpdateFPS()
    {
        fpsCounter.text = "FPS: " + fps;
        fps = 0;
    }
}
