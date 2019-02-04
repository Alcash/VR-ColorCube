using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerController : MonoBehaviour {

    public Text textGreenCount, textWhiteCount, textRedCount, textBlackCount;

    SceneController sceneController;

    public void Init(SceneController baseController)
    {
        sceneController = baseController;
    }

    public void UpdateUI()
    {
        textGreenCount.text = sceneController.GreenCount.ToString();
        textWhiteCount.text = sceneController.WhiteCount.ToString();
        textRedCount.text = sceneController.RedCount.ToString();
        textBlackCount.text = sceneController.BlackCount.ToString();
    }
}
