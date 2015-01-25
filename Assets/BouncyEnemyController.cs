using UnityEngine;
using System.Collections;

public class BouncyEnemyController : MonoBehaviour {
	public float jumpInterval = 3.0f;
	public const float gravity = 2.0f;
	public float rotateSpeed = 6.0f;
	private float _timeElapsed = 0;
	private float _initGndLvl;
	private Vector2 _velocity;
	private GameObject _playerObject;
	private bool _facingLeft = true;
	private bool _isJumping = false;
	private float _rotAngle = 0;
	private bool _foundPlayer = false;
	private Vector3 _initPosition;
	// Use this for initialization
	void Start () {
		_initGndLvl = transform.position.y;
		_velocity = Vector2.zero;
		_playerObject = GameObject.FindGameObjectWithTag("Player");
		_initPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		_timeElapsed += Time.deltaTime;
		if(Mathf.Abs(CalDistanceVec()) > 2.0f)
		{
			_foundPlayer = false;
		}
		else if(Mathf.Abs(CalDistanceVec()) < 0.4f)
		{
			renderer.material.color = Color.red;
		}
		else
		{
			renderer.material.color = Color.white;
		}

		//Only jump if player has been seen
		if(_foundPlayer)
		{
			if(_timeElapsed > jumpInterval)
			{
				//Custom "Physics"
				if(Mathf.Abs(CalDistanceVec()) > 1.0f)
				{
					_velocity = new Vector2(0, 1.5f);
					_timeElapsed = 0;
					ResetToInitGndLvl();
					_isJumping = true;
				}
	        }
		}
		_velocity = new Vector2(_velocity.x, _velocity.y - gravity * Time.deltaTime);
		if(transform.position.y < _initGndLvl)
		{
			_velocity = new Vector2(_velocity.x, 0);
			ResetToInitGndLvl();
			_isJumping = false;
			//transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0);
		}
		transform.position = new Vector3(transform.position.x + _velocity.x * Time.deltaTime, 
		                                 transform.position.y + _velocity.y * Time.deltaTime, 
		                                 transform.position.z);

		if(_isJumping) {
			if(_facingLeft)
			{
				_rotAngle += Time.deltaTime * rotateSpeed * 90.0f;
			}
			else
			{
				_rotAngle -= Time.deltaTime * rotateSpeed * 90.0f;
			}
		}
		else {
			_rotAngle = 0;
		}
		transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, _rotAngle);

		if(_foundPlayer || _isJumping){
			float distVec = CalDistanceVec();
			if(distVec < 0)
			{
				if(_isJumping) {
					_velocity = new Vector2(-1, _velocity.y);
	            }
				else {
					if(Mathf.Abs(CalDistanceVec()) < 0.4f){
						_velocity = new Vector2(0, _velocity.y);
	                }
					else{
						_velocity = new Vector2(-0.5f, _velocity.y);
					}
				}
				if(!_facingLeft)
				{
					changeFacing();
				}
			}
			else
			{
				if(_isJumping) {
					_velocity = new Vector2(1, _velocity.y);
				}
				else {
					if(Mathf.Abs(CalDistanceVec()) < 0.4f){
						_velocity = new Vector2(0, _velocity.y);
	                }
	                else{
						_velocity = new Vector2(0.5f, _velocity.y);
					}
				}
	            if(_facingLeft)
				{
					changeFacing();
	            }
	        }
		}
		else{
			float distVec = _initPosition.x - transform.position.x;
			//Already near spawn point, go back to hide
			if(Mathf.Abs (distVec) < 0.2f)
			{
				renderer.material.color = Color.blue;
				_velocity = new Vector2(0, _velocity.y);
			}
			else if(distVec > 0) //Move monster back to the spawn point
			{
				_velocity = new Vector2(0.5f, _velocity.y);
				if(_facingLeft)
				{
					changeFacing();
				}
			}
			else
			{
				_velocity = new Vector2(-0.5f, _velocity.y);
				if(!_facingLeft)
				{
					changeFacing();
				}
			}
		}
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Edge")
		{
			if(other.transform.position.x < transform.position.x)
			{
				//Left edge
				transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y, transform.position.z);
			}
			else
			{
				transform.position = new Vector3(transform.position.x - 0.1f, transform.position.y, transform.position.z);
			}
			_velocity = new Vector2(0, _velocity.y);
			_foundPlayer = false;
		}
	}
	
	void ResetToInitGndLvl(){
		transform.position = new Vector3(transform.position.x,
		                                 _initGndLvl,
		                                 transform.position.z);
    }
	void changeFacing(){
		transform.localScale = new Vector3(- transform.localScale.x, transform.localScale.y, transform.localScale.z);
		_facingLeft = !_facingLeft;
    }
	float CalDistanceVec(){
		Vector3 playerPos = _playerObject.transform.position;
		Vector3 enemyPos = transform.position;
		float distanceVec = playerPos.x - enemyPos.x;
		return distanceVec;
    }
	public void FoundPlayer()
	{
		_foundPlayer = true;
    }
}
