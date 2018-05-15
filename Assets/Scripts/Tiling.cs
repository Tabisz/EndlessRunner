using UnityEngine;
using System.Collections;

[RequireComponent (typeof(SpriteRenderer))]

public class Tiling : MonoBehaviour {

	public int offsetX = 2;			// the offset so that we don't get any weird errors

	// these are used for checking if we need to instantiate stuff
	public bool hasARightBuddy = false;
	public bool hasALeftBuddy = false;

	public bool reverseScale = false;	// used if the object is not tilable

	private float spriteWidth = 0f;		// the width of our element
	private Camera cam;
	private Transform myTransform;
	 private Transform newBuddy;		
				
	public float smoothing = 1f;			
	public float speed = 3f;


	void Awake () {
		cam = Camera.main;
		myTransform = transform;
	}

	// Use this for initialization
	void Start () {


		SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
		spriteWidth = sRenderer.sprite.bounds.size.x;
	}
	
	// Update is called once per frame
	void Update () {

			// the parallax is the opposite of the camera movement because the previous frame multiplied by the scale
		float parallax = speed; 
			
			// set a target x position which is the current position plus the parallax
			float backgroundTargetPosX = this.transform.position.x + parallax;
			
			// create a target position which is the background's current position with it's target x position
			Vector3 backgroundTargetPos = new Vector3 (backgroundTargetPosX, this.transform.position.y,this.transform.position.z);
			
			
			// fade between current position and the target position using lerp
		this.transform.position = Vector3.Lerp (this.transform.position, backgroundTargetPos, smoothing * Time.deltaTime);


		if (hasALeftBuddy == false || hasARightBuddy == false) {

			float camHorizontalExtend = cam.orthographicSize * Screen.width/Screen.height;


			float edgeVisiblePositionRight = (myTransform.position.x + spriteWidth/2) - camHorizontalExtend;
			float edgeVisiblePositionLeft = (myTransform.position.x - spriteWidth/2) + camHorizontalExtend;


			if (cam.transform.position.x >= edgeVisiblePositionRight - offsetX && hasARightBuddy == false)
			{

				MakeNewBuddy (1);
				hasARightBuddy = true;
			}
			else if (cam.transform.position.x <= edgeVisiblePositionLeft + offsetX && hasALeftBuddy == false)
			{
			
				MakeNewBuddy (-1);
				hasALeftBuddy = true;
			}

		}


	}


	void MakeNewBuddy (int rightOrLeft) {
	
		Vector3 newPosition = new Vector3 (myTransform.position.x + spriteWidth * rightOrLeft, myTransform.position.y, myTransform.position.z);

		newBuddy = Instantiate (myTransform, newPosition, myTransform.rotation) as Transform;
	


		if (reverseScale == true) {
			newBuddy.localScale = new Vector3 (newBuddy.localScale.x*-1, newBuddy.localScale.y, newBuddy.localScale.z);
		}

		newBuddy.parent = myTransform.parent;
		if (rightOrLeft > 0) {
			newBuddy.GetComponent<Tiling>().hasALeftBuddy = true;
		}
		else {
			newBuddy.GetComponent<Tiling>().hasARightBuddy = true;
		}
	}
}
