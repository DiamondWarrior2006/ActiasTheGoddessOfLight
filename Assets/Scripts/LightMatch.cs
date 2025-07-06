using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightMatch : MonoBehaviour
{
    [SerializeField] private SpriteRenderer flame;

    private bool canInteract;
    public bool isLit = false;

    private PlayerController player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        flame.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (canInteract)
        {
            if (Input.GetKeyDown(KeyCode.E) && flame.gameObject.activeInHierarchy == false)
            {
                flame.gameObject.SetActive(true);
                if (isLit == false)
                {
                    isLit = true;
                }
            }
        }
        if (isLit == true)
        {
            GetComponent<Collider2D>().enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canInteract = true;
            player.Interested();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canInteract = false;
            player.NotInterested();
        }
    }
}
