using UnityEngine;
using System.Collections;

public class LightLookAt : MonoBehaviour {

    Vector3 Target;
    public GameObject TargetObject;
    public Camera TargetCamera;
    public int MoveSpeed = 10;
    public Light ColorLight;
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

    public void ChangeLightColor( Color newColor ){

        ColorLight.color = newColor;
    }

	// Use this for initialization
	void Start () {
        Target = TargetObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        UpdateLookAt();
        transform.LookAt(Target);
        //ChangeLightColor(Test);
        //MoveLookAt(TestX, TestY);
	}
}
