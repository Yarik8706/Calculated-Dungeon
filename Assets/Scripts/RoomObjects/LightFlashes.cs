using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace RoomObjects
{
    public class LightFlashes : MonoBehaviour
    {
        [SerializeField] private float maxWaitingTime = 3;
        [SerializeField] private float minWaitingTime = 1.5f;
        [SerializeField] private float[] lightValues;

        protected Light2D _light2D;
        private float _waitTimeNow;
        private float _basePointLightOuterRadius;
        
        protected virtual void Start()
        {
            _light2D = GetComponent<Light2D>();
            _basePointLightOuterRadius = _light2D.pointLightOuterRadius;
        }

        private void Update()
        {
            _waitTimeNow -= Time.deltaTime;
            if (!(_waitTimeNow <= 0)) return;
            LightFlash();
            _waitTimeNow = Random.Range(minWaitingTime, maxWaitingTime);
        }

        private void LightFlash()
        {
            var randomIntensity = lightValues[Random.Range(0, lightValues.Length)];
            ResetAndChangeLightIntensity(_basePointLightOuterRadius + randomIntensity);
        }
        
        protected virtual void ResetAndChangeLightIntensity(float x)
        {
            _light2D.pointLightOuterRadius = x;
        }
    }
}