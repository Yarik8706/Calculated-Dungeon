using System.Collections;
using UnityEngine;

namespace RoomObjects
{
    [RequireComponent(typeof(ParticleSystem))]
    public class AutoDestroyEffect : MonoBehaviour
    {
        private void OnEnable()
        {
            StartCoroutine(CheckIfAlive());
        }

        private IEnumerator CheckIfAlive()
        {
            var ps = GetComponent<ParticleSystem>();
            while(ps != null)
            {
                yield return new WaitForSeconds(0.5f);
                if (ps.IsAlive(true)) continue;
                Destroy(gameObject);
                break;
            }
        }
    }
}