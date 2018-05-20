using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectorScript : MonoBehaviour {

	
	// Update is called once per frame
	void Update () {
        if (transform.childCount > 3)
            Destroy(transform.GetChild(0).gameObject);
	}
}
