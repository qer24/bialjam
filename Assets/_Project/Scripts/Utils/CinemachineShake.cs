	using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
	using Cinemachine;

	/// <summary>
/// Add this component to your Cinemachine Virtual Camera to have it shake when calling its ShakeCamera methods.
/// </summary>
[RequireComponent(typeof(CinemachineCameraOffset))]
public class CinemachineShake : MonoBehaviour
{
	private static CinemachineShake _instance;

	public static CinemachineShake Instance
	{
		get
		{
			if (_instance == null)
			{
				var instances = FindObjectsOfType<CinemachineShake>();
				if (instances.Length > 1) Debug.LogError("Multiple Instances of CameraShake in scene");
				else if (instances.Length == 0) Debug.LogError("No instances of CameraShake in scene");
				else _instance = instances[0];
			}

			return _instance;
		}
	}

	/// The default amplitude that will be applied to your shakes if you don't specify one
	public float DefaultShakeAmplitude = 1f;
	/// The default frequency that will be applied to your shakes if you don't specify one
	public float DefaultShakeFrequency = 10f;

	
	private CinemachineVirtualCamera vcam;
	private CinemachineCameraOffset offset;
	
	private float _trauma;

	private float _seed;
	private float _elapsedTime;
	
	private Vector2 startPos;
	private float startDutch;

	// Use this for initialization
	private void Start()
	{
		_seed = Random.Range(0, 999999);
		vcam = GetComponent<CinemachineVirtualCamera>();
		offset = vcam.GetComponent<CinemachineCameraOffset>();
		startPos = new Vector2(offset.m_Offset.x, offset.m_Offset.y);
		startDutch = 0;
	}

	// Update is called once per frame
	private void Update()
	{
		_trauma -= Time.deltaTime;
		_trauma = Mathf.Clamp(_trauma, 0, 1);

		var xPos = GetPerlin(_seed, DefaultShakeFrequency, DefaultShakeAmplitude);
		var yPos = GetPerlin(_seed + 1, DefaultShakeFrequency, DefaultShakeAmplitude);
		var rot = GetPerlin(_seed + 2, DefaultShakeFrequency, DefaultShakeAmplitude * 10f);
		// transform.position = new Vector3(startPos.x + xPos, startPos.y + yPos, startPos.z);
		// transform.eulerAngles = new Vector3(0, 0, rot);
		offset.m_Offset.x = startPos.x + xPos;
		offset.m_Offset.y = startPos.y + yPos;
		vcam.m_Lens.Dutch = startDutch + rot;
		
		_elapsedTime += Time.deltaTime;
	}

	private float GetPerlin(float newSeed, float frequency, float strength)
	{
		var noise = Mathf.PerlinNoise(newSeed + _elapsedTime * frequency, newSeed + _elapsedTime * frequency);
		noise = noise * 2 - 1;
		return noise * _trauma * _trauma * strength;
	}
	
	public void AddTrauma(float trauma)
	{
		_trauma += trauma;
	}
	
	public void SetTrauma(float trauma)
	{
		_trauma = trauma;
	}
}