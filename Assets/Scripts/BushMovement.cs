using UnityEngine;
using System.Collections;

public class BushMovement : MonoBehaviour {

    public float moveSpeed = 5f;
    public GameObject[] steps;
    private int lastStepIndex = 0;   

    void Start()
    {

    }
   

    void Update()
    {

    }



    public void ProcessInput(float x, float y)
    {
       Vector3 tmpPos = transform.position;
        tmpPos += x * transform.right * moveSpeed * Time.deltaTime;
        tmpPos += y * transform.up * moveSpeed * Time.deltaTime;

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

        transform.position = Camera.main.ViewportToWorldPoint(viewPos);
    }


    public void GrowBush()
    {
        if (!steps[lastStepIndex].activeSelf)
        {
            steps[lastStepIndex].SetActive(true);
            steps[lastStepIndex].transform.position = this.transform.position;

            lastStepIndex++;
            lastStepIndex %= steps.Length;
        }
    }
   
   
}


