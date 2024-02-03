using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Inputs
{
    public class ShootControlUI : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
    {
        [SerializeField] private Transform joystickCenter;
        [SerializeField] private Transform joystickCircle;

        private void Update()
        {
            var shootVector = -(joystickCircle.position - joystickCenter.position).normalized;
            if(shootVector == Vector3.zero) return;
            InputControl.Instance.shootVector = shootVector;
        }

        public IEnumerator ChangeShootActive()
        {
            yield return null;
            InputControl.Instance.shoot = false;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            InputControl.Instance.shoot = true;
            StartCoroutine(ChangeShootActive());
            InputControl.Instance.sunVectorLight = false;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            InputControl.Instance.sunVectorLight = true;
        }
    }
}