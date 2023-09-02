using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] private AudioSource music;

    private static MusicController musicController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (musicController == null)
        {
            musicController = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    public void PlayMusic()
    {
        if (music.isPlaying)
        {
            return;
        }
        else if (!music.isPlaying)
        {
            music.Play();
        }
    }

    public void StopMusic()
    {
        music.Stop();
    }
}
