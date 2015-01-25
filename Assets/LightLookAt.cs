using UnityEngine;
using System.Collections;

public class LightLookAt : MonoBehaviour {

    Vector3 Target;
    public GameObject TargetObject;
    public Camera TargetCamera;
    public int MoveSpeed = 10;
    private Color[] cycleColors = { Color.yellow, Color.blue, Color.red, Color.green };
    private uint currentColorIndex = 0;
    private Player klin;

    //public Color Test;
    //public float TestX;
    //public float TestY;

    public void MoveLookAt(float x, float y){

        Vector3 TempTarget = Target + new Vector3(MoveSpeed * x * Time.deltaTime, MoveSpeed * y * Time.deltaTime, 0);
        Vector3 viewPos = TargetCamera.camera.WorldToViewportPoint(TempTarget);
        //check for outside of viewport, don't update if it happens
        if (viewPos.x < 0.0f || viewPos.y < 0.0f || viewPos.x > 1.0f || viewPos.y > 1.0f)
        {
            return;
        }

        //inside viewport so update target
        Target = TempTarget;

    }

    void UpdateLookAt(){

        Vector3 viewPos = TargetCamera.camera.WorldToViewportPoint(Target);
        //check for outside of viewport, don't update if it happens
        if ( viewPos.x < 0.0f )
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

        Target = TargetCamera.camera.ViewportToWorldPoint(viewPos);
        
    }

    public void CycleLightColor()
    {        
        currentColorIndex++;
        currentColorIndex %= (uint) cycleColors.Length;
        this.gameObject.light.color = cycleColors[currentColorIndex];
        
    }

	// Use this for initialization
	void Start () {
        Target = TargetObject.transform.position;
        klin = TargetObject.GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
        UpdateLookAt();
        transform.LookAt(Target);
        //MoveLookAt(TestX, TestY);
	}

    void OnTriggerStay(Collider col)
//    void OnCollisionEnter(Collision col)
    {
        //Debug.Log("Col on light called");
        if (col.gameObject.name.Contains("healer") && cycleColors[currentColorIndex] == Color.green)
        {
            klin.Heal(50f);
            Destroy(col.gameObject.GetComponent<ParticleSystem>());
            Destroy(col);
            
        }
    }
}
