using UnityEngine;

public class MusicControl : MonoBehaviour
{
    public AudioSource music;

    // Called when the Music On button is clicked
    public void MusicOn()
    {
        if (!music.isPlaying)
        {
            music.Play();
        }
    }

    // Called when the Music Off button is clicked
    public void MusicOff()
    {
        music.Stop();
    }
}