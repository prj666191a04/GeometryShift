//Author Atilla Puskas
//desc Holds referances to AudioScorces so that the rest of the game can play them.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Do not place 
public class SystemSounds : MonoBehaviour
{
    public AudioSource acUI;
    public AudioSource acEffects;
    public AudioSource acBgMusic;

    public AudioClip deathEffect1;
    public AudioClip healEffect1;
    public AudioClip uiClick;
    public AudioClip uiError;
    public AudioClip uiAdvance;
    public AudioClip uiRollOver;
    

    public AudioClip effectsHit;


    public AudioClip menueMusic;
    public AudioClip levelMusic;
    public AudioClip levelMusic2;
    public AudioClip worldMusic;





    public void ChangeMusic(AudioClip newSong)
    {
        StopAllCoroutines();
        if (acBgMusic != null && newSong != acBgMusic.clip)
        {
            StartCoroutine(MusicFade(newSong));
        }
    }

    IEnumerator MusicFade(AudioClip clip)
    {
        while (acBgMusic.volume > 0.1f)
        {
            yield return new WaitForSeconds(0.2f);
            acBgMusic.volume -= 0.1f;
        }
        acBgMusic.volume = 0;
        acBgMusic.clip = clip;
        acBgMusic.Play();
        acBgMusic.loop = true;
        while (acBgMusic.volume < 0.9f)
        {
            yield return new WaitForSeconds(0.2f);
            acBgMusic.volume += 0.1f;
        }
        acBgMusic.volume = 1;

    }
    public static SystemSounds instance;
    private void Awake()
    {
        instance = this;
    }

    public void UIClick()
    {
        acUI.PlayOneShot(uiClick);
    }
    public void UIError()
    {
        acUI.PlayOneShot(uiError);
    }
    public void UIAdavance()
    {
        acUI.PlayOneShot(uiAdvance);
    }
    public void UIRollOver()
    {
        acUI.PlayOneShot(uiRollOver);
    }
    public void EffectsHit()
    {
        acEffects.PlayOneShot(effectsHit);
    }
    public void EffectDeath()
    {
        acEffects.PlayOneShot(deathEffect1);
    }
    public void EffectHeal()
    {
        acEffects.PlayOneShot(healEffect1);
    }
}
