using UnityEngine;
using System.Collections;

public class DinkleBehaviour : MonoBehaviour 
{
    Rigidbody2D thisDinkle;
    Vector2 randomDinkleDirection;
    private Animator animatornator;

	// Use this for initialization
    void OnEnable()
    {
        animatornator = GetComponent<Animator>();
        thisDinkle = gameObject.GetComponent<Rigidbody2D>();
        randomDinkleDirection = new Vector2(Random.Range(-1.5f, 1.5f), 0f);
	}
	
	// Update is called once per frame
    void Start()
    {
        thisDinkle.AddForce(randomDinkleDirection, ForceMode2D.Impulse);
	}

    void OnCollisionEnter2D(Collision2D col) 
    {
        if (!col.gameObject.name.Contains("dinkle"))
        {
            //play the expolsion
            animatornator.Play("dinkle.explosion");
        }

        if (col.gameObject.name.Contains("ground"))
        {
            Invoke("TheDinkleStopper", 0.15f);

            //invoke the animation end timer
            //Invoke("AnimationStopper", animatornator.animation.clip.length);
        }

        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponentInParent<Player>().Damage(25);
            TheDinkleStopper();
        }
    }

    void TheDinkleStopper()
    {
        thisDinkle.Sleep();
        //invoke the animation end timer
        Invoke("AnimationStopper", animatornator.animation.clip.length);
    }

    void AnimationStopper()
    {
        animatornator.StopPlayback();
        //Destroy the dinkle
        Destroy(this.gameObject);
    }
}
