using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FunMath
{
    public class AudioManager : MonoBehaviour
    {
        private AudioSource audioSource;

        void Awake()
        {
            audioSource = gameObject.GetComponent<AudioSource>();
        }

        // Method to play a sound clip
        public void PlaySound(AudioClip clip)
        {
            // Play the clip
            audioSource.PlayOneShot(clip, 0.3f);
        }
    }
}
