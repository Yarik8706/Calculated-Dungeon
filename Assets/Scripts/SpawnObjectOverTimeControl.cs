using UnityEngine;

public class SpawnObjectOverTimeControl : MonoBehaviour
{
    [SerializeField] private float startTimer = 10f;
    [SerializeField] private float timer;
    [SerializeField] private GameObject objectPrefab;

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            timer = startTimer;
            var newObject = Instantiate(objectPrefab, transform.position, transform.rotation);
            if (newObject.TryGetComponent(out ILifeControl lifeControl))
            {
                lifeControl.SetDied();
            }
        }
    }
}