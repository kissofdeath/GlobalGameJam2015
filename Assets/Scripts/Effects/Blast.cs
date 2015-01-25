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

    // dunno why this call didn't happen when the boxCollider2D attached to the blastprefab was not a trigger :-/
    //void OnCollisionEnter2D(Collision2D c)
    //{
    //    Debug.Log("Blast OnCollisionEnter " + c.gameObject.name);

    //    if (c.gameObject.layer == LayerMask.NameToLayer("Enemies"))
    //    { 
    //        Debug.Log("Blast collided with enemy " + c.gameObject.name);
    //        Destroy(c.gameObject);
    //    }
    //}

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            Debug.Log("Blast just pwned" + c.gameObject.name);
            Destroy(c.gameObject);
        }
    }

    void KillMe()
    {
        Destroy(this.gameObject);
    }
}
