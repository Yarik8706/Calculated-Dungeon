using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RoomObjects
{
    public class Exit : MonoBehaviour
    {
        private bool _isOpen;
        private Animator _animator;
        
        public static Exit Instance { get; private set; }

        private void Start()
        {
            _animator = GetComponent<Animator>();
            Instance = this;
        }

        public void OpenDoor()
        {
            _isOpen = true;
            _animator.SetTrigger("Open");
        }

        public void CameraShakeAnimationEvent()
        {
            CameraShake.Instance.Shake(0.4f, 0.5f);
        }

        private void OnTriggerStay2D(Collider2D col)
        {
            if(!col.attachedRigidbody.CompareTag("Player") 
               || !_isOpen || ExpressionsControl.Instance.UnsolvedExpressionCount != 0) return;
            PlayerControl.Instance.CanIDeath = false;
            PlayerControl.Instance.Death = true;
            PlayerWin();
        }

        [ContextMenu(nameof(PlayerWin))]
        public void PlayerWin()
        {
            EndLevelControl.Instance.EndLvl(true, 
                            GameTexts.WinTexts[Random.Range(0, GameTexts.WinTexts.Length)].GetText());
        }
    }
}