using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class NPCPatrol : MonoBehaviour
{
    public float speed;

    public bool isWalking { set; get; } = true;

    private bool hasFlipped;

    private Rigidbody2D rb;
    private Animator anim;

    private float randomTime, timer;

    private int facingDirection = 1;

    [SerializeField] private Transform groundDetection;

    [SerializeField] private float minPauseTime, maxPauseTime;
    [SerializeField] private float minWalkTime, maxWalkTime;
    [SerializeField] private float leftPatrolX, rightPatrolX;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        anim.SetBool("isWalking", isWalking ? true : false);

        randomTime = Random.Range(minWalkTime, maxWalkTime);
    }

    void Update()
    {
        if (timer >= randomTime)
        {
            StateChange();
        }

        if (!hasFlipped && (transform.position.x > rightPatrolX || transform.position.x < leftPatrolX))
        {
            StartCoroutine(Flip());
        }
    }

    void FixedUpdate()
    {
        if (isWalking)
            rb.velocity = Vector2.right * facingDirection * speed;
        timer += Time.deltaTime;
    }
    
    IEnumerator Flip()
    {
        hasFlipped = true;
        transform.Rotate(0, 180, 0);
        facingDirection *= -1;
        yield return new WaitForSeconds(0.5f);
        hasFlipped = false;
    }

    void StateChange()
    {
        isWalking = !isWalking;
        anim.SetBool("isWalking", isWalking ? true : false);
        randomTime = isWalking ? Random.Range(minWalkTime, maxWalkTime) : Random.Range(minPauseTime, maxPauseTime);
        timer = 0;
    }
}
