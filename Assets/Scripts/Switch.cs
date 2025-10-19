using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    private bool inArea;
    private bool switchEnabled;

    private PlayerController player;

    enum leverPurpose
    {
        openDoor,
        testOne
    }

    [SerializeField] leverPurpose lever;

    [Header("Open Door")]

    [SerializeField] private GameObject door;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inArea)
        {
            if (lever == leverPurpose.openDoor)
            {
                if (Input.GetKeyDown(KeyCode.E) && switchEnabled == false)
                {
                    door.SetActive(false);
                    GetComponent<SpriteRenderer>().flipX = true;
                    switchEnabled = true;
                }
            }
            else if (lever == leverPurpose.testOne)
            {
                print("This is Test One!!!");
            }

            if (switchEnabled == true)
            {
                GetComponent<Collider2D>().enabled = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inArea = true;
            player.Interested();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inArea = false;
            player.NotInterested();
        }
    }
}
