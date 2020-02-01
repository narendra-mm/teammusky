using SpaceWalk.GameLogic;
using UnityEngine;

namespace Experience.ExperienceState
{
	public class IntroState : MonoBehaviour, IExperienceState
	{
        private ExperienceStateManager _context;
        public void InitializeState(ExperienceStateManager context)
        {
			gameObject.SetActive(false);
            _context = context;
            Debug.Log($"Initialised {this.GetType()}");
        }

        public void EnterState()
        {
			gameObject.SetActive(true);
            Debug.Log($"Entered {this.GetType()}");
        }

        public void UpdateState()
        {
            
        }

        public void ExitState()
        {
			gameObject.SetActive(false);
            Debug.Log($"Exited {this.GetType()}");
        }

        public void DisposeState()
		{
			Debug.Log($"Disposed {this.GetType()}");
        }
        public void TimelineEnded()
        {
            _context.TransitionToNext();
        }
	}
}