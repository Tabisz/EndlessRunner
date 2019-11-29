using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {
	private float fadeTime;

	void Start () {
		fadeTime = GameObject.Find("Main Camera").GetComponent<Fading>().BeginFade(-1);
		
	}
	public void OnClickPlay(){
		StartCoroutine(LoadNew("Main"));

	}

	public void OnClickExit(){
		StartCoroutine(LoadNew("Quit"));
	}

    public void ClearTable()
    {
        PlayerPrefs.DeleteAll();
        OnClickPlay();
    }


    IEnumerator LoadNew(string level){
		fadeTime = GameObject.Find("Main Camera").GetComponent<Fading>().BeginFade(1);
		yield return new WaitForSeconds(fadeTime);
		if(level == "Quit")
			Application.Quit();
        else
		    SceneManager.LoadScene (level);




	}
}