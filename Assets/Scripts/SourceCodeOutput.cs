using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SourceCodeOutput : MonoBehaviour {
    private static SourceCodeOutput instance;
    public static SourceCodeOutput Instance
    {
        get
        {
            return instance;
        }
    }
    private Text text;

	void Awake () {
        instance = this;
        text = GetComponent<Text>();
	}

    public void SetText(string code) {
        if (code == "")
            text.text = "[empty string] | Press 'p' to print code.";
        else
            text.text = code;
    }
}
