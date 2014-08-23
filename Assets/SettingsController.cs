using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
	public enum TimerType
	{
		Main = 0,
		Interval0,
		Interval1
	}

	[SerializeField]
	GameObject presetPrefab;

	[SerializeField]
	Button btnHours0;

	[SerializeField]
	Button btnHours1;

	[SerializeField]
	Button btnMinutes0;
	
	[SerializeField]
	Button btnMinutes1;

	[SerializeField]
	Button btnSeconds0;
	
	[SerializeField]
	Button btnSeconds1;

	[SerializeField]
	Button btnRepetitions;

	[SerializeField]
	Text txtTime;





	[SerializeField]
	Slider slider;

	[SerializeField]
	Button slidetrBg;

	Button TimeButton {get; set;}

	private Transform sliderParent;
	private Vector3 sliderPosition;

	// Use this for initialization
	void Awake ()
	{
		sliderParent = slider.transform.parent;
		sliderPosition = slider.transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		txtTime.text = Application.Instance.Timer.DurationFormatted(false);

	}
	public void OnPresetConfigButtonClick(Button button)
	{
		Application.Instance.Timer.SetPresetConfig(button.name);

		Init ();
	}

	public void OnRepetitionsButtonClick(Button button)
	{
		Text textInButton = button.gameObject.GetComponentInChildren<Text>();

		TimeButton = button; 
		slider.minValue = 1;
		slider.maxValue = 100;
		
		int value = int.Parse(textInButton.text);
		slider.value = value;

		slider.gameObject.SetActive(true);
		Vector3 pos = button.transform.position + new Vector3(button.GetComponent<RectTransform>().rect.width,  0, 0);
		pos.y = slider.transform.position.y;
		slider.transform.position = pos;
	}

	public void OnButtonHourClick(Button button)
	{
		Text textInButton = button.gameObject.GetComponentInChildren<Text>();

		TimeButton = button; 
		slider.minValue = 0;
		slider.maxValue = 24;
		
		int value = int.Parse(textInButton.text);
		slider.value = value;

		ShowSlider(button, true);
	}

	public void OnButtonMinutesClick(Button button)
	{
		Text textInButton = button.gameObject.GetComponentInChildren<Text>();
		
		TimeButton = button; 
		slider.minValue = 0;
		slider.maxValue = 59;
		
		int value = int.Parse(textInButton.text);
		slider.value = value;

		ShowSlider(button, true);
	}

	public void OnButtonSecondsClick(Button button)
	{
		Text textInButton = button.gameObject.GetComponentInChildren<Text>();
		
		TimeButton = button; 
		slider.minValue = 0;
		slider.maxValue = 59;
		
		int value = int.Parse(textInButton.text);
		slider.value = value;

		ShowSlider(button, false);
	}

	
	public void Init()
	{
		btnHours0.GetComponentInChildren<Text>().text = Application.Instance.Timer.Cfg.Intervals[0].hours.ToString();
		btnHours1.GetComponentInChildren<Text>().text = Application.Instance.Timer.Cfg.Intervals[1].hours.ToString();
		btnMinutes0.GetComponentInChildren<Text>().text = Application.Instance.Timer.Cfg.Intervals[0].minutes.ToString();
		btnMinutes1.GetComponentInChildren<Text>().text = Application.Instance.Timer.Cfg.Intervals[1].minutes.ToString();
		btnSeconds0.GetComponentInChildren<Text>().text = Application.Instance.Timer.Cfg.Intervals[0].seconds.ToString();
		btnSeconds1.GetComponentInChildren<Text>().text = Application.Instance.Timer.Cfg.Intervals[1].seconds.ToString();
		btnRepetitions.GetComponentInChildren<Text>().text = Application.Instance.Timer.Cfg.RepetitionCount.ToString();
	
		slider.transform.position = sliderPosition;
		slider.transform.parent = sliderParent;
		HideSlider();
	}



	private void ShowSlider(Button button, bool right)
	{
		slider.gameObject.SetActive(true);
		slidetrBg.gameObject.SetActive(true);

		Vector3 offset = new Vector3(button.GetComponent<RectTransform>().rect.width,  0, 0);
		Vector3 pos = button.transform.position + (right ? offset : -offset);

		pos.y = slider.transform.position.y;
		slider.transform.position = pos;
	}


	public void HideSlider()
	{
		slider.gameObject.SetActive(false);
		slidetrBg.gameObject.SetActive(false);
	}

	public void OnSliderValueChanged(Slider slider)
	{
		if (TimeButton != null)
		{
			int value = (int)slider.value;

			TimeButton.gameObject.GetComponentInChildren<Text>().text = value.ToString();
	
			//hours
			if (TimeButton.name == "BtnHours_Main")
			{
				
			}
			else if (TimeButton.name == "BtnHours_0")
			{
				Application.Instance.Timer.Cfg.Intervals[0].hours = value;
			}
			else if (TimeButton.name == "BtnHours_1")
			{
				Application.Instance.Timer.Cfg.Intervals[1].hours = value;
			}


			//minutes
			if (TimeButton.name == "BtnMinutes_Main")
			{
				
			}
			else if (TimeButton.name == "BtnMinutes_0")
			{
				Application.Instance.Timer.Cfg.Intervals[0].minutes = value;
			}
			else if (TimeButton.name == "BtnMinutes_1")
			{
				Application.Instance.Timer.Cfg.Intervals[1].minutes = value;
			}

			//seconds
			if (TimeButton.name == "BtnSeconds_Main")
			{
				
			}
			else if (TimeButton.name == "BtnSeconds_0")
			{
				Application.Instance.Timer.Cfg.Intervals[0].seconds = value;
			}
			else if (TimeButton.name == "BtnSeconds_1")
			{
				Application.Instance.Timer.Cfg.Intervals[1].seconds = value;
			}


			// repetitions
			if (TimeButton.name == "BtnRepetitions")
			{
				Application.Instance.Timer.Cfg.RepetitionCount = value;
			}


		}
	}

	void OnEnable()
	{
		Init();
	}

	public void OnBack()
	{

		Application.Instance.ShowTimerPage();
	}


}
