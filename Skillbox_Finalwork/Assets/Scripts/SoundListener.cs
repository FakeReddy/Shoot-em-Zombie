using UnityEngine;

[System.Serializable]
internal class PistolClips
{
    [SerializeField] internal AudioClip _shoot;
    [SerializeField] internal AudioClip _reload;
}
[System.Serializable]
internal class AudioSources
{
    [SerializeField] internal AudioSource _shoot;
    [SerializeField] internal AudioSource _reload;
    [SerializeField] internal AudioSource _damage;
    [SerializeField] internal AudioSource _heal;
    [SerializeField] internal AudioSource _music;
}
[System.Serializable]
internal class MusicClips
{
    [SerializeField] internal AudioClip _music;
}
public class SoundListener : GameData
{
    [SerializeField] private PistolClips _pistolClips;
    [SerializeField] private MusicClips _musicClips;
    [SerializeField] private AudioSources _audioSources;

    private void Awake()
    {
        SetAudioVolume(_audioSources._shoot, LoadFloatData(GlobalStringsVars.SoundValueData));
        SetAudioVolume(_audioSources._reload, LoadFloatData(GlobalStringsVars.SoundValueData));
        SetAudioVolume(_audioSources._damage, LoadFloatData(GlobalStringsVars.SoundValueData));
        SetAudioVolume(_audioSources._heal, LoadFloatData(GlobalStringsVars.SoundValueData));
        SetAudioVolume(_audioSources._music, LoadFloatData(GlobalStringsVars.MusicValueData));
        PlayAudio(_musicClips._music, _audioSources._music);
    }
    public void PlaySoundShootWithPistol()
    {
        PlayAudio(_pistolClips._shoot, _audioSources._shoot);
    }
    public void PlaySoundReloadPistol()
    {
        PlayAudio(_pistolClips._reload, _audioSources._reload);
    }
    private void PlayAudio(AudioClip clip, AudioSource source)
    {
        source.clip = clip;
        source.Play();
    }
    private void SetAudioVolume(AudioSource source, float volume)
    {
        source.volume = volume;
    }
}