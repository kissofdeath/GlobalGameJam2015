using UnityEngine;
using System.Collections;

public class RayController : MonoBehaviour {
    public enum RayMode { BLAST, SHIELD, HEAL };

    public float viewportY; 
    public GameObject blast, shield, heal;
    public float moveSpeed;

    private RayMode curRayMode = RayMode.BLAST;

	// Use this for initialization
	void Start () {
     
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void CycleRayMode()
    {
        if (curRayMode == RayMode.HEAL)
            curRayMode = RayMode.BLAST;
        else
            curRayMode++;
    }

    public void ProcessInput(float x)
    {
        // keep the ray game object mostly around the same screenspace/viewport Y
        Vector3 tmpPos = transform.position + transform.right * x * moveSpeed * Time.deltaTime;

        // clamp the position in X and Y
        Vector3 viewPos = Camera.main.WorldToViewportPoint(tmpPos);
        //check for outside of viewport, don't update if it happens
        if (viewPos.x < 0.0f)
        {
            viewPos.x = 0.0f;
        }
        else if (viewPos.x > 1.0f)
        {
            viewPos.x = 1.0f;
        }
        if (viewPos.y < 0.0f)
        {
            viewPos.y = 0.0f;
        }
        else if (viewPos.y > 1.0f)
        {
            viewPos.y = 1.0f;
        }

        viewPos.y = viewportY;

        transform.position = Camera.main.ViewportToWorldPoint(viewPos);        
    }

    public void LetItRain()
    {
        Object effect;
        Debug.Log("Let it rain " + curRayMode);
        switch (curRayMode)
        {         
            case RayMode.BLAST:
                effect = Instantiate(blast, transform.position, Quaternion.identity);
                break;

            case RayMode.SHIELD:
                effect = Instantiate(shield, transform.position, Quaternion.identity);
                break;

            case RayMode.HEAL:
                effect = Instantiate(heal, transform.position, Quaternion.identity);
                break;
        }

        
    }

}
