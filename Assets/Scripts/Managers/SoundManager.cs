using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : Singlton<SoundManager>
{
    [Space]
    [SerializeField] private AudioSource backgroundSource = null;
    [SerializeField] private AudioSource effectSource = null;
    [SerializeField] private AudioClip bgm = null;

    [Space][Header("Volume")]
    public float backgroundVolume;
    public float effectVolume;
    private float preEffectVolume = 1f;
    private float preBackgroundVolume = 1f;


    [Space][Header("Addressable Asset Reference")]
    [SerializeField] private AssetReference[] hitEffectsReferences = null;

    private List<AudioClip> hitEffects = null;

    private int hitEffectIndex = 0;


    public void Start()
    {
        PlayMusic(bgm);
    }

    private void Update()
    {
        if (backgroundVolume != preBackgroundVolume)
        {
            if(preBackgroundVolume == 0f && backgroundVolume > 0f)
            {
                PlayMusic(bgm);
            }

            backgroundSource.volume = backgroundVolume;
            preBackgroundVolume = backgroundVolume;

            if (backgroundVolume == 0f)
            {
                backgroundSource.Stop();
            }
        }

        if (effectVolume != preEffectVolume)
        {
            effectSource.volume = effectVolume;
            preEffectVolume = effectVolume;
        }
    }

    public void PlayClip(AudioClip _audioClip)
    {
        effectSource.PlayOneShot(_audioClip);
    }

    public void PlayMusic(AudioClip _audioClip)
    {
        backgroundSource.clip = _audioClip;
        backgroundSource.Play();
    }

    public void PlayHitEffect()
    {
        effectSource.PlayOneShot(hitEffects[hitEffectIndex]);
        hitEffectIndex = ++hitEffectIndex % hitEffects.Count;
    }

    public void LoadHitEffect()
    {
        hitEffects = new List<AudioClip>();

        foreach (AssetReference reference in hitEffectsReferences)
        {
            Addressables.LoadAssetAsync<AudioClip>(reference).Completed += (op) =>
            {
                if (op.Status == AsyncOperationStatus.Succeeded)
                {
                    hitEffects.Add(op.Result);
                }
            };
        }
    }
}
