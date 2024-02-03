using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    public float speed = 15f;
    private Rigidbody2D _rigidbody2D;
	public GameObject postBullet;

	private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.velocity = transform.right * speed;
        Destroy(gameObject, 3f);
	}

	private void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.gameObject.layer != LayerMask.NameToLayer("Ground")) return;
		if (coll.CompareTag("Number"))
			coll.GetComponent<Number>().DestrMe();
		
		Died(0f);
	}

	private void Died(float timer)
	{
		GameObject asd = Instantiate(postBullet, transform.position, Quaternion.identity);
		Destroy(asd, 2f);
		Destroy(gameObject, timer);
	}
}
