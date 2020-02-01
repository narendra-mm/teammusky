using UnityEngine;

namespace Experience.ExperienceState
{
	[RequireComponent(typeof(ExperienceStateManager))]
	public class ToolPickupExperienceController : MonoBehaviour
	{
		private ExperienceStateManager _experienceStateManager;

		// Start is called before the first frame update
		void Start()
		{
			_experienceStateManager = GetComponent<ExperienceStateManager>();
			var states = transform.GetComponentsInChildren<IExperienceState>();
			_experienceStateManager.Setup(states);
            _experienceStateManager.TransitionTo<FixingPanelState>();
			Debug.Log($"Current state is {_experienceStateManager.CurrentState.GetType()}");
		}
	}
}