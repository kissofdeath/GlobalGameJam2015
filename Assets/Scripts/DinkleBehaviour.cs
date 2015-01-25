using UnityEngine;
using System.Collections;

public class DinkleBehaviour : MonoBehaviour 
{
    Rigidbody2D thisDinkle;
    Vector2 randomDinkleDirection;

	// Use this for initialization
    void OnEnable()
    {
        thisDinkle = gameObject.GetComponent<Rigidbody2D>();
        randomDinkleDirection = new Vector2(Random.Range(-.9f, .9f), 0f);
	}
	
	// Update is called once per frame
    void Start()
    {
        thisDinkle.AddForce(randomDinkleDirection, ForceMode2D.Impulse);
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name);

        if (col.gameObject.name.Contains("ground"))
        {
            //Destroy the dinkle
            Destroy(this.gameObject);

            //spawn magical explosion
        }
    }
}
