using UnityEngine;
using System.Collections;

public class BushStep : MonoBehaviour {
    public float stepTimeout;
    private float lastSpawnTime;
    private Animator animatornator;
	// Use this for initialization
	void Start () 
    {
        animatornator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	   
	}

    void OnEnable()
    {
        //Debug.Log("at on enable");
        Invoke("PrepareDeactivateStep", stepTimeout);       
    }

    void PrepareDeactivateStep()
    {
        animatornator.Play("bush.disapear");
        Invoke("DeactivateStep", animatornator.animation.clip.length);
    }

    void DeactivateStep()
    {
        gameObject.SetActive(false);
    }
}
