using UnityEngine;
using System.Collections;

public class crateScript : MonoBehaviour {

	private float maxY;
	private float minY;
	private int direction = 1;

	 public Sprite kawa;
	 public Sprite puszka;

	public bool inPlay = true;
	private bool releaseCrate = false;

	private SpriteRenderer crateRender; 
	// Use this for initialization
	void Start () {
		maxY = this.transform.position.y + 0.2f;
		minY = maxY - 0.6f;

		crateRender = this.transform.GetComponent<SpriteRenderer>();
		crateRender.sprite = puszka;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = new Vector2 (this.transform.position.x, this.transform.position.y + (direction * 0.01f));
		if (this.transform.position.y> maxY) 
						direction = -1;
		if (this.transform.position.y < minY)
						direction = 1;

		if (!inPlay && !releaseCrate)
						respawn ();
	
	}
	void OnTriggerEnter2D(Collider2D coll){

		if (coll.gameObject.tag == "Player") {

					GameObject.Find("Main Camera").GetComponent<LevelCreator>().gameSpeed +=1.0f;
		
			inPlay = false;
			this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y - 10.0f);
				
		}
	}
	void respawn(){
		releaseCrate = true;
		Invoke ("placeCrate", (float)Random.Range (3, 10));

	}
	void placeCrate(){

		inPlay = true;
		releaseCrate = false;
		maxY = this.transform.position.y + 0.2f;
		minY = maxY - 0.6f;
	}
}
