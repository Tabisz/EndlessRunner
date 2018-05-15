using UnityEngine;
using System.Collections;

public class playerHandler : MonoBehaviour {
	private bool inAir = false;
	private bool doubleJump = false;

	private int _animState = Animator.StringToHash("animState");
	private Animator _animator;


	public bool begin = true;	//!!!!!!!!


	// Use this for initialization
	void Start () {
		_animator = this.transform.GetComponent<Animator> ();


	}
	
	// Update is called once per frame
	void Update () {

				if (!begin) {
						if (this.GetComponent<Rigidbody2D>().velocity.y == 0.0f) {
								inAir = false;
								_animator.SetInteger (_animState, 0);
						}

						if (this.GetComponent<Rigidbody2D>().velocity.y != 0.0f) {
								inAir = true;
								_animator.SetInteger (_animState, 1);
						}
				}
				
				



		}

	public void jump(){
		if (!inAir) {
						doubleJump = false;
				}
				

		if(!inAir && !doubleJump){
				inAir = true;
				this.GetComponent<Rigidbody2D>().AddForce (Vector2.up * 600);

				
				}
				else if(inAir && !doubleJump){
				//this.rigidbody2D.AddForce (Vector2.up * 3500);
			this.GetComponent<Rigidbody2D>().velocity = new Vector2(0,15);
				doubleJump = true;
		}else if (inAir && doubleJump) {
			return;
		}

	}
}
