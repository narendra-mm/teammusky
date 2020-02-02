using Experience.ExperienceState;
using System.Collections.Generic;
using UnityEngine;

namespace Experience
{
	[RequireComponent(typeof(ExperienceStateManager))]
	public class ExperienceController : MonoBehaviour
	{
		public enum States
		{
			Intro,
			Fixing,
            DamageShip
		}

		public States DefaultState = States.Intro;

		private ExperienceStateManager _experienceStateManager;

		// Start is called before the first frame update
		void Start()
		{
			_experienceStateManager = GetComponent<ExperienceStateManager>();
			var states = transform.GetComponentsInChildren<IExperienceState>();
			_experienceStateManager.Setup(states);

			switch (DefaultState)
			{
					case States.Intro:
						_experienceStateManager.TransitionTo<IntroState>();
						break;
					case States.Fixing:
						_experienceStateManager.TransitionTo<FixingPanelState>();
					break;
                case States.DamageShip:
                    _experienceStateManager.TransitionTo<DamageShipState>();
                    break;
            }



			Debug.Log($"Current state is {_experienceStateManager.CurrentState.GetType()}");
		}
	}
}