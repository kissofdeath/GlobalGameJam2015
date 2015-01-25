using UnityEngine;
using System.Collections;

public class Blast : MonoBehaviour {
    public float blastLength = 1f;

    void OnEnable()
    {
        Invoke("KillMe", blastLength);
    }

	// Use this for initialization
	void Start () {
	
	}
	

	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            Destroy(c.gameObject);
        }
    }

    void KillMe()
    {
        Destroy(this.gameObject);
    }
}
