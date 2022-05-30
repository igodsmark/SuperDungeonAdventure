using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sparkle : MonoBehaviour
{
    [SerializeField] bool treasure = true;
    ParticleSystem particles;
    // Start is called before the first frame update
    void Awake()
    {
        particles = GetComponent<ParticleSystem>();
        if (treasure)
        {
            particles.Play();
        }
        else
        {
            particles.Stop();

        }
        FindObjectOfType<EventManager>().SwitchGameMode += Sparkle_SwitchGameMode;
    }

    private void Sparkle_SwitchGameMode(object sender, System.EventArgs e)
    {
        if (treasure)
        {
            particles.Stop();
        }
        else
        {
            particles.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
