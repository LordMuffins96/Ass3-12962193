using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musicandsounds : MonoBehaviour
{
    public AudioSource backgroundMusic;
    [SerializeField] private AudioClip[] Music;
    // Start is called before the first frame update
    void Start()
    {
        backgroundMusic.PlayOneShot(Music[0]);
        backgroundMusic.clip = Music[2];
        backgroundMusic.loop = true;
        backgroundMusic.PlayDelayed(8f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
