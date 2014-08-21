using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour 
{
	[SerializeField]
	Scrollbar scrollBar;

	void Start () 
	{
		//scrollBar.enabled = false;
	}
	
	void Update () 
	{}

	//0-1
	public void Set(float value)
	{
		scrollBar.size = value;
		//imgFill.rectTransform.localScale = new Vector3(fillFullScale.x * value, fillFullScale.y, fillFullScale.z);
	}
	
	//0-100
	public void Set(int value)
	{
		Set (value / 100.0f);
	}
}
