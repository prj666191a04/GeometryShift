﻿//Author Atilla puskas
//Description: Placeholder likely to be removed

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CParticleSystem : MonoBehaviour
{

    public ParticleSystem deathParticles;


    public void PlayerDeathVisuals()
    {
        deathParticles.Play();
    }
}
