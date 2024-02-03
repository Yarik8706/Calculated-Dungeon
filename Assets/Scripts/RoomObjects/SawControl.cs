using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public interface ILifeControl
{
    public void SetDied();
}

public class SawControl : MonoBehaviour, ILifeControl
{
    [SerializeField] private float speed = 15f;
    [SerializeField] private bool needDie;
    [SerializeField] private float lifeTime = 20f;
    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        _rigidbody.rotation = transform.rotation.eulerAngles.z;
        _rigidbody.velocity = transform.right * speed;
        
        if(needDie)SetDied();
        AudioControl.Instance.sawControls.Add(this);
    }

    private void Update()
    {
        _rigidbody.rotation -= 360.0f * Time.deltaTime;
    }

    private void OnDestroy()
    {
        AudioControl.Instance.sawControls.Remove(this);
    }

    public void SetDied()
    {
        Destroy(gameObject, lifeTime);
    }
}
