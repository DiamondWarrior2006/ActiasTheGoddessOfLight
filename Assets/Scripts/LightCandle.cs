using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCandle : MonoBehaviour
{
    [SerializeField] private SpriteRenderer flame;

    private bool canInteractWithCandle;
    public bool isCandleLit = false;

    private PlayerController player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        flame.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (canInteractWithCandle)
        {
            if (Input.GetKeyDown(KeyCode.E) && flame.gameObject.activeInHierarchy == false)
            {
                flame.gameObject.SetActive(true);
                if (isCandleLit == false)
                {
                    isCandleLit = true;
                }
            }
        }
        if (isCandleLit == true)
        {
            GetComponent<Collider2D>().enabled = false;
        }
    }
    public void UnLightCandle()
    {
        flame.gameObject.SetActive(false);
        isCandleLit = false;
        GetComponent<Collider2D>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canInteractWithCandle = true;
            player.Interested();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canInteractWithCandle = false;
            player.NotInterested();
        }
    }
}
