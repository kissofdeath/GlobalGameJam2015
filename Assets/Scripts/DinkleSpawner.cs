using UnityEngine;
using System.Collections;

public class DinkleSpawner : MonoBehaviour 
{
    public float stepTimeout;
    private float lastSpawnTime;
    private Animator animatornator;
    public GameObject enemyPref;
    private bool flag = false;
	// Use this for initialization
	void Start () 
    {
        animatornator = GetComponent<Animator>();
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "02.body")
        {
            if (!flag)
            {
                flag = true;
                if (col.gameObject.name == "02.body")
                {
                    animatornator.Play("door.open");
                    Invoke("AnimationStopper", animatornator.animation.clip.length);
                }
            }
        }
    }

    void AnimationStopper()
    {
        InvokeRepeating("TurdDropper", 0.0f, 1.0f);
        animatornator.StopPlayback();
    }

    void TurdDropper()
    {
        GameObject.Instantiate(enemyPref, transform.position, Quaternion.identity);
    }
}