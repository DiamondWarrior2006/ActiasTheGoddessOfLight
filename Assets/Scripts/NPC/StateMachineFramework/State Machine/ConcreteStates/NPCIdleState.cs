using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCIdleState : NPCState
{
    private Vector3 _targetPos;
    private Vector3 _direction;

    public NPCIdleState(NPC npc, NPCStateMachine npcStateMachine) : base(npc, npcStateMachine)
    {

    }

    public override void EnterState()
    {
        base.EnterState();

        _targetPos = GetRandomPointInArea();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        _direction = (_targetPos - npc.transform.position);

        npc.NPCMove(_direction * npc.RandomMovementSpeed);

        if ((npc.transform.position - _targetPos).sqrMagnitude < 0.01f)
        {
            _targetPos = GetRandomPointInArea();
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private Vector3 GetRandomPointInArea()
    {
        return npc.transform.position + (Vector3)Random.insideUnitCircle * npc.RandomMovementRange;
    }
}
