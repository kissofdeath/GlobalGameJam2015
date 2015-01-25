using UnityEngine;
using System.Collections;
using UniArt.PixelScifiLandscape.Sample;

public class BulletMove : MonoBehaviour {

    private Vector3 target = new Vector3();
    public Player Kiln;
    public float speed = 10f;
    public float Damage = 5f;
    private float StartTime;
    public float SelfDestructTime = 8f;
    private bool TargetSet = false;
	// Use this for initialization


    void OnEnable()
    {
        StartTime = Time.time;
    }

    public void SetTarget(Vector3 tpos)
    {
        target = tpos - transform.position;
        target.Normalize();
        TargetSet = true;
    }

	// Update is called once per frame
	void Update () {

        if (TargetSet)
        {
            //check to see if we have flown long enough
            if (Time.time - StartTime >= SelfDestructTime)
            {
                Destroy(this.gameObject);
                return;
            }

            //continue to your target
            float step = speed * Time.deltaTime;
            Vector3 Delta = target * step;
            transform.Translate(Delta);
        }	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        Character_LadderTesterListener DeadBoy = other.GetComponent<Character_LadderTesterListener>();

        if (DeadBoy != null && Kiln != null)
        {
            Kiln.Damage(Damage);
        }

        //if I collide with anything just kill myself
        if (!other.gameObject.tag.Contains("Shooty"))
            Destroy(this.gameObject);
    }
}
