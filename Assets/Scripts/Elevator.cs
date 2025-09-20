using System.Collections;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private LightCandle elevatorCandle; // candle tied to this elevator
    [SerializeField] private Transform downPos;
    [SerializeField] private Transform upPos;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float returnDelay = 3f; // time after player leaves before going down

    private bool isMoving = false;
    private bool isUp = false;
    private PlayerController playerController;

    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        transform.position = downPos.position; // start at bottom
    }

    void Update()
    {
        if (!isMoving && !isUp && playerController.onPlatform && elevatorCandle.isCandleLit)
        {
            // go up when candle lit & player is on platform
            StartCoroutine(MoveElevator(upPos.position, true));
        }
    }

    IEnumerator MoveElevator(Vector3 targetPos, bool goingUp)
    {
        isMoving = true;

        while (Vector2.Distance(transform.position, targetPos) > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPos;
        isUp = goingUp;
        isMoving = false;

        // if we just went up, wait for player to leave → go down after delay
        if (goingUp)
        {
            yield return new WaitUntil(() => !playerController.onPlatform);
            yield return new WaitForSeconds(returnDelay);

            StartCoroutine(MoveElevator(downPos.position, false));
        }
        else
        {
            // fully down, deactivate elevator
            elevatorCandle.UnLightCandle();
        }
    }
}
