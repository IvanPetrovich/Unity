using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class Player : NetworkBehaviour, IDamageable
{
	[SerializeField]
	private int _health;
	[SerializeField]
	private GameObject _snowBall;
	[SerializeField]
	private GameObject _speedHat;
	[SerializeField]
	private GameObject _armorBody;
	[SerializeField]
	private GameObject _armorPant;
	[SerializeField]
	private Transform _snowBallPosition;
 	private GameLogic _gameLogic;
	private GameMenuLogic _gameML;
	private CameraController mainCamCntr;
	private Network _network;
	private bool _speedUp = false;
	private float _speedUpValue = 1.5f;
	private bool _hasArmor = false;
	private Rigidbody2D _rigidbody2D;
	private float _moveSpeed = 4f;
	private float _jumpForce = 5f;
	private bool _lookOnTheRight = false;
	private PlayerAnimation _playerAnim;
	private bool _canHit = true;

	List<Collider2D> PlatformColliders = new List<Collider2D>();
	
	
	public int Health{get; set;}
    void Start(){
		_network = FindObjectOfType<Network>();
		_playerAnim = GetComponent<PlayerAnimation>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
		Health = _health;
		_gameLogic = FindObjectOfType<GameLogic>();
		_gameML = FindObjectOfType<GameMenuLogic>();
		mainCamCntr = FindObjectOfType<CameraController>();
		if(isLocalPlayer){
			mainCamCntr.SetTarget(transform);
		}
		
		SetArmor(false);
		SetSpeedUp(false);
    }

    void Update(){
		if(!isLocalPlayer || _gameML.IsPause()){
			return;
		}
		if(Health > 0){
			Movement();
			
			if (Input.GetButtonDown("Fire1")){
				_playerAnim.Attack();
			}
			
			if (Input.GetButtonDown("Fire2")){
				_playerAnim.Fire();
			}
		}	
    }

	public void SetSpeedUp(bool val){
		_speedUp = val;
		_speedHat.SetActive(val);
	}

	public void SetArmor(bool val){
		_hasArmor = val;
		_armorBody.SetActive(val);
		_armorPant.SetActive(val);
	}
	
	void OnCollisionStay2D(Collision2D other){
		if(other.gameObject.CompareTag("Platform")){
			if(!PlatformColliders.Contains(other.collider)){
				PlatformColliders.Add(other.collider);
			}
		}
	}
	
	void OnCollisionExit2D(Collision2D other){
		if (PlatformColliders.Contains(other.collider)){
            PlatformColliders.Remove(other.collider);
		}
	}
	
	void Movement(){
		
		float move = Input.GetAxisRaw("Horizontal");
		float moveVertical = Input.GetAxisRaw("Vertical");
		
		float _addSpeedJump = 1f;
		if(_speedUp){
			_addSpeedJump = _speedUpValue;
		}
		
		_playerAnim.Move(move);
		_rigidbody2D.velocity = new Vector2(move * _moveSpeed * _addSpeedJump,_rigidbody2D.velocity.y);
		
		if(move < 0 && _lookOnTheRight){
			Flip();
		}
		if(move > 0 && !_lookOnTheRight){
			Flip();
		}
		
		Debug.DrawRay(new Vector2(transform.position.x, transform.position.y-0.75f), Vector2.down * 0.25f, Color.green);
		
		
		if(Input.GetKeyDown(KeyCode.Space) && IsGrounded() && moveVertical >= 0){
			_rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x,_jumpForce * _addSpeedJump);
		}
		
		if(Input.GetKeyDown(KeyCode.Space) && IsGrounded() && moveVertical < 0){
			foreach (Collider2D coll in PlatformColliders){
				Physics2D.IgnoreCollision(coll, GetComponent<Collider2D>(),true);
				StartCoroutine(ReturnPlatformCollisionRoutine(coll));
			}
		}
	}

	IEnumerator ReturnPlatformCollisionRoutine(Collider2D coll){
		yield return new WaitForSeconds(0.5f);
		Physics2D.IgnoreCollision(coll, GetComponent<Collider2D>(),false);
	}
	
	void Flip(){
		
		_lookOnTheRight = !_lookOnTheRight;
		transform.localScale = new Vector3(-transform.localScale.x,transform.localScale.y,transform.localScale.z);
		CmdFlip(transform.localScale.x);
	}
	
	bool IsGrounded(){
		RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y-0.75f), Vector2.down, 0.25f,1<<8);	
		if (hit.collider != null){
			return true;
		}
		return false;
	}
	
	public void Damage(GameObject author){
		if(_canHit){
			if(_hasArmor){
				SetArmor(false);
			}else{
				if(_speedUp){
					SetSpeedUp(false);
				}
				Health--;
			}	
			_canHit = false;
			StartCoroutine(CanHitResetRoutine());
		}
		if(Health<1){
			_playerAnim.Die();
			Destroy(this.gameObject,4f);
			StartCoroutine(GoToMainMenuRoutine());
		}
	}
	
	public void Fire(){
		GameObject snowBall;
		snowBall = (GameObject)Instantiate(_snowBall, _snowBallPosition.transform.position, Quaternion.identity);
		SnowBall sb = snowBall.GetComponent<SnowBall>();
		sb.SetVelocity(_rigidbody2D.velocity);
		sb.SetDirection(transform.localScale.x);
		sb.SetOwner(transform.gameObject);
	}
	
	IEnumerator CanHitResetRoutine(){
		yield return new WaitForSeconds(0.5f);
		_canHit = true;
	}

	IEnumerator GoToMainMenuRoutine(){
		yield return new WaitForSeconds(3f);
		Application.LoadLevel(0);
	}
	
	[Command]
	public void CmdFlip(float localScaleX){
		RpcFlip(localScaleX);
	}
	
	[ClientRpc]
	void RpcFlip(float licalScalseX){
		transform.localScale = new Vector3(licalScalseX,transform.localScale.y,transform.localScale.z);
	}
		
}
