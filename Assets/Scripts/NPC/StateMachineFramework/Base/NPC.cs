using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, INPCMoveable
{
    public Rigidbody2D rb { get; set; }
    public bool IsFacingRight { get; set; }

    #region Idle Variables

    public float RandomMovementRange = 5f;
    public float RandomMovementSpeed = 3f;

    #endregion

    public NPCStateMachine stateMachine { get; set; }
    public NPCIdleState idleState { get; set; }

    private void Awake()
    {
        stateMachine = new NPCStateMachine();

        idleState = new NPCIdleState(this, stateMachine);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        stateMachine.Initialize(idleState);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.CurrentNPCState.FrameUpdate();
    }

    void FixedUpdate()
    {
        stateMachine.CurrentNPCState.PhysicsUpdate();
    }

    public void CheckForLeftOrRightFacing(Vector2 velocity)
    {
        if (IsFacingRight && velocity.x < 0f)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            IsFacingRight = !IsFacingRight;
        }
        else if (!IsFacingRight && velocity.x > 0f)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            IsFacingRight = !IsFacingRight;
        }
    }

    public void NPCMove(Vector2 velocity)
    {
        rb.linearVelocity = new Vector2(velocity.x, rb.linearVelocity.y);
        CheckForLeftOrRightFacing(velocity);
    }
}
