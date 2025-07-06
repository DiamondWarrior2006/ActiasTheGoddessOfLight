using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    [Header("Footsteps")]
    public List<AudioClip> concreteSFX;
    public List<AudioClip> woodSFX;

    [SerializeField] private AudioSource footstepSource;

    enum groundTags
    {
        Concrete,
        Wood,
        Empty
    }

    [SerializeField] private groundTags groundTag;

    void Start()
    {

    }

    void Update()
    {
        
    }
    
    public void PlayFootsteps()
    {
        AudioClip clip = null;

        if (groundTag == groundTags.Concrete)
        {
            clip = concreteSFX[Random.Range(0, concreteSFX.Count)];
        }
        else if (groundTag == groundTags.Wood)
        {
            clip = woodSFX[Random.Range(0, woodSFX.Count)];
        }

        if (groundTag != groundTags.Empty && footstepSource.isPlaying == false)
        {
            footstepSource.clip = clip;
            footstepSource.volume = Random.Range(0.02f, 0.05f);
            footstepSource.pitch = Random.Range(0.8f, 1.2f);
            footstepSource.Play();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Concrete"))
        {
            groundTag = groundTags.Concrete;
        }
        else if (collision.gameObject.CompareTag("Wood"))
        {
            groundTag = groundTags.Wood;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        groundTag = groundTags.Empty;
    }
}
