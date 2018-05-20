using UnityEngine;
using System.Collections;

public class up_down : MonoBehaviour {
    //skrypt do sterowania kamera w osi y
	 GameObject pla;
	private float odleglosc;
    public float minLevel =-1;



	// Use this for initialization
	void Start(){
		pla = GameObject.Find ("Player");
		}

	void Update(){
				if (/*pla.transform.position.y < 10 &*/ pla.transform.position.y > minLevel-2) {
						odleglosc = pla.transform.position.y - transform.position.y + 2f;
						if (odleglosc > 0)
								transform.position = new Vector3 (transform.position.x, transform.position.y + 0.5f * odleglosc * Time.deltaTime, transform.position.z);
						else
								transform.position = new Vector3 (transform.position.x, transform.position.y + 5f * odleglosc * Time.deltaTime, transform.position.z);
				}
		}
}

