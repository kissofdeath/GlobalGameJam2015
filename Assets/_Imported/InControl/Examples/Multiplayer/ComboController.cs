using System;   
using UnityEngine;
using InControl;


namespace MultiplayerExample
{
	public class ComboController : MonoBehaviour
	{
		public int playerNum;
        public float moveSpeed, jumpSpeed;
        private InputControl left, right, jump, duck, shoot, shield;
        public bool isJumping;
        private int floorLayer;

        void Start()
        {
            // assign various player actions to specific controllers

            if (InputManager.Devices[0] == null || InputManager.Devices[1] == null)
                Debug.LogError("Null handles to devices");

            // assume you have 2 players [todo randomize]
            left = InputManager.Devices[0].LeftStickX;
            right = InputManager.Devices[1].LeftStickX;
            jump = InputManager.Devices[0].Action1;
            duck = InputManager.Devices[1].Action1;
            shoot = InputManager.Devices[1].Action2;
            shield = InputManager.Devices[0].Action2;

            floorLayer = LayerMask.NameToLayer("floor");

            isJumping = false;

        }

		void Update()
		{
            HandlePlayerInput();
		}

        void FixedUpdate()
        {
            if (jump && !isJumping)
            {
                Debug.Log("Jump: " + jump);
               // transform.position += transform.up * Time.deltaTime * jumpSpeed;
                rigidbody.AddForce(transform.up * jumpSpeed);
                isJumping = true;
            }

        }

        void HandlePlayerInput()
        {
            if (left < 0)
                transform.position += left * transform.right * Time.deltaTime * moveSpeed;

            if (right > 0)
                transform.position += right * transform.right * Time.deltaTime * moveSpeed;

 

            if (duck)
                Debug.Log("Duck");

            if (shoot)
                Debug.Log("Shoot");

            if (shield)
                Debug.Log("Shield");



        }

        void OnCollisionStay(Collision col)
        {
            Debug.Log("Collision detection at " + Time.time);

            if (col.gameObject.layer == floorLayer)
                isJumping = false;
        }


		void UpdateCubeWithInputDevice( InputDevice inputDevice )
		{
			// Set object material color based on which action is pressed.
			if (inputDevice.Action1)
			{
				renderer.material.color = Color.green;
			}
			else
			if (inputDevice.Action2)
			{
				renderer.material.color = Color.red;
			}
			else
			if (inputDevice.Action3)
			{
				renderer.material.color = Color.blue;
			}
			else
			if (inputDevice.Action4)
			{
				renderer.material.color = Color.yellow;
			}
			else
			{
				renderer.material.color = Color.white;
			}
			
			// Rotate target object with both sticks and d-pad.
			transform.Rotate( Vector3.down,  500.0f * Time.deltaTime * inputDevice.Direction.X, Space.World );
			transform.Rotate( Vector3.right, 500.0f * Time.deltaTime * inputDevice.Direction.Y, Space.World );
			transform.Rotate( Vector3.down,  500.0f * Time.deltaTime * inputDevice.RightStickX, Space.World );
			transform.Rotate( Vector3.right, 500.0f * Time.deltaTime * inputDevice.RightStickY, Space.World );
		}
	}
}

