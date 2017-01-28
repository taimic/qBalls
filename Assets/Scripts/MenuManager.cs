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
        StartCoroutine(DoDelayEnergy());
        
    }

    public IEnumerator DoDelayEnergy()
    {
        yield return new WaitForSeconds(0.2f);
        energyPanel.SetActive(true);
    }

    public void ShowInstructionPanel() {
        energyPanel.SetActive(false);
        
        StartCoroutine(DoDelayInstruction());
    }

    public IEnumerator DoDelayInstruction()
    {
        yield return new WaitForSeconds(0.2f);
        instructionPanel.SetActive(true);
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
