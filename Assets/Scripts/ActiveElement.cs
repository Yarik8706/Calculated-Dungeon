using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class ActiveElement : MonoBehaviour
    {
        [SerializeField] private Transform hidePosition;
        [SerializeField] private bool hideInStart;

        private Transform _activePosition;
        
        private void Awake()
        {
            _activePosition = Instantiate(hidePosition, transform.parent);
            _activePosition.position = transform.position;
            if (hideInStart) transform.position = hidePosition.position;
        }

        public Tween ChangeActive(bool isActive)
        {
            return transform.DOMove(isActive ? _activePosition.position : hidePosition.position, 0.7f).SetLink(gameObject)
                .SetEase(Ease.InOutExpo).Play();
        }
    }
}