using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseMouvement : MonoBehaviour {

    private float       _screenTier = Screen.width / 3;

	void Start () {
    }
	
	void Update () {
        float pos = Input.mousePosition.x;
        if (pos > 0 && pos < this._screenTier)
            transform.Rotate(new Vector3(0, -40, 0) * Time.deltaTime);
        else if (pos > this._screenTier * 2 && pos < Screen.width)
            transform.Rotate(new Vector3(0, 40, 0) * Time.deltaTime);
		
	}
}
