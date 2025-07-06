using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Elevator : MonoBehaviour
{
    [SerializeField] private LightMatch match;
    [SerializeField] private Transform downPos;
    [SerializeField] private Transform upPos;

    public float speed;

    private bool isElevatorDown;
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        StartElevator();
    }

    void StartElevator()
    {
        if (match.isLit == true && Input.GetKeyDown(KeyCode.E))
        {
            if (transform.position.y <= downPos.position.y)
            {
                isElevatorDown = true;
            }
            else if (transform.position.y >= upPos.position.y)
            {
                isElevatorDown = false;
            }
        }

        if (isElevatorDown)
        {
            transform.position = Vector2.MoveTowards(transform.position, upPos.position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, downPos.position, speed * Time.deltaTime);
        }
    }
}
