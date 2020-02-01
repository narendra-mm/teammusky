using UnityEngine;

namespace Experience.ExperienceState
{
	public class DamagePipeState : MonoBehaviour, IExperienceState
	{
		public void DisposeState()
		{
			Debug.Log($"Disposed {this.GetType()}");
		}

		public void EnterState()
		{
            gameObject.SetActive(true);
			Debug.Log($"Entered {this.GetType()}");
		}

		public void ExitState()
		{

			Debug.Log($"Exited {this.GetType()}");
		}

		public void InitializeState(ExperienceStateManager context)
		{
            gameObject.SetActive(false);
			Debug.Log($"Initialised {this.GetType()}");
		}

		public void UpdateState()
		{
		}
	}
}