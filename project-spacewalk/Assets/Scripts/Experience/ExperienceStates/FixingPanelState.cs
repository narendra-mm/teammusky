using SpaceWalk.GameLogic;
using UnityEngine;
using System.Collections.Generic;
using System;

namespace Experience.ExperienceState
{
    public class FixingPanelState : MonoBehaviour, IExperienceState
    {
        [SerializeField] private Transform _camera;
        [SerializeField] private Transform _toolsRoot;
        [SerializeField] private RivetGun _rivetGun;
        [SerializeField] private OxygenCounter _oxygenCounter;
        [SerializeField] private GameObject _hud;
        [SerializeField] private ArmControllerTight _arm;
        [SerializeField] private HandAnimator _hand;

        [SerializeField] private GameObject[] _patchPanels;
        [SerializeField] private GameObject[] _frames;
        [SerializeField] private GameObject[] _pipes;
        [SerializeField] private GameObject[] _wires;

        // [SerializeField] private Panel _panel;
        private ExperienceStateManager _context;
        private GrabbableItem[] _tools;
        public void InitializeState(ExperienceStateManager context)
        {
            _context = context;
            gameObject.SetActive(false);
            Debug.Log($"Initialised {this.GetType()}");
            if (_toolsRoot == null)
            {
                Debug.LogError("Provide root transform for tools.");
            }
            _tools = _toolsRoot.GetComponentsInChildren<GrabbableItem>();

            _rivetGun.OnRivetPlaced = OnRivetPlaced;
            _oxygenCounter.OnOxygenDepleted = OnOxygenDepleted;
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

        private int health = 3;
        private void OnRivetPlaced(List<ShipMaterial> rivetTypes)
        {

            var containsWire = rivetTypes.Contains(ShipMaterial.Wire);
            var containsPipe = rivetTypes.Contains(ShipMaterial.Pipe);
            if (containsPipe)
            {
                health--;
                GameState.instance.DamagedMaterial = ShipMaterial.Pipe;
                _context.TransitionTo<DamageShipState>();
                _oxygenCounter.AddOxygenLossPerSecond(1.5f);
            }
            else if (containsWire)
            {
                health--;
                GameState.instance.DamagedMaterial = ShipMaterial.Wire;
                _context.TransitionTo<DamageShipState>();
            }
            if (health <= 0) {
                GameState.instance.EndStory = EndGameType.ExplodeShip;
                _context.TransitionTo<EndState>();
            }
        }

        private void OnOxygenDepleted() {
            // GameState.instance.DamagedMaterial = ShipMaterial.Pipe;
            GameState.instance.EndStory = EndGameType.ExplodeShip;
            _context.TransitionTo<EndState>();
        }

        public void EnterState()
        {
            transform.position = new Vector3(0f, 0f, transform.position.z);
            gameObject.SetActive(true);
            // Debug.Log($"Entered {this.GetType()}");
            foreach (var tool in _tools)
            {
                tool.EnableInputs();
            }
            var sprites = _arm.GetComponentsInChildren<SpriteRenderer>();
            foreach(var sprite in sprites) {
                sprite.enabled = true;
            }
            _arm.EnableInputs();
            _hand.EnableInputs();
            _hud.active = true;
            _rivetGun.EnableInputs = true;
            _oxygenCounter.SetIsRunning(true);
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
            var sprites = _arm.GetComponentsInChildren<SpriteRenderer>();
            foreach(var sprite in sprites) {
                sprite.enabled = false;
            }
            _arm.DisableInputs();
            _hand.DisableInputs();
            _hud.active = false;
            _rivetGun.EnableInputs = false;
            _oxygenCounter.SetIsRunning(false);
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