using UnityEngine;
using System.Collections;


public class LevelCreator : MonoBehaviour {



	public GameObject tilePos;
	private float startUpPosY;
	private  float tileWidth = 2.66f;
	public float hightSpacing = 0.5f;
	public int heightLevel = 0;
	private GameObject tmpTile;
	private GameObject tmpCoin;

	private GameObject collectedTiles;
	private GameObject gameLayer;


	public float gameSpeed = 5.0f;
	private float outofbounceX;
	private int blankCounter = 0;
	private int middleCounter = 0;
	private int coinCounter = 0;
	private int waitCounter = 0;
	private string lastTile = "right";
	private float startTime;

	public Sprite L1sprite;
	public Sprite M1sprite;
	public Sprite R1sprite;

	public Sprite L2sprite;
	public Sprite M2sprite;
	public Sprite R2sprite;

	public Sprite L3sprite;
	public Sprite M3sprite;
	public Sprite R3sprite;

	public ScoreHandler scoH;

	public bool isMenu = true;

	



	private Vector2 Jcollider = new Vector2 (2.65f, 9.55f);

	
	void Awake(){
		Application.targetFrameRate = 60;
	}


	
	void Start () {

			

		gameLayer = GameObject.Find ("gameLayer");

		collectedTiles = GameObject.Find ("tiles");

		for (int i = 0; i<24; i++){         //spawnowanie wszystkich prefabow uzywanych do ukladania budynkow, ustawianie ich poza widokiem kamery
			GameObject tmpg1 = Instantiate(Resources.Load("blok_lewy", typeof(GameObject))) as GameObject;
			tmpg1.transform.parent = collectedTiles.transform.FindChild("gLeft").transform;
			tmpg1.transform.position = Vector2.zero;
			GameObject tmpg2 = Instantiate(Resources.Load("blok_prawy", typeof(GameObject))) as GameObject;
			tmpg2.transform.parent = collectedTiles.transform.FindChild("gRight").transform;
			tmpg2.transform.position = Vector2.zero;
			GameObject tmpg3 = Instantiate(Resources.Load("blok_srodek", typeof(GameObject))) as GameObject;
			tmpg3.transform.parent = collectedTiles.transform.FindChild("gMiddle").transform;
			tmpg3.transform.position = Vector2.zero;
			GameObject tmpg4 = Instantiate(Resources.Load("blank", typeof(GameObject))) as GameObject;
			tmpg4.transform.parent = collectedTiles.transform.FindChild("gBlank").transform;
			tmpg4.transform.position = Vector2.zero;
 		}
		for (int i = 0; i<16; i++) {
			GameObject tCoin = Instantiate(Resources.Load("Monetka",typeof(GameObject))) as GameObject;
			tCoin.transform.parent = collectedTiles.transform.FindChild("tCoins").transform;
			tCoin.transform.position = Vector2.zero;
				}
		collectedTiles.transform.position = new Vector2(-60.0f, -20.0f);

		tilePos = GameObject.Find ("startTilePosition");
		startUpPosY = tilePos.transform.position.y;
		outofbounceX = tilePos.transform.position.x -4.0f;
		fillScene ();
		startTime = Time.time;

		scoH = GameObject.Find("Score").GetComponent<ScoreHandler>();


	}


