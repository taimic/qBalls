using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Manager : Singleton<UI_Manager> {
    public GameObject uiStart, uiCode, uiEnd, uiPlay;


    private void OnLevelWasLoaded(int level) {
        //Init();
        //ToStartScreen();
    }
    // Use this for initialization
    void Start () {
        //Init();
        //ToStartScreen();
	}

    private void Update() {
        if (uiEnd == null)
            return;

        if(uiEnd.activeSelf == true) {
            if (Input.anyKeyDown) {
                LoadStartScreen();
            }
        }
    }

    private void Init() {
        uiStart = GameObject.FindGameObjectWithTag("uiStart");
        uiCode = GameObject.FindGameObjectWithTag("uiCode");
        uiEnd = GameObject.FindGameObjectWithTag("uiEnd");
        uiPlay = GameObject.FindGameObjectWithTag("uiPlay");
    }

    private void LoadStartScreen() {
        SceneManager.LoadScene(0);
    }

    IEnumerator DelayedStart() {
        yield return new WaitForSeconds(5);
        LoadStartScreen();
    }

    public void ToStartScreen() {
        Init();
        Debug.LogError("to start");
        uiStart.SetActive(true);
        //uiCode.SetActive(false);
        uiEnd.SetActive(false);
        uiPlay.SetActive(false);
    }

    public void ToPlayScreen() {
        Debug.LogError("to play");
        uiStart.SetActive(false);
        uiCode.SetActive(true);
        uiEnd.SetActive(false);
        uiPlay.SetActive(true);
    }

    public void ToEndScreen() {
        Debug.LogError("to end");
        uiStart.SetActive(false);
        //uiCode.SetActive(false);
        uiEnd.SetActive(true);
        uiPlay.SetActive(false);
    }
}
