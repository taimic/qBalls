using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager : Singleton<UI_Manager> {
    public GameObject uiStart, uiCode, uiEnd, uiPlay;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ToStartScreen() {
        uiStart.SetActive(true);
        uiCode.SetActive(false);
        uiEnd.SetActive(false);
        uiPlay.SetActive(false);
    }

    public void ToPlayScreen() {
        uiStart.SetActive(false);
        uiCode.SetActive(true);
        uiEnd.SetActive(false);
        uiPlay.SetActive(true);
    }

    public void ToEndScreen() {
        uiStart.SetActive(false);
        uiCode.SetActive(false);
        uiEnd.SetActive(true);
        uiPlay.SetActive(false);
    }
}
