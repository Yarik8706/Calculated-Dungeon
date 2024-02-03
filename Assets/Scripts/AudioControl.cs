using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class AudioControl : MonoBehaviour
    {
        [SerializeField] private AudioSource sawSource;
        [SerializeField] private float sawMusicDistance = 8;

        internal readonly List<SawControl> sawControls = new List<SawControl>();
        internal int sceneIndex;

        public static AudioControl Instance { get; private set; }
        
        private void Awake()
        {
            sceneIndex = SceneManager.GetActiveScene().buildIndex;
            transform.parent = null;
            if (Instance != null && Instance != this)
            {
                if(Instance.sceneIndex != sceneIndex)
                {
                    Destroy(Instance.gameObject);
                }
                else
                {
                    Destroy(gameObject);
                    return;
                }
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            if(sawControls.Count == 0) return;
            var distances = new List<float>();
            foreach (var sawControl in sawControls)
            {
                distances.Add(Vector2.Distance(PlayerControl.Instance.transform.position, sawControl.transform.position));
            }

            var minDistance = distances.Min();
            if(minDistance >= sawMusicDistance) return;
            sawSource.volume = 1.0f - minDistance / sawMusicDistance;
        }
    }
}