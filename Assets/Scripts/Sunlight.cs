using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sunlight : MonoBehaviour
{
    private HealthManager healthManager;

    private bool inSunlight;

    // Start is called before the first frame update
    void Start()
    {
        healthManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<HealthManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (inSunlight == true)
        {
            healthManager.Heal(Time.deltaTime * 10);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inSunlight = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inSunlight = false;
        }
    }
}
