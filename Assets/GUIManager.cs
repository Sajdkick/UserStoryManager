using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour {

    public static GUIManager Instance()
    {
        return GameObject.Find("GUI Manager").GetComponent<GUIManager>();
    }

    public List<GameObject> canvasList;

    public void LoadCanvas(string canvasName)
    {
        foreach(GameObject canvas in canvasList)
        {
            canvas.SetActive(canvas.name == canvasName);
        }
    }

}
