using UnityEngine;
using System.Collections;
using InControl;

public class MultiPlayerController : MonoBehaviour {
    public InputControl left, right, up, down, 
                         jump, duck, shoot, shield, run, cycleLightColor,
                         lightX, lightY, bushX, bushY, growBush;
    public Light spotLight;
    public BushMovement bushMove;

    private InputDevice[] playerDevices;
    private LightLookAt lightLook;
    private bool m_bHaveControllers = false;
    private int numPlayers = 0;
    private int maxPlayers = 4;

    public bool HaveControllers
    {
        get
        {
            return m_bHaveControllers;
        }
    }

	// Use this for initialization
	void Start () {
        playerDevices = new InputDevice[maxPlayers];
        lightLook = spotLight.GetComponent<LightLookAt>();
	}
	
	// Update is called once per frame
	void Update () {
        CheckForNewActivePlayer();
        HandleSpotLightInput();
        HandleTheBushInput();
	} 


    private void CheckForNewActivePlayer()
    {
        // Add a unique player device as soon as any button is pressed
        //Debug.Log(InputManager.Devices.Count + " controllers attached");

        for (int ii = 0; ii < InputManager.Devices.Count; ii++ )
        {
            InputDevice id = InputManager.Devices[ii];            
            
            if (id.Action1.WasPressed)
            {
                //Debug.Log("press detected");
                // skip device if it was already added
                bool newDevice = true;

                foreach (InputDevice playerDevice in playerDevices)
                {
                    if (playerDevice != null && playerDevice == id)
                    {
                        newDevice = false;
                        break;
                    }
                }

                // Add if the device is a new active one
                if (newDevice)
                {
                    m_bHaveControllers = true;

                    playerDevices[numPlayers] = id;
                    Debug.Log("Player " + numPlayers + " has arrived");
                    numPlayers++;
                    MapControlsToActions();

                    id.Vibrate(2f, 10f);
                }
            }
        }                    	
    }

    // Called when a new player device is added
    void MapControlsToActions()
    {
        // Debug.Log("Mapping actions for " + numPlayers + " players");
        // Partition the actions amongst the various players
        if (numPlayers == 1)
        {
            // can control everything
            InputDevice p1 = playerDevices[0];
            left = right    = p1.LeftStickX;
            up = down       = p1.LeftStickY;
            lightX          = p1.RightStickX;
            lightY          = p1.RightStickY;
            
            run            = p1.LeftBumper;
            jump = p1.Action1;
            cycleLightColor = p1.Action2;
            shoot           = p1.Action3;
            
        }
        else if (numPlayers == 2)
        {
            InputDevice p1 = playerDevices[0], p2 = playerDevices[1];

            left = right    =  p1.LeftStickX;
            up = down       = p1.LeftStickY;
            run = p1.LeftBumper;
            jump = p1.Action1;

            bushX = p2.LeftStickX;
            bushY = p2.LeftStickY;
            growBush = p2.Action2;

            //shoot           = p2.Action1;
            //cycleLightColor = p2.Action2;

            //lightX          = p2.LeftStickX;
            //lightY          = p2.LeftStickY;
            
        }
        else if (numPlayers == 3)
        {
            InputDevice p1 = playerDevices[0], p2 = playerDevices[1], p3 = playerDevices[2];

            left = right    = p1.LeftStickX;
            up = down       = p1.LeftStickY;
            run = p1.LeftBumper;
            cycleLightColor = p1.Action1;

            bushX = p2.LeftStickX;
            bushY = p2.LeftStickY;
            growBush = p2.Action2;

            lightX          = p3.LeftStickX;
            lightY          = p3.LeftStickY;
            jump            = p3.Action1;
            cycleLightColor = p3.Action2;

        }
        else 
        {
            InputDevice p1 = playerDevices[0], p2 = playerDevices[1], 
                        p3 = playerDevices[2], p4 = playerDevices[3];

            left = right    = p1.LeftStickX;
            cycleLightColor = p1.Action1;

            up = down       = p2.LeftStickY;            
            shoot = p2.Action1;
            
            lightX = p3.RightStickX;
            lightY = p3.RightStickY;

            jump = p4.Action1;
            run = p2.Action2;
        }      
    }

    private void HandleSpotLightInput()
    {
        if (m_bHaveControllers)
        {
            lightLook.MoveLookAt(lightX, lightY);

            if (cycleLightColor.WasPressed)
                lightLook.CycleLightColor();
        }
    }

    private void HandleTheBushInput()
    {
        if (m_bHaveControllers && numPlayers > 1)
        {
            bushMove.ProcessInput(bushX, bushY);

            if (growBush.WasPressed)
                bushMove.GrowBush();
        }
    }
}
