using UnityEngine;
using System.Collections;

public class InstructionButton : MonoBehaviour {
    public bool isValueButton;
    public MenuManager menuManager;
    public void SendInstruction() {
        FlowBuilder.Instance.CurrentInstruction = this.name; // DaRa: mi no anderstand LoKr: is redundant atm
        FlowBuilder.Instance.AddInstruction(this.name);
        FlowBuilder.Instance.CurrentInstruction = null;
    }

    public void SendInstructionWithValue()
    {
        FlowBuilder.Instance.CurrentInstruction = this.name;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Ball") {
            if (isValueButton)
            {
                SendInstructionWithValue();
                menuManager.ShowEnergyPanel();
                
            }
            else
            {
                SendInstruction();
            }
            
        }
    }
}
