using UnityEngine;
using System.Collections;
using InControl;

public class MultiPlayerController : MonoBehaviour {
    public InputControl left, right, up, down, jump, duck, run, 
                        raysX, raysY/*not used for rays, used for light*/, changeRayMode, rainRays,
                        platformX, platformY, plantPlatform, togglePlatformRot;

    //public Light spotLight;
    //private LightLookAt lightLook; // replacing light with ray
    public RayController rays;
    public PlatformController platform;

    private InputDevice[] playerDevices;
    private bool m_bHaveControllers = false;
    private int numPlayers = 0;
    private int maxPlayers = 4;
    private int[] ThreePlayerPermute  = new int[18] { 0, 1, 2,  0, 2, 1,  1, 0, 2,  1, 2, 0,  2, 0, 1,  2, 1, 0 };

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
        //lightLook = spotLight.GetComponent<LightLookAt>();
	}
	
	// Update is called once per frame
	void Update () {
        CheckForNewActivePlayer();
       // HandleSpotLightInput();
        HandlePlatformInput();
        HandleRayInput();
	} 


    private void CheckForNewActivePlayer()
    {
        // Add a unique player device as soon as any button is pressed
        //Debug.Log(InputManager.Devices.Count + " controllers attached");

        for (int ii = 0; ii < InputManager.Devices.Count; ii++ )
        {
            InputDevice id = InputManager.Devices[ii];            
            
            //if (id.Action1.WasPressed)
            //{
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
            //}
        }                    	
    }

    // Called when a new player device is added
    public void MapControlsToActions()
    {
        // Debug.Log("Mapping actions for " + numPlayers + " players");
        // Partition the actions amongst the various players
        if (numPlayers == 1)
        {
            // can control everything
            InputDevice p1 = playerDevices[0];
            left = right    = p1.LeftStickX;
            up = down       = p1.LeftStickY;
            raysX          = p1.RightStickX;
            raysY          = p1.RightStickY; // not used
            
            run            = p1.LeftBumper;
            jump = p1.Action1;
            changeRayMode = p1.Action2;
            rainRays = p1.Action3;
           
            
        }
        else if (numPlayers == 2)
        {
            InputDevice p1 = playerDevices[0], p2 = playerDevices[1];

            left = right    =  p1.LeftStickX;
            up = down       = p1.LeftStickY;
            run = p1.LeftBumper;
            jump = p1.Action1;

            platformX = p2.LeftStickX;
            platformY = p2.LeftStickY;
            plantPlatform = p2.Action2;
            togglePlatformRot = p2.Action3;

            //shoot           = p2.Action1;
            //cycleLightColor = p2.Action2;

            //lightX          = p2.LeftStickX;
            //lightY          = p2.LeftStickY;
            
        }
        else if (numPlayers == 3)
        {
            int RandVal = Random.Range(0, 6);
            RandVal *= 3;
            InputDevice p1 = playerDevices[ThreePlayerPermute[RandVal++]], p2 = playerDevices[ThreePlayerPermute[RandVal++]], p3 = playerDevices[ThreePlayerPermute[RandVal]];

            left = right    = p1.LeftStickX;
            up = down       = p1.LeftStickY;
            run             = p1.Action3;
            jump            = p1.Action1;

            platformX       = p2.LeftStickX;
            platformY       = p2.LeftStickY;            
            togglePlatformRot = p2.Action3;
            plantPlatform = p2.Action1;

            raysX          = p3.LeftStickX;
            raysY          = p3.LeftStickY;           
            changeRayMode  = p3.Action3;
            rainRays       = p3.Action1;

        }
        else if (numPlayers == 4)
        {
            InputDevice p1 = playerDevices[0], p2 = playerDevices[1], 
                        p3 = playerDevices[2], p4 = playerDevices[3];

            left = right    = p1.LeftStickX;
            changeRayMode = p1.Action1;

            up = down       = p2.LeftStickY;
            changeRayMode   = p2.Action1;
            
            raysX = p3.RightStickX;
            raysY = p3.RightStickY;

            jump = p4.Action1;
            run = p2.Action2;
        }      
    }

    //private void HandleSpotLightInput()
    //{
    //    if (m_bHaveControllers)
    //    {
    //        lightLook.MoveLookAt(raysX, raysY);

    //        if (rayMode.WasPressed)
    //            lightLook.CycleLightColor();
    //    }
    //}

    private void HandlePlatformInput()
    {
        if (m_bHaveControllers && numPlayers > 1)
        {
            platform.ProcessInput(platformX, platformY);

            if (plantPlatform.WasPressed)
                platform.GrowBush();

            if (togglePlatformRot.WasPressed)
                platform.ToggleBushMode();
        }
    }



    private void HandleRayInput()
    {
        if (m_bHaveControllers && numPlayers > 0)
        {
            rays.ProcessInput(raysX);

            if (changeRayMode.WasPressed)
                rays.CycleRayMode();

            if (rainRays.WasPressed)
                rays.LetItRain();
        }
    }


}
