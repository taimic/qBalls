using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

    public GameObject energyPanel, instructionPanel;

    public void ShowEnergyPanel() {
        instructionPanel.SetActive(false);
        energyPanel.SetActive(true);
    }

    public void ShowInstructionPanel() {
        instructionPanel.SetActive(true);
        energyPanel.SetActive(false);
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
