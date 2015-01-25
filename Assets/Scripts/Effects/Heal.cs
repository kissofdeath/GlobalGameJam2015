using UnityEngine;
using System.Collections;

public class Heal : MonoBehaviour {
    public float blastLength = 1f;
    public float healAmount = 10f;
    public Player player; // set by RayController

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


    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "Player")
        {
            Debug.Log("Healed player");
            c.gameObject.GetComponentInParent<Player>().Heal(healAmount);
        }
    }

    void KillMe()
    {
        Destroy(this.gameObject);
    }
}
