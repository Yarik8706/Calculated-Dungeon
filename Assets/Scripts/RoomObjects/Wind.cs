using System.Collections;
using UnityEngine;

namespace RoomObjects
{
    public class Wind : MonoBehaviour
    {
        [SerializeField] private float windForce = 15f;

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Rigidbody2D playerRb = other.gameObject.GetComponent<Rigidbody2D>();
                
                Vector2 windDirection = transform.right;
                
                playerRb.AddForce(windDirection * windForce);
            }
        }
    }
}