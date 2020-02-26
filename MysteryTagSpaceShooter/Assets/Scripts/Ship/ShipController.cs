using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ShipController : MonoBehaviour
{
	public ShipModel shipModel;
	public ShipView shipView;
	
	private Rigidbody2D _rb2D;

	private void Awake()
	{
		_rb2D = GetComponent<Rigidbody2D>();
		shipModel = new ShipModel();
	}
	void Start()
	{
		shipModel.Initialize();

		Observable.EveryUpdate()
			.Where(_ => Input.GetKey(KeyCode.UpArrow)
						||Input.GetKey(KeyCode.DownArrow)
						||Input.GetKey(KeyCode.LeftArrow)
						||Input.GetKey(KeyCode.RightArrow)
						)
			.Subscribe(_ => {
				Moving();
			}
	
			).AddTo(this);
			
		Observable.EveryUpdate()
			.Where(_ => Input.GetKey(KeyCode.Space)
						)
			.Subscribe(_ => {
				Shot();
			}
	
			).AddTo(this);
			
		shipModel.position
		.ObserveEveryValueChanged(x => x.Value)
		.Subscribe(xs => {
			SetPosition(xs);
		}).AddTo(this);	
			
		shipModel.health
		.ObserveEveryValueChanged(x => x.Value)
		.Subscribe(xs => {
			OnHealthChange(xs);
		}).AddTo(this);	
			
			
	}
	
	private void Shot()
	{
		Debug.Log("Piu");
	}
	
	private void SetPosition(Vector3 val)
	{
		transform.position = val;
	}
	
	private void OnHealthChange(int val)
	{
		if(val <= 0)
		{
			Debug.Log("GameOver");
			shipView.Death();
		}
	}
	
	private void Moving()
	{
		
		Vector3 temp = Vector3.zero;//shipModel.position.Value;
		
		if (Input.GetKey(KeyCode.UpArrow))
		{
			temp = new Vector3(temp.x,temp.y + 1f, temp.z);
		}	

		if (Input.GetKey(KeyCode.DownArrow))
		{
			temp = new Vector3(temp.x,temp.y - 1f, temp.z);
		}	

		if (Input.GetKey(KeyCode.LeftArrow))
		{
			temp = new Vector3(temp.x - 1f,temp.y, temp.z);
		}	

		if (Input.GetKey(KeyCode.RightArrow))
		{
			temp = new Vector3(temp.x + 1f,temp.y, temp.z);
		}	

		temp = Vector3.Normalize(temp) * shipModel.speed.Value * Time.deltaTime;
		
		float newPosX = shipModel.position.Value.x + temp.x;
		float newPosY = shipModel.position.Value.y + temp.y;
		float newPosZ = shipModel.position.Value.z + temp.z;
		
		newPosX = Mathf.Clamp(newPosX,-6,6);
		newPosY = Mathf.Clamp(newPosY,-4,4);

		shipModel.position.Value = new Vector3(newPosX, newPosY, newPosZ);

	}
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Enemy"))
		{	
			shipModel.DealDamage(1);
		}	
	}
	
}
