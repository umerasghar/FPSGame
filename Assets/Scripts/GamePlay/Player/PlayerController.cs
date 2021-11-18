using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public TouchEvent touch;
	private Vector3 walking;
	[Range(0, 0.3f)]
	public float walkingSpeed;
	[Range(0f, 5f)]
	public float Sensitivity;
	private float horizontalWalk, verticalWalk;
	private float look, lookUp;
	public GameObject analog;
	private Camera fpsCam;

	void Start()
	{
		fpsCam= Camera.main;
		transform.localEulerAngles = new Vector3(0, 0, 0);
	}

	// Update is called once per frame
	void Update()
	{

		horizontalWalk = Input.GetAxis("Horizontal") * walkingSpeed * Time.smoothDeltaTime;
		verticalWalk = Input.GetAxis("Vertical") * walkingSpeed * Time.smoothDeltaTime;


		walking = new Vector3(analog.transform.localPosition.y, 0, -analog.transform.localPosition.x);

		transform.Translate((walking * (walkingSpeed * Time.deltaTime)));
		//PlayerLookAround
		look += touch.touchDist.x * Sensitivity;

		transform.localEulerAngles = new Vector3(0, look, 0);
		lookUpCamera();
	}
	void lookUpCamera()
	{
		lookUp = touch.touchDist.y * Sensitivity;
		fpsCam.transform.localEulerAngles -= new Vector3(lookUp, 0, 0);
	}

}
