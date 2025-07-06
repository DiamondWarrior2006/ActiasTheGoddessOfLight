using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TMP_Text textComponent;
    public Animator anim;
    public string[] lines;
    public bool needsInput = true;

    [SerializeField] private float waitTime;

    private int index;
    private bool inRange;
    private bool hasPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
    }

    // Update is called once per frame
    void Update()
    {
        if (inRange == true)
        {
            if (Input.GetKeyDown(KeyCode.E) && needsInput == true)
            {
                StartCoroutine(ShowLines());
            }
            else if (needsInput == false && hasPlayed == false)
            {
                StartCoroutine(ShowLines());
                hasPlayed = true;
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
    }

    IEnumerator ShowLines()
    {
        foreach (string line in lines)
        {
            textComponent.text = line;
            anim.Play("FadeIn");
            yield return new WaitForSeconds(waitTime);
            anim.Play("FadeOut");
            yield return new WaitForSeconds(waitTime / 2);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(ShowLines());
        }
        else
        {
            anim.Play("Idle");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (needsInput == true)
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().Interested();
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (needsInput == true)
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().NotInterested();
            else 
                hasPlayed = false;
            inRange = false;
        }
    }
}
