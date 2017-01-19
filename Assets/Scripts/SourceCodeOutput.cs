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
    private string initText;

	void Awake () {
        instance = this;
        text = GetComponent<Text>();
	}

    void Start() {
        initText = text.text;
    }

    public void Reset() {
        text.text = initText;
    }

    public void SetText(string code) {
        if (code == "")
            text.text = "[empty string] | Press 'p' to print code.";
        else
            text.text = code;
    }
}
