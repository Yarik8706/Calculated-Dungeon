using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class SettingsUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text ruButton;
        [SerializeField] private TMP_Text enButton;
        // [SerializeField] private Image musicIcon;
        // [SerializeField] private Image effectsIcon;
        // [SerializeField] private AudioMixer audioMixer;

        private bool musicState = true;
        private bool effectsState = true;
        
        public void ChangeLanguage(string language)
        {
            Constants.isRu = language == "ru";
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void ChangeMusic()
        {
            // audioMixer.SetFloat("Music", )
        }
    }
}