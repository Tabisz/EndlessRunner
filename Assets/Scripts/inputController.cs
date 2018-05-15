using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class inputController : MonoBehaviour {

	private bool isMobile = true;
	private playerHandler _player;
	private float fadeTime;
	// Use this for initialization
	void Start () {
		 fadeTime = GameObject.Find("Player").GetComponent<Fading>().BeginFade(-1);
				if (Application.isEditor)
						isMobile = false;
		_player = GameObject.Find ("Player").GetComponent<playerHandler> ();


		}

	
	// Update is called once per frame
	void Update () {

						if (isMobile) {
						int tmpC = Input.touchCount;
						tmpC--;
						if (Input.GetTouch (tmpC).phase == TouchPhase.Began) {
								handleInteraction (true);
						}
						if (Input.GetTouch (tmpC).phase == TouchPhase.Ended) {
								handleInteraction (false);
						}
				
				} else 
				{
				if(Input.GetKeyDown(KeyCode.Space))
				handleInteraction (true);
				if(Input.GetKeyUp(KeyCode.Space))
				handleInteraction (false);
				} 

	
	}


	void handleInteraction(bool starting){
		if (starting) {
				_player.jump ();
				} else {

				}

}
	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.tag == "Player") {		
			StartCoroutine(LoadNew());
			fadeTime = GameObject.Find("Player").GetComponent<Fading>().BeginFade(1);
						
				}

		}
	IEnumerator LoadNew(){
		yield return new WaitForSeconds(fadeTime);
		//Application.LoadLevel ("Die");
		SceneManager.LoadScene ("Die");
	}


}