	void FixedUpdate()// przesuwanie calego obszaru gry - postac biegnie
	{
				if (startTime - Time.time % 5 == 0 && gameSpeed < 10) {
						gameSpeed += 0.2f;
				}
				if(!isMenu)
			gameLayer.transform.position = new Vector2 (gameLayer.transform.position.x - gameSpeed * Time.deltaTime, gameLayer.transform.position.y);


		if (gameLayer.transform.childCount < 25) {// jezeli wykorzystywanych jest mniej niz 25 segmentow, pobierz segment z poza widoku kamery
			spawnTile ();
		}
		


                                                        //jezeli segment jest poza polem gry, przenies go do odpowiedniego miejsca gdzie bedzie czekal na ponowne wykorzystanie
				foreach (Transform child in gameLayer.transform) {
						if (child.position.x < outofbounceX) {
								switch (child.gameObject.name) {
								case"blok_lewy(Clone)":

										child.gameObject.transform.position = collectedTiles.transform.FindChild ("gLeft").transform.position;
										child.gameObject.transform.parent = collectedTiles.transform.FindChild ("gLeft").transform;
										break;
								case"blok_srodek(Clone)":
										if(waitCounter>0)
										waitCounter--;
										child.gameObject.transform.position = collectedTiles.transform.FindChild ("gMiddle").transform.position;
										child.gameObject.transform.parent = collectedTiles.transform.FindChild ("gMiddle").transform;
										break;
								case"blok_prawy(Clone)":
										child.gameObject.transform.position = collectedTiles.transform.FindChild ("gRight").transform.position;
										child.gameObject.transform.parent = collectedTiles.transform.FindChild ("gRight").transform;
										break;
								case"blank(Clone)":
										child.gameObject.transform.position = collectedTiles.transform.FindChild ("gBlank").transform.position;
										child.gameObject.transform.parent = collectedTiles.transform.FindChild ("gBlank").transform;
										break;
								case"Monetka(Clone)":
										child.gameObject.transform.position = collectedTiles.transform.FindChild ("tCoins").transform.position;
										child.gameObject.transform.parent = collectedTiles.transform.FindChild ("tCoins").transform;											
										break;  
								case"Reward":                                                                       // tu pickupy
										GameObject.Find ("Reward").GetComponent<crateScript> ().inPlay = false;
										break;
								default:
										Destroy (child.gameObject);
										break;

								}
				scoH.AddScore(1);
						}
				
				}

					if (coinCounter == 0 && waitCounter == 0) {     
						coinCounter = (int)Random.Range (5, 8);
						waitCounter = Random.Range (6,13);
				}
		}
						
						
				
		
		

	private void fillScene () {         //zapelnianie sceny jeszcze w menu
		for(int i = 0; i<8; i++){
			setTile("middle");
		}
		setTile("right");
	}

	private void setTile (string type) {        //pobieranie odpowiedniego segmentu z poza gry

		switch (type) {
		case"left":
				tmpTile = collectedTiles.transform.FindChild("gLeft").transform.GetChild(0).gameObject;
		break;
		case"right":
				tmpTile = collectedTiles.transform.FindChild("gRight").transform.GetChild(0).gameObject;
		break;
		case"middle":
				tmpTile = collectedTiles.transform.FindChild("gMiddle").transform.GetChild(0).gameObject;
			break;
		case"blank":
				tmpTile = collectedTiles.transform.FindChild("gBlank").transform.GetChild(0).gameObject;
			break;
					}
		tmpTile.transform.parent = gameLayer.transform;
		tmpTile.transform.position = new Vector2 (tilePos.transform.position.x+(tileWidth),startUpPosY + (heightLevel*hightSpacing));

		tilePos = tmpTile;
		lastTile = type;
	}
	private void spawnTile(){       // ustawianie segmentow w budynki



		if (coinCounter > 0) {
			setCoin();
			coinCounter--;	
		}

		if (blankCounter > 0) {
			setTile("blank");
			blankCounter--;
			return;
				}
		if (middleCounter > 0) {
			setTile("middle");
			middleCounter--;
			return;
		}
		if (lastTile == "blank") {              // losowanie dlugosci budynkow i odstepu miedzy nimi

			//changeHeight ();
						setTile ("left");                   
						middleCounter = (int)Random.Range (1, 4);
				} else if (lastTile == "right") {
						changeHeight ();
						blankCounter = (int)Random.Range (1, 2);
				} else if (lastTile == "middle") {
					setTile("right");
				}


	
				
	}

