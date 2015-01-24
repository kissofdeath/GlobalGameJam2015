using UnityEngine;
using System.Collections;
using InControl;

namespace UniArt.PixelScifiLandscape.Sample
{
    [AddComponentMenu("UniArt/PixelScifiLandscape/Sample/Character/Character_Input")]
    public class Proto_Character_Input : MonoBehaviour
    {
        public MultiPlayerController m_ComboController;

        private bool m_bJumpWasPressed;

        private bool m_bJump;

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
                if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.A) || (m_ComboController.HaveControllers && m_ComboController.left < 0))
                {
                    fValue -= 1.0f;
                }

                if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) || (m_ComboController.HaveControllers && m_ComboController.right > 0))
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
                if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) || (m_ComboController.HaveControllers && m_ComboController.down < 0))
                {
                    fValue -= 1.0f;
                }

                if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.W) || (m_ComboController.HaveControllers && m_ComboController.up > 0))
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
                return Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt) || (m_ComboController.HaveControllers && m_ComboController.jump.WasPressed);
            }
        }

        public bool RunInput
        {
            get
            {
                return Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftCommand) || Input.GetKey(KeyCode.RightCommand) || Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) || (m_ComboController.HaveControllers && m_ComboController.run); 
            }
        }

        void Start()
        {
            m_ComboController = gameObject.GetComponent<MultiPlayerController>();

            if (m_ComboController == null)
                Debug.LogError("WTF");
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
