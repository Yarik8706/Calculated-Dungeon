using System;
using System.Collections;
using Inputs;
using UnityEngine;

public class AttackControl : MonoBehaviour
{
	[SerializeField] private Transform bulletSpawnContainer;
	[SerializeField] private Transform bulletSpawnTransform;
	[SerializeField] private GameObject bullet;
	[SerializeField] private GameObject sunVectorLight;

	private float _activeAttackCooldown;
	private const float AttackCooldown = 0.4f;

	private void Start()
	{
		_activeAttackCooldown = AttackCooldown;
		sunVectorLight.SetActive(false);
	}

	private void Update()
	{
		if (PlayerControl.Instance.Death) return;
		bulletSpawnContainer.right = InputControl.Instance.shootVector;

		_activeAttackCooldown -= Time.deltaTime;

		if (InputControl.Instance.shoot && _activeAttackCooldown <= 0)
		{
			_activeAttackCooldown = AttackCooldown;
			SpawnBullet();
		}

		if (InputControl.Instance.sunVectorLight != sunVectorLight.activeSelf)
		{
			sunVectorLight.SetActive(InputControl.Instance.sunVectorLight);
		}
	}

	private void SpawnBullet()
	{
		Instantiate(bullet, bulletSpawnTransform.position, bulletSpawnContainer.rotation);
	}
}
