using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

	public float speed;
	public float tilt;

	public Boundary boundary;
	private Rigidbody rb;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;

	//For touchpad controls - enable below line
	//public SimpleTouchPad touchPad;
	//public SimpleTouchAreaButton areaButton;

	private float nextFire;
	private GameObject newShot;
	private Quaternion calibrationQuaternion;

	private AudioSource audioShot;

	//In Unity 5 you get with get component and you do it in Start () to get better performance.
	void Start (){
		rb = GetComponent<Rigidbody> ();
		audioShot = GetComponent<AudioSource> ();
//		CalibrateAccelerometer ();
	}

	void Update () {

		//For PC Controls - Enable below block of code for pc controls
		if (Input.GetButton ("Fire1") && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			newShot = Instantiate (shot, shotSpawn.position, shotSpawn.rotation) as GameObject;
			audioShot.Play ();

		}

		//For Android Controls
		/*Touch myTouch = Input.GetTouch(0);
		Touch[] myTouches = Input.touches;
		for (int i = 0; i < Input.touchCount; i++) {
			if (Time.time > nextFire) {
				nextFire = Time.time + fireRate;
				newShot = Instantiate (shot, shotSpawn.position, shotSpawn.rotation) as GameObject;
				audioShot.Play ();
			}
		} */

		//For Android Controls - Touchpad fire
		/*if (areaButton.CanFire () && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			newShot = Instantiate (shot, shotSpawn.position, shotSpawn.rotation) as GameObject;
			audioShot.Play ();

		} */
	}

	//Used to calibrate the Input.acceleration input
	void CalibrateAccelerometer () {
		Vector3 accelerationSnapshot = Input.acceleration;
		Quaternion rotateQuaternion = Quaternion.FromToRotation (new Vector3 (0.0f, 0.0f, -1.0f), accelerationSnapshot);
		calibrationQuaternion = Quaternion.Inverse (rotateQuaternion);
	}

	//Get the 'calibrated' value from the Input
	Vector3 FixAcceleration (Vector3 acceleration) {
		Vector3 fixedAcceleration = calibrationQuaternion * acceleration;
		return fixedAcceleration;
	}

	void FixedUpdate () {
		//float moveHorizontal = Input.GetAxis ("Horizontal");
		//float moveVertical = Input.GetAxis ("Vertical");

		//Android Controls - Accelerometer Test 
		//float acmoveHorizontal = Input.acceleration.x;
		//Don't like this control as much
		//float acmoveVertical = -Input.acceleration.z;

		//For normal computer testing, enable line below
		//Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		//For Android Controls - Accelerometer Test - enable line below
		//Vector3 movementAC = new Vector3 (acmoveHorizontal, 0.0f, moveVertical);
		//Vector3 accelerationRaw = Input.acceleration;
		//Vector3 acceleration = FixAcceleration (accelerationRaw);
		Vector3 acceleration = Input.acceleration;

		//For Touch controls enable the below lines
		//Vector2 direction = touchPad.GetDirection ();
		//Vector3 movement = new Vector3 (direction.x, 0.0f, direction.y);

		Vector3 movement = new Vector3 (acceleration.x, 0.0f, 0.0f);
		Debug.Log ("x + " + Input.acceleration.x + "y + " + Input.acceleration.y + "z + " + Input.acceleration.z);

		//For Computer Testing enable line below
		rb.velocity = movement * speed;

		//For Android enable line below
		//rb.velocity = movementAC * speed;

		rb.position = new Vector3 
		(
			Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax), 
			0.0f,
			Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
		);

		rb.rotation = Quaternion.Euler (0.0f, 180.0f, rb.velocity.x * tilt);
	}
}
