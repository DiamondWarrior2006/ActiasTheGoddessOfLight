using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchManager : MonoBehaviour
{
    [SerializeField] private LightMatch[] matches;
    [SerializeField] private Transform door;
    [SerializeField] private AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool areAllMatchesLit = true;
        for (int i = 0; i < matches.Length; i++)
        {
            if (!matches[i].isLit)
            {
                areAllMatchesLit = false;
                break;
            }
        }

        if (areAllMatchesLit)
        {
            door.gameObject.SetActive(false);
            sound.Play();
        }
    }
}
