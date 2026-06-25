using UnityEngine;

public class MusicControl : MonoBehaviour
{
    public void TurnMusicOff()
    {
        AudioManager.instance.StopMusic();
    }

    public void TurnMusicOn()
    {
        AudioManager.instance.PlayMusic();
    }
}