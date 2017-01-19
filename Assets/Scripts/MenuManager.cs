using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuManager : MonoBehaviour {
    public GameObject energyPanel, instructionPanel;
    public List<GameObject> ifInstructions;

    void Start() {
        FlowBuilder.EventAddedIfElseNode += HideIfInstructions;
        FlowBuilder.EventAddedElseNode += ShowIfInstructions;
    }

    void OnDestroy() {
        FlowBuilder.EventAddedIfElseNode -= HideIfInstructions;
        FlowBuilder.EventAddedElseNode -= ShowIfInstructions;
    }

    public void ShowEnergyPanel() {
        instructionPanel.SetActive(false);
        energyPanel.SetActive(true);
    }

    public void ShowInstructionPanel() {
        instructionPanel.SetActive(true);
        energyPanel.SetActive(false);
    }

    private void HideIfInstructions() {
        foreach (GameObject item in ifInstructions)
            item.SetActive(false);
    }

    private void ShowIfInstructions() {
        foreach (GameObject item in ifInstructions)
            item.SetActive(true);
    }
}
