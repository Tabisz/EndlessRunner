using UnityEngine;
using System.Collections;

public class Reklamy : MonoBehaviour {

	private const string AdUnitId = "ca-app-pub-5563323914149485/3393923250";
	private AdMobPlugin adMob;
	public bool isReklama = false;
	// Use this for initialization
	void Start () {
	if (isReklama == false) {
						adMob = GetComponent<AdMobPlugin> ();
						adMob.CreateBanner (AdUnitId, AdMobPlugin.AdSize.SMART_BANNER, true);
						adMob.RequestAd ();
						adMob.ShowBanner ();
						isReklama = true;
				}
		//adMob.HideBanner();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
