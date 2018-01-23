using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmSeManager : MonoBehaviour
{
    [SerializeField]
    List<AudioClip> seList;
    [SerializeField]
    List<AudioClip> voiceList;
    [SerializeField]
    List<AudioClip> bgmList;
    [SerializeField]
    AudioSource seAudio;
    [SerializeField]
    AudioSource bgmAudio;
    [SerializeField]
    AudioSource voiceAudio;

    public void SePlay(int number)
    {
        seAudio.clip = seList[number];
        seAudio.Play();
    }

    public void BgmPlay(int number)
    {
        bgmAudio.clip = bgmList[number];
        bgmAudio.Play();
    }

    public void VoicePlay(int number)
    {
        voiceAudio.clip = voiceList[number];
        voiceAudio.Play();
    }
    public void BgmStop()
    {
        bgmAudio.Stop();
    }
}
