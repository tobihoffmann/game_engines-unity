﻿using Managers;
using UnityEngine;

namespace GameAudio
{
    public class SpiderAudio : MonoBehaviour
    {
        private void Beep()
        {
            AudioManager.Instance.Play("SpiderBeep");
        }
        
        private void Explode()
        {
            AudioManager.Instance.Play("SpiderExplosion");
        }
    }
}
