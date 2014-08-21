using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Application : MonoBehaviour 
{
	[SerializeField]
	RectTransform pnlTimer;

	[SerializeField]
	RectTransform pnlSettings;

	[SerializeField]
	RectTransform pnlWorkout;

	[SerializeField]
	RectTransform pnlMusic;

	[SerializeField]
	TimerVisualController TimerController;


	[SerializeField]
	public ExcerciseDb ExcerciseDb;


	[SerializeField]
	public MusicDb MusicDb;

	[SerializeField]
	public AudioManager AudioManager;


	[SerializeField]
	public Timer Timer;

	public static Application Instance { get { return _instance; } }

	private static Application _instance = null;

	void Awake()
	{
		_instance = this;

		// run in 20 fps
		UnityEngine.Application.targetFrameRate = 20;
		UnityEngine.Application.runInBackground = true;

	}


	void Start () 
	{
		TimerController.TimerRef = Timer;

		Timer.Reset();

		Timer.IntervalTick += AudioManager.OnTick;
		Timer.IntervalStarted += AudioManager.OnIntervalStarted;
		Timer.TimerStarted += AudioManager.OnTimerStarted;
		Timer.TimerEnded += AudioManager.OnTimerEnded;
		Timer.TimerPause += AudioManager.OnTimerPause;
		Timer.TimerUnPause += AudioManager.OnTimerUnPause;
		Timer.TimerReset += AudioManager.OnTimerReset;


		Timer.TimerStarted += TimerController.OnTimerStarted;
		Timer.TimerEnded += TimerController.OnTimerEnded;
		Timer.TimerPause += TimerController.OnTimerPause;
		Timer.TimerUnPause += TimerController.OnTimerUnPause;
		Timer.TimerReset += TimerController.OnTimerReset;

		string dataPath = UnityEngine.Application.dataPath;
	

		Debug.Log(dataPath);
	}
	
	void Update () 
	{
	}


	public void PauseTimer()
	{
		if (Timer.CurrentState == Timer.State.Running)
		{
			Timer.Pause();
		}
	}

	public void Stop()
	{
		Screen.sleepTimeout = SleepTimeout.SystemSetting;
		Timer.Reset();
	}


	public void Play()
	{
		if (Timer.CurrentState == Timer.State.Stopped)
		{
			Screen.sleepTimeout = SleepTimeout.NeverSleep;
			Timer.Run();
		}
		else if (Timer.CurrentState == Timer.State.Paused)
		{
			Screen.sleepTimeout = SleepTimeout.NeverSleep;
			Timer.UnPause();
		}
	}



	public void ShowSettingsPage()
	{
		pnlTimer.gameObject.SetActive(false);
		pnlSettings.gameObject.SetActive(true);
		pnlSettings.GetComponent<SettingsController>().Init();
	}

	public void ShowTimerPage()
	{
		pnlTimer.gameObject.SetActive(true);
		pnlSettings.gameObject.SetActive(false);
	}

	public void ShowPageWorkout()
	{
		pnlTimer.gameObject.SetActive(false);
		pnlSettings.gameObject.SetActive(false);
		pnlWorkout.gameObject.SetActive(true);
	}

	public void ShowPageMusic()
	{
		pnlTimer.gameObject.SetActive(false);
		pnlSettings.gameObject.SetActive(false);
		pnlWorkout.gameObject.SetActive(false);
		pnlMusic.gameObject.SetActive(true);
	}



}
