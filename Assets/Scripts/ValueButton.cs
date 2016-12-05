using UnityEngine;
using System.Collections;

public class ValueButton : MonoBehaviour {
    public Value val;

    public void SetValueForCurrentInstruction()
    {
        FlowBuilder.Instance.AddInstruction(FlowBuilder.Instance.CurrentInstruction, val);
        FlowBuilder.Instance.CurrentInstruction = null;
    }
}
