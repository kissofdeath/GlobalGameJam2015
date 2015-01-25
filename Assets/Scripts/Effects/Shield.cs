using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour {
    public float shieldDuration = 3f;

    void OnEnable()
    {
        Invoke("KillMe", shieldDuration);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void KillMe()
    {
        Destroy(this.gameObject);
    }

}
