using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioController : MonoBehaviour
{
    private static AudioController _instance;
    public static AudioController Instance { get { return _instance; } }

    public List<AudioClip> songs = new List<AudioClip>();
    public List<AudioClip> soundEffects = new List<AudioClip>();
    public AudioSource audiosource;
    int song = 0;
    public int lastSong = 0;
    public int sfx = 0;

    private void Awake()
    {
        if (_instance == null) {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        audiosource = GetComponent<AudioSource>();
        RandomizeSong();
    }

    private void Update()
    {
        if (!audiosource.isPlaying) {
            RandomizeSong();
        }
    }
    private void RandomizeSong()
    {
        song = Random.Range(0, songs.Count - 1);
        if (song != lastSong)
            PlayNewSong();
        else RandomizeSong();
    }

    private void PlayNewSong()
    {
        lastSong = song;
        if (SceneManager.GetActiveScene().buildIndex == 0)
            audiosource.PlayOneShot(songs[5]);
        else {
            if (song == 0 || song == 2 || song == 4)
                audiosource.PlayOneShot(songs[song], 0.4f);
            else
                audiosource.PlayOneShot(songs[song], 0.6f);
        }
    }


    public void PlaySFX(int sfx)
    {
        if (sfx != 2)
            audiosource.PlayOneShot(soundEffects[sfx]);
        else
            audiosource.PlayOneShot(soundEffects[sfx], 2);

    }

}
