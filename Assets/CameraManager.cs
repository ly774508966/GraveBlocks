using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

	public float startFOV;
	public float finalFOV;
	public float speedFOV;

	private float m_ftimer;
	private float m_fruntimeFOV;

	private void Awake()
	{
		Camera.main.fieldOfView = startFOV;
		m_fruntimeFOV = startFOV;
	}
	private void Update()
	{
		if ( m_fruntimeFOV >= startFOV && m_fruntimeFOV <= finalFOV)
		{
			m_fruntimeFOV = m_fruntimeFOV + Mathf.Sin( m_ftimer * speedFOV );
			Camera.main.fieldOfView = m_fruntimeFOV;
			m_ftimer = +Time.deltaTime;
		}
	}
}
