using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour {


    public static SceneController instance;

    int greenCount;
    public int GreenCount
    {
        get { return greenCount; }
        set { greenCount = value;
            UpdateUI();
        }
    }

    int whiteCount;
    public int WhiteCount
    {
        get { return whiteCount; }
        set
        {
            whiteCount = value;
            UpdateUI();
        }
    }

    int blackCount;
    public int BlackCount
    {
        get { return blackCount; }
        set
        {
            blackCount = value;
            UpdateUI();
        }
    }

    int redCount;
    public int RedCount
    {
        get { return redCount; }
        set
        {
            redCount = value;
            UpdateUI();
        }
    }
     

    public Canvas canvas;

    UIPlayerController uiPlayerController;
    // Use this for initialization
    void Start() {

        if (instance == null)
        {
            instance = this;
        }
                
        uiPlayerController = canvas.GetComponentInChildren<UIPlayerController>(true);
        uiPlayerController.Init(this);

    }
	
	// Update is called once per frame
	void UpdateUI() {
        uiPlayerController.UpdateUI();

    }
}
