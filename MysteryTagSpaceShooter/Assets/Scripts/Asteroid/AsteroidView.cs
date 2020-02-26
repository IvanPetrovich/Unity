using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class AsteroidView : MonoBehaviour
{
	public SpriteRenderer _spriteRenderer = null;
	private bool _destroyed = false;
	public ReactiveProperty<bool> destroyComplete {get; set;}
	private Vector3 _scale;
	
	void Awake()
	{
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_scale = transform.localScale;
		destroyComplete = new ReactiveProperty<bool>(false);
		Initialize();
	}
	
	public void Initialize()
	{
		_spriteRenderer.enabled = true;
		_destroyed = false;
		destroyComplete.Value = false;
		transform.localScale = _scale;
		_spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, 1.0f);
	}
	
	public void SetVisible(bool val)
	{
		_spriteRenderer.enabled = val;
	}

	public void DestroyIt()
	{
		_destroyed = true;
	}
	
	void Update()
	{
		if(_destroyed && _spriteRenderer.color.a > 0.2f)
		{
			transform.localScale = new Vector3(transform.localScale.x * 1.05f, transform.localScale.y * 1.05f, transform.localScale.z * 1.05f);
			_spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, _spriteRenderer.color.a - Time.deltaTime * 5);
		}else
		if(_destroyed)
		{
			SetVisible(false);
			destroyComplete.Value = true;
		}
	}
	
}
