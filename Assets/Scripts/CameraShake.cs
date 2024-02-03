using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraShake : MonoBehaviour
{
	public static CameraShake Instance { get; private set; }

	private void Awake()
	{
		Instance = this;
	}

	public void Shake(float time, float magnitube)
	{
		StartCoroutine(ShakeCoroutine(time, magnitube));
	}

	private IEnumerator ShakeCoroutine(float time, float magnitube)
	{
		var startPosition = transform.localPosition;
		var elapsedTime = 0f;
		while (elapsedTime < time)
		{
			var xPos = Random.Range(-0.5f, 0.5f) * magnitube;
			var yPos = Random.Range(-0.5f, 0.5f) * magnitube;
			transform.localPosition = startPosition + new Vector3(xPos, yPos, 0);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		transform.localPosition = startPosition;
	}
}
