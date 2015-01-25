using UnityEngine;
using System.Collections;

public class BulletMove : MonoBehaviour {

    public Transform target;
    public float speed = 10f;
    private float StartTime;
    public float SelfDestructTime = 3f;
	// Use this for initialization
	void Start () {
	
	}

    void OnEnable()
    {
        StartTime = Time.time;
    }

	// Update is called once per frame
	void Update () {

        //check to see if we have flown long enough
        if( Time.time - StartTime >= SelfDestructTime)
        {
            gameObject.SetActive(false);
            return;
        }

        //continue to your target
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
	
	}
}
