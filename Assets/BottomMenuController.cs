using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class BottomMenuController : MonoBehaviour 
{
	public enum PanelType
	{
		Timer = 0,
		Settings,
		Workout,
		Music
	}

	[System.Serializable]
	class Panel
	{
		public PanelType Type;
		public Button UIButton;
		public RectTransform UIPanel;
	}

	[SerializeField]
	List<Panel> Panels;


	public void OnButtonClick(Button button)
	{
		for (int i = 0; i < Panels.Count; ++i)
		{
			if (Panels[i].UIButton == button)
			{
				ShowPanel(i);
				break;
			}
		}
	}


	private void ShowPanel(int index)
	{
		for (int i = 0; i < Panels.Count; ++i)
		{
			Panels[i].UIPanel.gameObject.SetActive(i == index);
		}
	}


}
