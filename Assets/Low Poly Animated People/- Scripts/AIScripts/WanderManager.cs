﻿using UnityEngine;

namespace PolyPerfect
{
    public class WanderManager : MonoBehaviour
    {
        [SerializeField]
        private bool peaceTime;
        public bool PeaceTime
        {
            get
            {
                return peaceTime;
            }
            set
            {
                SwitchPeaceTime(value);
            }
        }

        private static WanderManager instance;
        public static WanderManager Instance
        {
            get
            {
                return instance;
            }
        }

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                Debug.LogError("Two Wander Managers were found in the scene, Make sure there is only ever one!");
                return;
            }

            instance = this;
        }

        private void Start()
        {
            StopAllCoroutines();

            if (peaceTime)
            {
                Debug.Log("AnimalManager: Peacetime is enabled, all animals are non-agressive.");
                SwitchPeaceTime(true);
            }
        }

        public void SwitchPeaceTime(bool enabled)
        {
            if (enabled == peaceTime)
            {
                return;
            }

            peaceTime = enabled;

            Debug.Log(string.Format("AnimalManager: Peace time is now {0}.", enabled ? "On" : "Off"));
            
            foreach (WanderScript animal in WanderScript.AllAnimals)
            {
                animal.SetPeaceTime(enabled);
            }
        }

        public void Nuke()
        {
            foreach (WanderScript animal in WanderScript.AllAnimals)
            {
                animal.Die();
            }
        }
    }
}