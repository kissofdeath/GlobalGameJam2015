using UnityEngine;
using System.Collections;

public class Chesty : MonoBehaviour {

    enum ToggleState { OPEN_STATE, CLOSED_STATE };
    ToggleState CurrentState;
    int OpenState;
    int CloseState;
    private Animator anim;
    public bool TestBool = false;

	// Use this for initialization
	void Start () {

        CurrentState = ToggleState.CLOSED_STATE;
	}
	
	// Update is called once per frame
	void Update () {

        if (TestBool)
        {
            ToggleChest();
            TestBool = false;
        }
            
	}

    void Awake()
    {
        anim = GetComponent<Animator>();
        OpenState = Animator.StringToHash("Base Layer.box_open");
        CloseState = Animator.StringToHash("Base Layer.box_close");      
    }

    public void ToggleChest()
    {
        switch (CurrentState)
        {
            case ToggleState.CLOSED_STATE:
                CurrentState = ToggleState.OPEN_STATE;
                anim.Play(OpenState);
                break;
            case ToggleState.OPEN_STATE:
                CurrentState = ToggleState.CLOSED_STATE;
                anim.Play(CloseState);
                break;
        }
        
    }

}
