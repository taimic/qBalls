using UnityEngine;
using System.Collections;

public class ValueButton : MonoBehaviour {
    public Value val;
    public MenuManager menuManager;
    public void SetValueForCurrentInstruction()
    {
        FlowBuilder.Instance.AddInstruction(FlowBuilder.Instance.CurrentInstruction, val);
        FlowBuilder.Instance.CurrentInstruction = null;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Ball")
        {
            SetValueForCurrentInstruction();
            menuManager.ShowInstructionPanel();

        }
    }
}
