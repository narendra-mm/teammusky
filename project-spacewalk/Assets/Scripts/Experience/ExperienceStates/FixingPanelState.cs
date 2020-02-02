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
        [SerializeField] private OxygenCounter _oxygenCounter;
        [SerializeField] private GameObject _hud;

        [SerializeField] private GameObject[] _patchPanels;
        [SerializeField] private GameObject[] _frames;
        [SerializeField] private GameObject[] _pipes;
        [SerializeField] private GameObject[] _wires;

        // [SerializeField] private Panel _panel;
        private ExperienceStateManager _context;
        private ItemController[] _tools;
        public void InitializeState(ExperienceStateManager context)
        {
            _context = context;
            gameObject.SetActive(false);
            Debug.Log($"Initialised {this.GetType()}");
            if (_toolsRoot == null)
            {
                Debug.LogError("Provide root transform for tools.");
            }
            _tools = _toolsRoot.GetComponentsInChildren<ItemController>();

            _rivetGun.OnRivetPlaced = OnRivetPlaced;
            _oxygenCounter.SetIsRunning(true);
            _hud.active = false;

            GameObject patchPanel = _patchPanels[UnityEngine.Random.Range(0, _patchPanels.Length)];
            GameObject frame = _frames[UnityEngine.Random.Range(0, _frames.Length)];
            GameObject pipes = _pipes[UnityEngine.Random.Range(0, _pipes.Length)];
            GameObject wires = _wires[UnityEngine.Random.Range(0, _wires.Length)];

            patchPanel.active = true;
            frame.active = true;
            pipes.active = true;
            wires.active = true;
            // if(_tools == null)
            // {
            //     Debug.LogError("Tools components are missing");
            // }

            // foreach(var tool in _tools)
            // {
            //     tool.OnToolGrabbed += HandleToolGrabbed;
            // }

        }

        private void OnRivetPlaced(List<ShipMaterial> rivetTypes)
        {

            var containsWire = rivetTypes.Contains(ShipMaterial.Wire);
            var containsPipe = rivetTypes.Contains(ShipMaterial.Pipe);
            if (containsWire)
            {
                GameState.instance.DamagedMaterial = ShipMaterial.Wire;
                _context.TransitionTo<DamageShipState>();
            }
            else if (containsPipe)
            {
                GameState.instance.DamagedMaterial = ShipMaterial.Pipe;
                _context.TransitionTo<DamageShipState>();
            }

        }

        public void EnterState()
        {
            gameObject.SetActive(true);
            // Debug.Log($"Entered {this.GetType()}");
            foreach (var tool in _tools)
            {
                tool.EnableInputs();
            }
            _hud.active = true;
            _rivetGun.EnableInputs = true;
        }

        public void UpdateState()
        {

        }

        public void ExitState()
        {
            foreach (var tool in _tools)
            {
                tool.DisableInputs();
            }
            _hud.active = false;
            _rivetGun.EnableInputs = false;
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