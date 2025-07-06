using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCStateMachine
{
    public NPCState CurrentNPCState {  get; set; }

    public void Initialize(NPCState currentNPCState)
    {
        CurrentNPCState = currentNPCState;
        CurrentNPCState.EnterState();
    }

    public void ChangeState(NPCState newState)
    {
        CurrentNPCState.ExitState();
        CurrentNPCState = newState;
        CurrentNPCState.EnterState();
    }
}
