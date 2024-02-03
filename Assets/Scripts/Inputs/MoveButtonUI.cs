using UnityEngine;
using UnityEngine.EventSystems;

namespace Inputs
{
    public class MoveButtonUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private float moveX;
        
        public void OnPointerDown(PointerEventData eventData)
        {
            InputControl.Instance.moveX = moveX;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if(moveX == InputControl.Instance.moveX) InputControl.Instance.moveX = 0;
        }
    }
}