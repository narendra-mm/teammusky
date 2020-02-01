using UnityEngine;

namespace GoogleRCS.Experience.ExperienceState
{
	public class FixingPanelState : MonoBehaviour, IExperienceState
	{
		public void DisposeState()
		{
			Debug.Log($"Disposed {this.GetType()}");
		}

		public void EnterState()
		{
			Debug.Log($"Entered {this.GetType()}");
		}

		public void ExitState()
		{
			Debug.Log($"Exited {this.GetType()}");
		}

		public void InitializeState(ExperienceStateManager context)
		{
			Debug.Log($"Initialised {this.GetType()}");
		}

		public void UpdateState()
		{
		}
	}
}