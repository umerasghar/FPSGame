using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public TouchEvent touch;
	public Rigidbody playerRB;
	private Vector3 walking;
	[Range(0, 0.3f)]
	public float walkingSpeed;
	[Range(0f, 5f)]
	public float Sensitivity;
	[Range(1, 300)]
	public int jumpForce;
	//private float horizontalWalk, verticalWalk;
	private float look, lookUp;
	public GameObject analog;
	private Camera fpsCam;
	[HideInInspector]
	public bool isGrounded=true;
	[HideInInspector]
	public bool canJump=true;
	void Start()
	{
		fpsCam= Camera.main;
		transform.localEulerAngles = new Vector3(0, 0, 0);
	}

	// Update is called once per frame
	void Update()
	{

	//	horizontalWalk = Input.GetAxis("Horizontal") * walkingSpeed * Time.smoothDeltaTime;
	//	verticalWalk = Input.GetAxis("Vertical") * walkingSpeed * Time.smoothDeltaTime;


		walking = new Vector3(analog.transform.localPosition.y, 0, -analog.transform.localPosition.x);

		transform.Translate((walking * (walkingSpeed * Time.deltaTime)));
		//PlayerLookAround
		look += touch.touchDist.x * Sensitivity;

		transform.localEulerAngles = new Vector3(0, look, 0);
		lookUpCamera();
	}
    private void FixedUpdate()
    {
		RaycastHit hit;
		if (Physics.Raycast(gameObject.transform.position, Vector3.down, out hit, Mathf.Infinity))
		{
			Debug.DrawRay(gameObject.transform.position, Vector3.down * hit.distance, Color.red);
			var floorDistance= Vector3.Distance(hit.collider.transform.position, gameObject.transform.position);
			Debug.Log(floorDistance);
            if (floorDistance < 1.5f)
            {
				isGrounded = true;
            }
		}
			if (isGrounded)
		{
			if (canJump)
			{
				playerRB.AddForce(Vector3.up * jumpForce);
				isGrounded = false;
				canJump = false;
			}
		}
	}
    void lookUpCamera()
	{
		lookUp = touch.touchDist.y * Sensitivity;
		fpsCam.transform.localEulerAngles -= new Vector3(lookUp, 0, 0);
	}
	public void PlayerJump()
    {
		canJump = !canJump;
    }

}
