using UnityEngine;
using System.Collections;

public class BushMovement : MonoBehaviour {

    public enum BushMode { HORZ_BUSH, VERT_BUSH };
    public BushMode CurrentBMode = BushMode.HORZ_BUSH;
    public float moveSpeed = 5f;
    public GameObject[] steps;
    private int lastStepIndex = 0;   

    void Start()
    {

    }
   

    void Update()
    {

    }

    public void ToggleBushMode()
    {
        switch (CurrentBMode)
        {
            case BushMode.HORZ_BUSH:
                CurrentBMode = BushMode.VERT_BUSH;
                transform.Rotate(0, 0, 90);
                break;
            case BushMode.VERT_BUSH:
                CurrentBMode = BushMode.HORZ_BUSH;
                transform.Rotate(0, 0, -90);
                break;
        }
    }

    public void ProcessInput(float x, float y)
    {
        if (CurrentBMode == BushMode.VERT_BUSH)
        {
            transform.Rotate(0, 0, -90);
        }
        
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

        if (CurrentBMode == BushMode.VERT_BUSH)
        {
            transform.Rotate(0, 0, 90);
        }

    }


    public void GrowBush()
    {
        if (!steps[lastStepIndex].activeSelf)
        {
            steps[lastStepIndex].SetActive(true);
            steps[lastStepIndex].transform.position = this.transform.position;
            steps[lastStepIndex].transform.rotation = Quaternion.identity;

            if (CurrentBMode == BushMode.VERT_BUSH)
            {
                steps[lastStepIndex].transform.Rotate(0, 0, 90);
            }

            lastStepIndex++;
            lastStepIndex %= steps.Length;
        }
    }
   
   
}


