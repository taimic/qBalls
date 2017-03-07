using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowLastInstruction : MonoBehaviour {

    public GameObject energy, sleep, move;

    private void OnEnable()
    {
        HideInstructions();
        if (FlowBuilder.Instance.CurrentInstruction == energy.name)
        {
            energy.SetActive(true);
        }
        else if(FlowBuilder.Instance.CurrentInstruction == sleep.name)
        {
            sleep.SetActive(true);
        }
        else if (FlowBuilder.Instance.CurrentInstruction == move.name)
        {
            move.SetActive(true);
        }
    }

    private void OnDisable()
    {
        HideInstructions();
    }

    private void HideInstructions()
    {
        energy.SetActive(false);
        sleep.SetActive(false);
        move.SetActive(false);
    }
}
