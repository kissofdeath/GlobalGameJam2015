using UnityEngine;
using System.Collections;

public class RayController : MonoBehaviour {
    public enum RayMode { BLAST, SHIELD, HEAL };

    public float viewportY; 
    public GameObject blast, shield, heal;
    public float moveSpeed;
    public LayerMask effectsLayers;
    public float[] coolDowns = { 3f, 4f, 6f };
    private float[] timeSinceLastUsed = {10f, 10f, 10f};

    private Color[] rayModeColors = { Color.red, Color.blue, Color.green };
    private RayMode curRayMode = RayMode.BLAST;

	// Use this for initialization
	void Start () {
        renderer.material.color = rayModeColors[(uint)curRayMode];        
	}
	
	// Update is called once per frame
    void Update()
    {
        for (int ii = 0; ii < timeSinceLastUsed.Length; ii++)
            timeSinceLastUsed[ii] += Time.deltaTime; // while pausing via Time.timescale = 0, Time.deltatime will be 0. So cooldowns wont be affected by pause.

        if (Input.GetKeyDown(KeyCode.B))
            LetItRain();

        if (Input.GetKey(KeyCode.Q))
            ProcessInput(-5f);

        if (Input.GetKey(KeyCode.E))
            ProcessInput(5f);
    }

    public void CycleRayMode()
    {
        if (curRayMode == RayMode.HEAL)
            curRayMode = RayMode.BLAST;
        else
            curRayMode++;

        renderer.material.color = rayModeColors[(uint)curRayMode];
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
        if (timeSinceLastUsed[(uint)curRayMode] < coolDowns[(uint)curRayMode]) {
            Debug.Log("Cooldown...hold your horses.");
            return;
        }

        Vector2 effectSpawnPos = new Vector2();

        // Need to intelligently spawn the effect such that it stops on colliding a platform. Don't use the y position of
        // the rays object either - start the cast from the top of the viewport
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 1.0f, 0.0f)).y), -Vector2.up, Mathf.Infinity);
        if (hit.collider != null)
        {
            //Debug.DrawRay(transform.position, new Vector3(hit.point.x, hit.point.y, 0), Color.green, 5f);
            //Debug.Log("Found collider at " + hit.point + " with " + hit.collider.gameObject.name);
            effectSpawnPos = hit.point;            
        }

        GameObject prefab = blast; // init

        switch (curRayMode)
        {         
            case RayMode.BLAST:
                prefab = blast;
                break;

            case RayMode.SHIELD:
                prefab = shield;
                break;

            case RayMode.HEAL:
                prefab = heal;
                break;
        }

        //Debug.Log("blast y size" + prefab.collider2D);

        effectSpawnPos.y += 4.67f * 0.5f; // prefab.collider2D.bounds.size.y returns 0 :-/
        Instantiate(prefab, effectSpawnPos, Quaternion.identity);
        

        timeSinceLastUsed[(uint)curRayMode] = 0f;        
    }

}
