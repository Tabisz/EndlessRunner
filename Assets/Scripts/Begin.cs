using UnityEngine;
using System.Collections;

/* skrypt uruchamia gre z poziomu menu */
public class Begin : MonoBehaviour {

	private Tiling tiling1;
	private Tiling tiling2;
	private Tiling tiling3;
	private Tiling tiling4;
	public Transform start;
	public Vector3 end;
	private up_down up;
	public Transform player;
	private LevelCreator main;
	private inputController input;



	private Canvas menu;
	private Canvas score;

	public bool isMenu = true;

	private playerHandler anim;
	// Use this for initialization
	void Start () {

		tiling1 = GameObject.Find ("1_plan").GetComponent<Tiling>();
		tiling2 = GameObject.Find ("2_plan").GetComponent<Tiling>();
		main = GameObject.Find ("Main Camera").GetComponent<LevelCreator>();
		up = main.GetComponent<up_down>();
		end = new Vector3(player.position.x + 10.7f, player.position.y+2f, -1f);
		score = GameObject.Find ("ScoreC").GetComponent<Canvas>();
		menu =GameObject.Find ("Canvas").GetComponent<Canvas>();
		input = GameObject.Find ("Main Camera").GetComponent<inputController>();
		anim = GameObject.Find ("Player").GetComponent<playerHandler>();


	}
	
	// Update is called once per frame
	void Update () {
		if (!isMenu) {/* oddalanie kamery przy starcie gry*/
			Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position,end,0.01f);
			if(Camera.main.orthographicSize<6.29f){
				Camera.main.orthographicSize += 2f *Time.deltaTime;
				return;
			}
			//Time.timeScale =0;
			up.enabled = true;
			Object.Destroy(this.gameObject);

				}

	}
	public void onClickPlay()
	{
			
			tiling1.enabled = true;
			tiling2.enabled = true;
			input.enabled = true;
		main.isMenu = false;
		isMenu = false;
		score.enabled = true;
		menu.enabled = false;
		anim.begin = false;


	}
	public void onClickExit()
	{
		Application.Quit();
	}
}
