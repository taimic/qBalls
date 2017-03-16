using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SessionMaster : Singleton<SessionMaster> {
    public string botName;

    private void Start() {
        Initialize();
    }

    private void OnLevelWasLoaded(int level) {
        Initialize();
    }

    private void Initialize() {
        botName = string.Empty;
        UI_Manager.Instance.ToStartScreen();
    }

    public void SetName(string input) {
        input = input.Replace(" ", string.Empty);
        input = Regex.Replace(input, @"[^\u0000-\u007F]+", string.Empty);

        botName = input;
        SceneManager.LoadScene(1);
    }
}