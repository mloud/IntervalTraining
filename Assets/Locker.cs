using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Locker : MonoBehaviour 
{
	[SerializeField]
	Image imgCenter;

	[SerializeField]
	Image imgLock;

	[SerializeField]
	Image imgLockIcon;

	[SerializeField]
	Image imgLockBg;

	[SerializeField]
	Button btnTouchBlocker;


	private enum State
	{
		Disabled,
		TapAndHold,
		Unlocking
	}

	private State CurrentState { get; set; }

	private class LockerTouch
	{
		public Vector3 touchBeganPos;
		public Vector3 selectedUIOriginPos;
		public Image selectedUI;
		public float time;
	}





	private LockerTouch ActiveTouch { get; set; }

	private bool Locked { get; set; }

	void Awake()
	{
		ActiveTouch = null;

		Unlock();

		SetState(State.Disabled);
	}


	void Update()
	{
		UpdateTouch();
	}



	void Lock()
	{
		Locked = true;

		btnTouchBlocker.gameObject.SetActive(true);
		imgLockIcon.gameObject.SetActive(true);
	}

	void Unlock()
	{
		Locked = false;

		btnTouchBlocker.gameObject.SetActive(false);
		imgLockIcon.gameObject.SetActive(false);
	}

	void ToggleLock()
	{
		if (Locked)
			Unlock();
		else 
			Lock();
	}

	void SetState(State state)
	{
		switch(state)
		{
		case State.Disabled:
			imgCenter.gameObject.SetActive(false);
			imgLock.gameObject.SetActive(false);
			imgLockBg.enabled = false;
			break;
		
		case State.TapAndHold:
			imgCenter.gameObject.SetActive(true);
			imgLock.gameObject.SetActive(true);
			imgLockBg.enabled = true;
			break;

		case State.Unlocking:
			imgCenter.gameObject.SetActive(true);
			imgLock.gameObject.SetActive(true);
			imgLockBg.enabled = true;
			break;
		}

		CurrentState = state;
	}


	private bool IsPositionInside(Image image, Vector3 pos)
	{
		Vector3 center = image.transform.position;
		Vector3 size = new Vector3(image.rectTransform.rect.width, image.rectTransform.rect.height, 0);

		return  pos.x > (center.x - size.x * 0.5f) &&
				pos.x < (center.x + size.x * 0.5f) &&
				pos.y > (center.y - size.y * 0.5f) &&
				pos.y < (center.y + size.y * 0.5f);
	}


	void OnMouseButtonDown(Vector3 pos)
	{
		bool insideTapAndHoldArea = IsPositionInside(imgCenter, pos);
		
		if (insideTapAndHoldArea)
		{
			ActiveTouch = new LockerTouch(); 
			ActiveTouch.touchBeganPos = pos;
			ActiveTouch.time = Time.time;
			
			SetState(State.Unlocking);
			
			ActiveTouch.selectedUI = imgCenter;
			ActiveTouch.selectedUIOriginPos = imgCenter.transform.position;
		}
	}

	void OnMouseButtonUp(Vector3 pos)
	{
		if (CurrentState == State.Unlocking)
		{
			if (IsPositionInside(imgLock, pos))
			{
				ToggleLock();
			}
			
			ActiveTouch.selectedUI.transform.position = ActiveTouch.selectedUIOriginPos;
		}
		ActiveTouch = null;
		
		SetState(State.Disabled);
	}


	void UpdateTouch()
	{

		if (ActiveTouch != null)
		{
			if (CurrentState == State.TapAndHold && (Time.time > (ActiveTouch.time + 0.1f)))
			{
				SetState(State.Unlocking);
			}
		}

#if UNITY_EDITOR
		//if (Locked)
		{
			if (Input.GetMouseButtonDown(0))
			{
				OnMouseButtonDown(Input.mousePosition);
			}
			else if (Input.GetMouseButtonUp(0))
			{
				OnMouseButtonUp(Input.mousePosition);
			}
			else
			{
				if (CurrentState == State.Unlocking)
				{
					ActiveTouch.selectedUI.transform.position = Input.mousePosition;
				}
			}
		}
#else
		if (Input.touchCount > 0) 
		{
			Touch touch = Input.GetTouch(0);

			if (touch.phase == TouchPhase.Began) 
			{
				OnMouseButtonDown(new Vector3(touch.position.x,touch.position.y, 0));
			}
			else if (touch.phase == TouchPhase.Ended)
			{
				OnMouseButtonUp(new Vector3(touch.position.x,touch.position.y, 0));
			}
			else
			{
				if (CurrentState == State.Unlocking)
				{
					ActiveTouch.selectedUI.transform.position = Input.mousePosition;
				}
			}
		}
#endif
		
	}


}
