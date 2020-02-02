using UnityEngine;
using UnityEngine.UI;

namespace Experience.ExperienceState
{
    public class EndState : MonoBehaviour, IExperienceState
    {
        [SerializeField] private Button _startButton;
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

        private void OnStartClicked()
        {
        }
    }
}