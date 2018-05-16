using UnityEngine;
using System.Collections;

public class monetkaScript : MonoBehaviour {
	private int ran;
	
	public Sprite kawa;
	public Sprite monetka;

	private ScoreHandler scoH;

	private SpriteRenderer crateRender; 

	private AudioSource player;
	public AudioClip kawaS;
	public AudioClip monetkaS;

	private int scoreValue;

	void Start () {
		player = this.GetComponent<AudioSource> ();
		scoreValue = 10;

		ran = Random.Range (1, 10);
		crateRender = this.transform.GetComponent<SpriteRenderer>();
		if (ran == 1) {						
						crateRender.sprite = kawa;
						player.clip = kawaS;
				} else {	
						crateRender.sprite = monetka;
						player.clip = monetkaS;
				}
	
			scoH = GameObject.Find("Score").GetComponent<ScoreHandler>();


	}
	
	// Update is called once per frame


	void OnTriggerEnter2D(Collider2D coll){ 
		
		if (coll.gameObject.tag == "Player") {
			scoH.AddScore(scoreValue);
			this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y - 10.0f);

			if(crateRender.sprite == kawa){
			GameObject.Find("Main Camera").GetComponent<LevelCreator>().gameSpeed +=1.0f;
				player.clip = kawaS;
			}
			else
				player.clip = monetkaS;

			player.Play();


			ran = Random.Range (1, 10);
			if (ran == 1)
				crateRender.sprite = kawa;
			else
				crateRender.sprite = monetka;
		}

	}
	
}
