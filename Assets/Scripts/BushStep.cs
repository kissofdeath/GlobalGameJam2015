using UnityEngine;
using System.Collections;

public class BushStep : MonoBehaviour {
    public float stepTimeout;
    private float lastSpawnTime;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	   
	}

    void OnEnable()
    {
        //Debug.Log("at on enable");
        Invoke("DeactivateStep", stepTimeout);       
    }


    void DeactivateStep()
    {
        gameObject.SetActive(false);
    }


    //IEnumerator GrowStep()
    //{
    //    Debug.Log(this.gameObject.name + ": starting countdown at " + Time.time);
    //    yield return new WaitForSeconds(stepTimeout);

    //    Debug.Log(this.gameObject.name + ": deactivating object  at " + Time.time);
    //    gameObject.SetActive(false);
    //}

}
