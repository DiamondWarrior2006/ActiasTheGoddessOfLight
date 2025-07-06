using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INPCMoveable
{
    Rigidbody2D rb { get; set; }
    bool IsFacingRight { get; set; }
    void NPCMove(Vector2 velocity);
    void CheckForLeftOrRightFacing(Vector2 velocity);
}
