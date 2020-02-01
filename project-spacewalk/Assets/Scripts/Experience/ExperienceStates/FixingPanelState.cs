using SpaceWalk.GameLogic;
using UnityEngine;
using System.Collections.Generic;
using System;

namespace Experience.ExperienceState
{
	public class FixingPanelState : MonoBehaviour, IExperienceState
	{
        [SerializeField] private Transform _toolsRoot;
        [SerializeField] private RivetGun _rivetGun;
        // [SerializeField] private Panel _panel;

        private ItemController[] _tools;
        public void InitializeState(ExperienceStateManager context)
        {
			gameObject.SetActive(false);
            Debug.Log($"Initialised {this.GetType()}");
            if(_toolsRoot == null)
            {
                Debug.LogError("Provide root transform for tools.");
            }
            _tools = _toolsRoot.GetComponentsInChildren<ItemController>();

            _rivetGun.OnRivetPlaced = OnRivetPlaced;
            // if(_tools == null)
            // {
            //     Debug.LogError("Tools components are missing");
            // }

            // foreach(var tool in _tools)
            // {
            //     tool.OnToolGrabbed += HandleToolGrabbed;
            // }

        }

        private void OnRivetPlaced(List<String> rivetTypes) {
            Debug.Log(rivetTypes[0]);
        }

        public void EnterState()
        {
			gameObject.SetActive(true);
            // Debug.Log($"Entered {this.GetType()}");
            foreach (var tool in _tools) {
                tool.EnableInputs();
            }
        }

        public void UpdateState()
        {
            
        }

        public void ExitState()
        {
            foreach (var tool in _tools) {
                tool.DisableInputs();
            }
            // Debug.Log($"Exited {this.GetType()}");
        }

        public void DisposeState()
		{
			// Debug.Log($"Disposed {this.GetType()}");

            // foreach (var tool in _tools)
            // {
            //     tool.OnToolGrabbed -= HandleToolGrabbed;
            // }
        }

        private void HandleToolGrabbed(ToolType toolType)
        {
            // _panel.SetScanMode(toolType);
        }
	}
}