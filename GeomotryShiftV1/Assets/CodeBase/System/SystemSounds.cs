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