	private void setCoin(){
		tmpCoin = collectedTiles.transform.FindChild("tCoins").transform.GetChild(0).gameObject;
		tmpCoin.transform.parent = gameLayer.transform;
		tmpCoin.transform.position = new Vector2 (tilePos.transform.position.x+(tileWidth),startUpPosY + (heightLevel*hightSpacing)+6.0f);

		}

	private void changeHeight(){                    // losowanie wysokosci tak zeby nie wychodzily poza jakis zakres i zeby dalo sie na nie wskoczyc
		int newHeightLevel = (int)Random.Range (1, 4);


		switch (newHeightLevel) {				// 2 <= heightlevel => 5		_-_4 wysokości budynkow

		case 1:
			heightLevel++;
			if(heightLevel>6)		//7
				heightLevel-=2;
			break;

		case 2:
			heightLevel--;
			if(heightLevel<1)		//0
				heightLevel += 2;
			break;
		case 3:
			heightLevel += 2;
			if(heightLevel>6)		//7
				heightLevel -= 3;
			break;
		case 4:
			heightLevel -= 2;		
			if(heightLevel<1)		//0
				heightLevel += 3;
			break;
		
		}

		if (heightLevel == 2 | heightLevel == 3) {              // zmiana koloru budynkow w zaleznosci od wysokosci

						foreach (Transform child in collectedTiles.transform.FindChild("gLeft")) {
								child.gameObject.GetComponent<SpriteRenderer> ().sprite = L1sprite;
								child.gameObject.GetComponent<BoxCollider2D> ().size = Jcollider;
								

						}

						foreach (Transform child in collectedTiles.transform.FindChild("gRight")) {
								child.gameObject.GetComponent<SpriteRenderer> ().sprite = R1sprite;
								child.gameObject.GetComponent<BoxCollider2D> ().size = Jcollider;
								
						}

						foreach (Transform child in collectedTiles.transform.FindChild("gMiddle")) {
								child.gameObject.GetComponent<SpriteRenderer> ().sprite = M1sprite;
								child.gameObject.GetComponent<BoxCollider2D> ().size =Jcollider;
						}
				
				
		} if(heightLevel == 4) {
						
						foreach (Transform child in collectedTiles.transform.FindChild("gLeft")) {
								child.gameObject.GetComponent<SpriteRenderer> ().sprite = L2sprite;
								child.gameObject.GetComponent<BoxCollider2D> ().size = Jcollider;
								
								
				
						}
			
						foreach (Transform child in collectedTiles.transform.FindChild("gRight")) {
								child.gameObject.GetComponent<SpriteRenderer> ().sprite = R2sprite;
								child.gameObject.GetComponent<BoxCollider2D> ().size = Jcollider;
								
						}
			
						foreach (Transform child in collectedTiles.transform.FindChild("gMiddle")) {
								child.gameObject.GetComponent<SpriteRenderer> ().sprite = M2sprite;
								child.gameObject.GetComponent<BoxCollider2D> ().size = Jcollider;
						}
						
				}
		if(heightLevel == 5) {
			
			foreach (Transform child in collectedTiles.transform.FindChild("gLeft")) {
				child.gameObject.GetComponent<SpriteRenderer> ().sprite = L3sprite;
				child.gameObject.GetComponent<BoxCollider2D> ().size = Jcollider;

				
				
			}
			
			foreach (Transform child in collectedTiles.transform.FindChild("gRight")) {
				child.gameObject.GetComponent<SpriteRenderer> ().sprite = R3sprite;
				child.gameObject.GetComponent<BoxCollider2D> ().size = Jcollider;

			}
			
			foreach (Transform child in collectedTiles.transform.FindChild("gMiddle")) {
				child.gameObject.GetComponent<SpriteRenderer> ().sprite = M3sprite;
				child.gameObject.GetComponent<BoxCollider2D> ().size = Jcollider;
			}
			
		}
	
	}		



}
