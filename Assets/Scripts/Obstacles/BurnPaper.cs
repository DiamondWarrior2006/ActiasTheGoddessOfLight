using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BurnPaper : MonoBehaviour
{
    public Animator anim;

    [SerializeField] private AnimationClip clip;

    private bool inTrigger = false;

    private PlayerController player;

    private bool burned;

    // Start is called before the first frame update
    void Start()
    {
        anim.speed = 0;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();    
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && inTrigger == true)
        {
            anim.speed = 1;
            burned = true;
        }

        if (burned == true)
        {
            GetComponent<Collider2D>().enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inTrigger = true;
            player.Interested();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inTrigger = false;
            player.NotInterested();
        }
    }
}
