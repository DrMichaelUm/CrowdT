using CrowdT;
using CrowdT.Level;
using Ketchapp.CrowdTerritory;
using KetchappTools.Menus;
using KetchappTools.SimpleFeedbacks;
using Menus;
using TMPro;
using UnityEngine;

namespace Menus
{
	public class VictoryWindow : Menu
	{
		[SerializeField] private TMP_Text levelNumText;
		[SerializeField] private float delayBeforeShow;
		[SerializeField] private ParticleSystem confettiParticles;
		[SerializeField] private Vector3 confettiSpawnPos;
		
		[SerializeField] private GameObject dancingUnits;

		#region Properties
		
		#endregion
		
		#region Menu Methods
		
		// Call at the start of the show animation
		protected override void OnShowStart()
		{
			base.OnShowStart();
		}
		
		// Call at the end of the show animation
		protected override void OnShowEnd()
		{
			base.OnShowEnd();
		}
		
		// Call at the start of the hide animation
		protected override void OnHideStart()
		{
			base.OnHideStart();
		}
		
		// Call at the end of the hide animation
		protected override void OnHideEnd()
		{
			base.OnHideEnd();
		}
		
		#endregion

		private void OnEnable()
		{
			GetComponent<Canvas>().worldCamera = FindObjectOfType<UICamera>().GetComponent<Camera>();
			MenuManager.Instance.MenuList.GameWindow.Hide();
			Instantiate(dancingUnits, Vector3.zero, Quaternion.identity);
			levelNumText.text = $"LEVEL {Save.data.level + 1} COMPLETED";
		}
		
		public async void ShowWithDelay()
		{
			await new WaitForSeconds(delayBeforeShow);
			Show();

			// Instantiate(confettiParticles, GetComponent<RectTransform>().position + confettiSpawnPos, Quaternion.identity, transform);
			
			confettiParticles.gameObject.SetActive(true);/* = 0;*/
			confettiParticles.Play();
			
			Feedbacks.Play("DisplayVictoryWindow", transform.position, Vector3.zero);
		}
		
		public void ClickToContinue()
		{
			LevelsLoader.MoveToNextLevel();
		}

		private void OnDisable()
		{
			confettiParticles.gameObject.SetActive(false);
		}
	}
}
namespace KetchappTools.Menus
{
	public partial class MenuList
	{
		[SerializeField]
		private VictoryWindow victoryWindow;
		public VictoryWindow VictoryWindow => victoryWindow;
	}
}
