using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Inputs
{
    public class JumpButton : MonoBehaviour,IPointerDownHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {
            InputControl.Instance.jump = true;
            StartCoroutine(ChangeJumpActive());
        }

        private IEnumerator ChangeJumpActive()
        {
            yield return null;
            InputControl.Instance.jump = false;
        }
    }
}