using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "AudioData", menuName = "ScriptableObjects/AudioData", order = 1)]
public class AudioDataSO : DataSO
{
    [Header("Music")]
    public AudioClip[] musicArray;
    [Header("Ambiance")]
    public AudioClip[] ambianceArray;
    public AudioClip[] ambianceArray2;
    [Header("SFX")]
    public AudioClip[] footstepsArray;
    public AudioClip[] iceCrackingArray;
    public AudioClip[] iceBreakSFX;
}
