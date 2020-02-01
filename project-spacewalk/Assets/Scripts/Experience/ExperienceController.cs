using Experience.ExperienceState;
using UnityEngine;

namespace Experience
{
	[RequireComponent(typeof(ExperienceStateManager))]
	public class ExperienceController : MonoBehaviour
	{
		private ExperienceStateManager _experienceStateManager;

		// Start is called before the first frame update
		void Start()
		{
			_experienceStateManager = GetComponent<ExperienceStateManager>();
			var states = transform.GetComponentsInChildren<IExperienceState>();
			_experienceStateManager.Setup(states);
            _experienceStateManager.TransitionTo<IntroState>();
			Debug.Log($"Current state is {_experienceStateManager.CurrentState.GetType()}");
		}
	}
}