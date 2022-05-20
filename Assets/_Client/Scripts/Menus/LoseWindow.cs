using CrowdT;
using CrowdT.Level;
using Ketchapp.CrowdTerritory;
using KetchappTools.Menus;
using KetchappTools.SimpleFeedbacks;
using Menus;
using UnityEngine;

namespace Menus
{
	public class LoseWindow : Menu
	{
		#region Properties

		[SerializeField] private float delayBeforeShow;
		[SerializeField] private GameObject dancingUnits;

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
			Instantiate(dancingUnits, Vector3.zero, Quaternion.identity);
		}
		
		public async void ShowWithDelay()
		{
			await new WaitForSeconds(delayBeforeShow);
			Show();
			
			Feedbacks.Play("DisplayLoseWindow", transform.position, Vector3.zero);
		}
		
		public void ClickToContinue()
		{
			LevelsLoader.RestartLevel();
		}
	}
}
namespace KetchappTools.Menus
{
	public partial class MenuList
	{
		[SerializeField]
		private LoseWindow loseWindow;
		public LoseWindow LoseWindow => loseWindow;
	}
}
