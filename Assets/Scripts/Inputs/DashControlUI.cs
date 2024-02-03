using UnityEngine;
using UnityEngine.EventSystems;

namespace Inputs
{
    public class DashControlUI : MonoBehaviour,IPointerDownHandler, IPointerUpHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {
            InputControl.Instance.shift = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            InputControl.Instance.shift = false;
        }
    }
}