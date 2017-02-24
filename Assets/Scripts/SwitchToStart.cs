using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchToStart : MonoBehaviour {

	void Start () {
        StartCoroutine(DelayedStart());	
	}

    private void Update() {
        if (Input.anyKeyDown) {
            LoadStartScreen();
        }
    }

    private void LoadStartScreen() {
        SceneManager.LoadScene(0);
    }

    IEnumerator DelayedStart() {
        yield return new WaitForSeconds(5);
        LoadStartScreen();
    }	
}
