using SpaceWalk.GameLogic;
using UnityEngine;

namespace GoogleRCS.Experience.ExperienceState
{
	public class FixingPanelState : MonoBehaviour, IExperienceState
	{
        [SerializeField] private Transform _toolsRoot;
        [SerializeField] private Panel _panel;

        private Tool[] _tools;
        public void InitializeState(ExperienceStateManager context)
        {
            Debug.Log($"Initialised {this.GetType()}");
            if(_toolsRoot == null)
            {
                Debug.LogError("Provide root transform for tools.");
            }
            _tools = _toolsRoot.GetComponentsInChildren<Tool>();

            if(_tools == null)
            {
                Debug.LogError("Tools components are missing");
            }

            foreach(var tool in _tools)
            {
                tool.OnToolGrabbed += HandleToolGrabbed;
            }

        }

        public void EnterState()
        {
            Debug.Log($"Entered {this.GetType()}");
        }

        public void UpdateState()
        {
        }

        public void ExitState()
        {
            Debug.Log($"Exited {this.GetType()}");
        }

        public void DisposeState()
		{
			Debug.Log($"Disposed {this.GetType()}");

            foreach (var tool in _tools)
            {
                tool.OnToolGrabbed -= HandleToolGrabbed;
            }
        }

        private void HandleToolGrabbed(ToolType toolType)
        {
            _panel.SetScanMode(toolType);
        }
	}
}