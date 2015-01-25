using UnityEngine;
using System.Collections;

public class BouncyEnemyController : MonoBehaviour {
	public float jumpInterval = 3.0f;
	public const float gravity = 2.0f;
	public float rotateSpeed = 6.0f;
    public float dmg = 20.0f;
	private float _timeElapsed = 0;
	private float _initGndLvl;
	private Vector2 _velocity;
	private GameObject playerObject;
	private bool _facingLeft = true;
	private bool _isJumping = false;
	private Vector3 _initPosition;
	private Animator _animator;
    private Player playerComp;
	enum State{
		Hide, //In the mud
		Wake, //Rise, my hero
		Idle, //At the idle wondering what to to next
		WalkBack, //Resolute to go to sleep
		Attack    //Either chasing or attacking
	}
	private State _curState = State.Hide;
	// Use this for initialization
	void Start () {
		_initGndLvl = transform.position.y;
		_velocity = Vector2.zero;
		playerObject = GameObject.Find("klin");
        playerComp = playerObject.GetComponent<Player>();
		_initPosition = transform.position;
		_animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		_timeElapsed += Time.deltaTime;
		//State.Hide waits for an external trigger hence there is no handler for it
		if(_curState == State.Wake)
		{
			if(_timeElapsed > 1.5f)
			{
				_curState = State.Attack;
				_animator.SetTrigger("ToAttack");
				_timeElapsed = 0;
			}
		}
		else if(_curState == State.Idle)
		{
			if(_timeElapsed > 5)
			{
				_curState = State.WalkBack;
			}
		}
		else if(_curState == State.WalkBack)
		{
			float distVec = _initPosition.x - transform.position.x;
			//Lost the player, and since already near spawn point, go back to hide
			if(Mathf.Abs (distVec) < 0.2f)
			{
				transform.position = _initPosition;
				//renderer.material.color = Color.blue;
				_velocity = new Vector2(0, _velocity.y); //Reset the velocity
				_curState = State.Hide;
				_animator.SetTrigger("ToHide");
			}
			else
			{
				_velocity = distVec > 0 ? (new Vector2(0.5f, _velocity.y)) : (new Vector2(-0.5f, _velocity.y));
			}

			if((_facingLeft && _velocity.x > 0) || (!_facingLeft && _velocity.x <= 0))
			{
				changeFacing();
			}
		}
		else if(_curState == State.Attack)
		{
			var distVec = CalDistanceVec();
			float dist = Mathf.Abs(distVec);
			if(dist > 2.0f)
			{
				_curState = State.WalkBack; //Bushy lost interest...
				_animator.SetTrigger("ToIdle");
			}
			else if(dist < 0.4f)
			{
				//renderer.material.color = Color.red;
			}
			else  //Not too far, not too close...
			{
				//renderer.material.color = Color.white;
			}

			//Calculate vertical velocity
			if(_timeElapsed > jumpInterval)
			{
				//Custom "Physics"
				if(dist > 1.0f) //Player has to be far enough for Bushy to make the effort
				{
					_velocity = new Vector2(0, 1.5f);
					ResetToInitGndLvl();
					_isJumping = true;
					_timeElapsed = 0;
				}
			}
			//Calculate horizontal velocity
			float xVelocity = 0;
			float multiplier = distVec < 0 ? -1.0f : 1.0f; //Left or Right?
			if(dist >= 0.4f)
			{
				if(_isJumping)
				{
					xVelocity = multiplier;
				}
				else
				{
					xVelocity = 0.5f * multiplier;
				}
			}
			_velocity = new Vector2(xVelocity, _velocity.y);
			//Change facing
			if((_facingLeft && distVec > 0) || (!_facingLeft && distVec <= 0))
			{
				changeFacing();
			}
		}
		//Updates velocity
		_velocity = new Vector2(_velocity.x, _velocity.y - gravity * Time.deltaTime);
		//Fix the velocity
		if(transform.position.y < _initGndLvl)
		{
			_velocity = new Vector2(_velocity.x, 0);
			ResetToInitGndLvl();
			_isJumping = false;
		}
		//Updates position
		transform.position = new Vector3(transform.position.x + _velocity.x * Time.deltaTime, 
		                                 transform.position.y + _velocity.y * Time.deltaTime, 
		                                 transform.position.z);
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Edge")
		{
			//Force x velocity to 0
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
			_timeElapsed = 0;
			if(_curState != State.Idle)
			{
				_curState = State.Idle;
				_animator.SetTrigger("ToIdle");
			}
		}
        else if (other.tag == "Player")
        {
            playerComp.Damage(dmg);
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
		Vector3 playerPos = playerObject.transform.position;
		Vector3 enemyPos = transform.position;
		float distanceVec = playerPos.x - enemyPos.x;
		return distanceVec;
    }
	public void FoundPlayer()
	{
		if(_curState == State.Hide)
		{
			_curState = State.Wake;
			_timeElapsed = 0;
			_animator.SetTrigger("ToWake");
		}
    }
}
