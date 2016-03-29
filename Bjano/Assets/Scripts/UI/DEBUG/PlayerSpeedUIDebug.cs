using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerSpeedUIDebug : MonoBehaviour {

    private Text text;

	// Use this for initialization
	void Start () {
        text = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        text.text = GameObject.Find("Player").GetComponent<Rigidbody2D>().velocity.x.ToString();
	}
}
