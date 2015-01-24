using UnityEngine;
using System.Collections;
using InControl;

namespace UniArt.PixelScifiLandscape.Sample
{
    [AddComponentMenu("UniArt/PixelScifiLandscape/Sample/Character/Character_Input")]
    public class Proto_Character_Input : MonoBehaviour
    {
        private InputControl left, right, up, down, jump, duck, shoot, shield, run;
        private bool m_bJumpWasPressed;

        private bool m_bJump;
        private bool m_bHaveControllers = false;

        private bool m_bRun;

        public bool Run
        {
            get
            {
                return m_bRun;
            }
        }

        public bool Jump
        {
            get
            {
                return m_bJump;
            }
        }

        public bool JumpHeld
        {
            get
            {
                return m_bJumpWasPressed;
            }
        }

        public float Horizontal
        {
            get
            {
                float fValue = 0.0f;
                if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.A) || (m_bHaveControllers && left < 0))
                {
                    fValue -= 1.0f;
                }

                if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) || (m_bHaveControllers && right > 0))
                {
                    fValue += 1.0f;
                }

                return fValue;
            }
        }

        public float Vertical
        {
            get
            {
                float fValue = 0.0f;
                if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) || (m_bHaveControllers && down < 0))
                {
                    fValue -= 1.0f;
                }

                if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.W) || (m_bHaveControllers && up > 0))
                {
                    fValue += 1.0f;
                }

                return fValue;
            }
        }

        public bool JumpInput
        {
            get
            {
                return Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt) || (m_bHaveControllers && jump);
            }
        }

        public bool RunInput
        {
            get
            {

                //Debug.Log("run: " + run + run.IsPressed + run.RawValue);

                return Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftCommand) || Input.GetKey(KeyCode.RightCommand) || Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) || (m_bHaveControllers && run); //todo
            }
        }

        void Start()
        {
            // assign various player actions to specific controllers
            if (InputManager.Devices.Count == 0)
                return;

            if (InputManager.Devices[0] == null || InputManager.Devices[1] == null || InputManager.Devices[2] == null)
            {
                Debug.LogError("Null handles to devices");
            }
            else
            {
                m_bHaveControllers = true;
                // assume you have 2 players [todo randomize]
                left = InputManager.Devices[0].LeftStickX;
                right = InputManager.Devices[1].LeftStickX;
                up = InputManager.Devices[0].LeftStickY;
                down = InputManager.Devices[1].LeftStickY;

                jump = InputManager.Devices[2].Action1;
                duck = InputManager.Devices[1].Action1;
                shoot = InputManager.Devices[1].Action2;
                shield = InputManager.Devices[0].Action2;
                run = InputManager.Devices[0].Action3; // InputManager.Devices[0].RightTrigger has issues; see http://www.gallantgames.com/posts/27/details-on-the-xbox-360-controller-bug-in-unity
            }

         }

        private void FixedUpdate()
        {
            UpdateRunInput();
            UpdateJumpInput();
        }

        private void UpdateJumpInput()
        {
            bool bJumpPressed = JumpInput;
            bool bJumpJustPressed = m_bJumpWasPressed == false && bJumpPressed;
            m_bJumpWasPressed = bJumpPressed;

            m_bJump = bJumpJustPressed;
        }

        private void UpdateRunInput()
        {
            m_bRun = RunInput;
        }
    }
}
