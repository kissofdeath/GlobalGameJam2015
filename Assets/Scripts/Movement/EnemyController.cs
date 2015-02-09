using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {
	public float leftRange;
	public float rightRange;
	public float walkSpeed = 0.5f;
	public float chaseSpeed = 0.7f;
	public float timeOnEdge = 5.0f;
	public float attackRange = 0.4f;
    public float dmg = 20.0f;

	private GameObject playerObject;
	private float _leftBound;
	private float _rightBound;
	private bool _facingLeft = true;
	private bool _edgeHit = false;
	private bool _isLeftEdge;
	private float _timeElapsed = 0;
	private bool _foundPlayer = false;
    private Player playerComp;
	private Animator animator;
	enum State{
		Walk,
		Idle,
		Attack
	};
	private State _curState = State.Walk;
	// Use this for initialization
	void Start () {
		playerObject = GameObject.Find("klin");
		_leftBound = transform.position.x - leftRange;
		_rightBound = transform.position.x + rightRange;
        playerComp = playerObject.GetComponent<Player>();
		animator = GetComponent<Animator>();
	}

	void changeFacing(){
		transform.localScale = new Vector3(- transform.localScale.x, transform.localScale.y, transform.localScale.z);
		_facingLeft = !_facingLeft;
	}

	// Update is called once per frame
	void Update () {
		_timeElapsed += Time.deltaTime;
		if(_foundPlayer && Mathf.Abs(CalDistanceVec().magnitude) <= attackRange)
		{
			if(_curState != State.Attack)
			{
				animator.SetTrigger("ToAttack");
				_curState = State.Attack;
			}
			//renderer.material.color = Color.red;
		}
		else
		{
			animator.ResetTrigger("ToAttack");
			if(_curState == State.Attack)
			{
				animator.SetTrigger("ToWalk");
				_curState = State.Walk;
			}
			//renderer.material.color = Color.white;
		}
		//Only walk if edge is not hit
		if(!_edgeHit)
		{
			if(!_foundPlayer)
			{
				Wander();
			}
			else
			{
				ChasePlayer();
			}
		}
		else
		{
            if(_timeElapsed > timeOnEdge)
			{
				//He gave up on the player cuz he doesn't want to fall
				_foundPlayer = false;
				changeFacing();
				EdgeReposition();
			}
			else if((playerObject.transform.position.x - transform.position.x < 0 && !_isLeftEdge) ||
			        (playerObject.transform.position.x - transform.position.x > 0 && _isLeftEdge))
			{
				EdgeReposition();
            }
		}
	}
	//Move enemy out of the edge in case player triggers the chase again
	void EdgeReposition()
	{
		_edgeHit = false;
		if(_curState != State.Walk)
		{
			animator.SetTrigger("ToWalk");
			_curState = State.Walk;
		}
		if(_isLeftEdge)
		{
			transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y, transform.position.z);
		}
		else
		{
			transform.position = new Vector3(transform.position.x - 0.1f, transform.position.y, transform.position.z);
        }
    }
    
    void Wander(){
        if(_facingLeft)
		{
			transform.position = new Vector3(transform.position.x - Time.deltaTime * walkSpeed, transform.position.y, transform.position.z); 
			if(transform.position.x < _leftBound)
			{
				changeFacing();
				transform.position = new Vector3(_leftBound, transform.position.y, transform.position.z);
			}
		}
		else
		{
			transform.position = new Vector3(transform.position.x + Time.deltaTime * walkSpeed, transform.position.y, transform.position.z); 
			if(transform.position.x > _rightBound)
			{
				changeFacing();
                transform.position = new Vector3(_rightBound, transform.position.y, transform.position.z);
            }
        }
    }
	void ChasePlayer(){
		Vector2 distanceVec = CalDistanceVec();
		if(_facingLeft)
		{
			if(distanceVec.x > 0)
			{
				changeFacing();
			}
		}
		else
		{
			if(distanceVec.x <= 0)
			{
				changeFacing();
			}
		}
		//Coming toward the player (only enough to attack it)
		if(distanceVec.magnitude >= attackRange)
		{
			if(distanceVec.x <= -0.1)
			{
				transform.position = new Vector3(transform.position.x - Time.deltaTime * chaseSpeed, transform.position.y, transform.position.z);
			}
			else if(distanceVec.x > 0.1)
			{
				transform.position = new Vector3(transform.position.x + Time.deltaTime * chaseSpeed, transform.position.y, transform.position.z);
			}
		}
	}
    void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Edge")
		{
			Debug.Log("Hit edge");
			_edgeHit = true;

			if(_curState != State.Idle)
			{
				animator.SetTrigger("ToIdle");
				_curState = State.Idle;
			}

			if(other.transform.position.x < transform.position.x)
			{
				_isLeftEdge  = true;
			}
			else
			{
				_isLeftEdge = false;
			}
			_timeElapsed = 0;
		}
        else if (other.tag == "Player")
        {
            playerComp.Damage(dmg);
        }
	}
	public void FoundPlayer()
	{
		_foundPlayer = true;
	}
	Vector2 CalDistanceVec(){
		Vector3 playerPos = playerObject.transform.position;
		Vector3 enemyPos = transform.position;
		return (new Vector2(playerPos.x - enemyPos.x, playerPos.y - enemyPos.y));
    }
}
