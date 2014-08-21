using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class TimerVisualController : MonoBehaviour {

	[SerializeField]
	Text lblTimeElapsed;

	[SerializeField]
	Text lblTimeDuration;

	[SerializeField]
	Text lblTimeRest;

	[SerializeField]
	Text lblIntervalDuration;

	[SerializeField]
	Text lblIntervalElapsed;

	[SerializeField]
	Text lblIntervalRest;

	[SerializeField]
	Button btnPlay;

	[SerializeField]
	Button btnPause;


	[SerializeField]
	Text txtRound;

	[SerializeField]
	Text txtRoundCount;

	[SerializeField]
	ProgressBar progBarMainTimer;


	[SerializeField]
	ProgressBar progBarIntervalTimer;

	[SerializeField]
	Image imgIntervalImage;


	private Color InactiveIntervalImageColor;

	public Timer TimerRef { get; set; }

	void Start ()
	{
		InactiveIntervalImageColor = imgIntervalImage.color;
	}
	
	void Update () 
	{
		if (TimerRef != null)
		{
			lblIntervalDuration.text = TimerRef.CurrentIntervalDurationFormatted(false);
			lblIntervalElapsed.text = TimerRef.CurrentIntervalElapsedTimeFormatted(true);
			lblIntervalRest.text = TimerRef.CurrentIntervalRestTimeFormatted(true);

			lblTimeElapsed.text = TimerRef.ElapsedTimeFormatted(false);
			lblTimeDuration.text = TimerRef.DurationFormatted(false);
			lblTimeRest.text = TimerRef.RestTimeFormatted(false);

			txtRound.text = TimerRef.CurrentRound().ToString();
			txtRoundCount.text = TimerRef.RoundCount().ToString();

			progBarMainTimer.Set (1 - TimerRef.ElapsedTime() / TimerRef.Duration());
			progBarIntervalTimer.Set (1 - TimerRef.CurrentIntervalElapsedTime() / TimerRef.CurrentIntervalDuration());

			if (TimerRef.CurrentState == Timer.State.Running)
			{
				imgIntervalImage.color = TimerRef.Cfg.Intervals[TimerRef.CurrentIntervalState.Index].Color;
			}
			else
			{
				imgIntervalImage.color = InactiveIntervalImageColor;
			}
		}
	}

	public void OnTimerStarted(Timer.Config config)
	{
		btnPlay.gameObject.SetActive(false);
		btnPause.gameObject.SetActive(true);
	}
	
	public void OnTimerEnded()
	{
		btnPlay.gameObject.SetActive(true);
		btnPause.gameObject.SetActive(false);
	}
	
	public void OnTimerPause(Timer.IntervalDefinition intervalDef)
	{
		btnPlay.gameObject.SetActive(true);
		btnPause.gameObject.SetActive(false);
	}
	
	public void OnTimerUnPause(Timer.IntervalDefinition intervalDef)
	{
		btnPlay.gameObject.SetActive(false);
		btnPause.gameObject.SetActive(true);
	}
	
	public void OnTimerReset()
	{
		btnPlay.gameObject.SetActive(true);
		btnPause.gameObject.SetActive(false);
	}

}
