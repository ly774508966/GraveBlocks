using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

	public GameObject environment;
	public GameObject wall;
	public Camera gameCamera;
	public int gridSize;
	public int stackHeight;

	private Vector3[] gridPositions;
	private int gridCounter;

	private void Awake()
	{
		GridComposition();
		CameraComposition();
	}
	private void Update()
	{

	}

	private void Start()
	{
	}

	private void GridComposition()
	{
		gridPositions = new Vector3[gridSize * gridSize * stackHeight];
		gridCounter = 0;
		for ( int h = 0; h < stackHeight; h++ )
		{
			for ( int c = 0; c < gridSize; c++ )
			{
				for ( int r = 0; r < gridSize; r++ )
				{
					gridPositions[gridCounter].x = -(gridSize * 0.5f) + 0.5f + r;
					gridPositions[gridCounter].z = -( gridSize * 0.5f ) + 0.5f + c;
					gridPositions[gridCounter].y = h + 0.5f;
					GameObject hCube = Instantiate( environment, gridPositions[gridCounter], Quaternion.identity ) as GameObject;

					hCube.transform.localScale *= 0.5f;
					gridCounter++;
				}
			}
		}

		float wallY = 0.5f * stackHeight;
		float wallOffset = gridSize * 0.5f;

		Vector3 vPos1 = new Vector3( -wallOffset - 0.5f, wallY, 0f );
		Vector3 vPos2 = new Vector3( +wallOffset + 0.5f, wallY, 0f );
		Vector3 vPos3 = new Vector3( 0f, wallY, -wallOffset - 0.5f );
		Vector3 vPos4 = new Vector3( 0f, wallY, +wallOffset + 0.5f );
		GameObject hWallLeft		= Instantiate( wall, vPos1, Quaternion.identity ) as GameObject;
		GameObject hWallRight		= Instantiate( wall, vPos2, Quaternion.identity ) as GameObject;
		GameObject hWallBack		= Instantiate( wall, vPos3, Quaternion.identity ) as GameObject;
		GameObject hWallForward		= Instantiate( wall, vPos4, Quaternion.identity ) as GameObject;

		hWallLeft.transform.localScale = new Vector3( hWallLeft.transform.localScale.x, hWallLeft.transform.localScale.y * stackHeight, hWallLeft.transform.localScale.z * gridSize );
		hWallForward.transform.localScale = new Vector3( hWallForward.transform.localScale.x * gridSize, hWallForward.transform.localScale.y * stackHeight, hWallForward.transform.localScale.z );
		hWallRight.transform.localScale = new Vector3( hWallRight.transform.localScale.x, hWallRight.transform.localScale.y * stackHeight, hWallRight.transform.localScale.z * gridSize );
		hWallBack.transform.localScale = new Vector3( hWallBack.transform.localScale.x * gridSize, hWallBack.transform.localScale.y * stackHeight, hWallBack.transform.localScale.z);

	}

	void CameraComposition()
	{
		gameCamera.transform.position = Vector3.zero;
		gameCamera.transform.rotation = Quaternion.Euler( -90f, 0f, 0f );
		float cameraHalfAngle = gameCamera.fieldOfView * 0.5f;
		float tanAngle = Mathf.Tan( cameraHalfAngle * Mathf.Deg2Rad );
		this.transform.position = new Vector3( 0f, - ( gridSize * 0.5f ) / tanAngle , 0f );
	}
}
