using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NameInput : MonoBehaviour {

    private Text botName;

	void Start () {
        botName = GetComponent<Text>();
	}
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.Return)) {
            string input = botName.text;
            input = input.Replace(" ", string.Empty);
            input = Regex.Replace(input, @"[^\u0000-\u007F]+", string.Empty);

            if (input.Length > 0)
                SessionMaster.Instance.SetName(input);
        }
	}
}