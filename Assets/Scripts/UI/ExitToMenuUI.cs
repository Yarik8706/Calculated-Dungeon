using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class ExitToMenuUI : MonoBehaviour
    {
        public void Exit()
        {
            SceneManager.LoadScene(1);
        }
    }
}