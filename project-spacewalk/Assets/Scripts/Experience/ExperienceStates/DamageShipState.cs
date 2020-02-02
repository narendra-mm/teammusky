using SpaceWalk.GameLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Experience.ExperienceState
{
	public class DamageShipState : MonoBehaviour, IExperienceState
	{
        private ShipPart[] _shipParts;
        private ExperienceStateManager _context;
        private ShipPartType _partToDamange;
		public void DisposeState()
		{
			Debug.Log($"Disposed {this.GetType()}");
		}

		public void EnterState()
		{
            gameObject.SetActive(true);
            _partToDamange = GameState.instance.GetAHealthyPart();
            var pair = _shipParts.FirstOrDefault(item => item.ShipPartType == _partToDamange);
            if(pair == null)
            {
                Debug.LogError($"Could not find a timeline of type {_partToDamange}");
            }
            pair.PlayTimeline();
            Debug.Log($"Playing damange {pair.ShipPartType}");
		}

		public void ExitState()
		{
            var pair = _shipParts.FirstOrDefault(item => item.ShipPartType == _partToDamange);
            pair.ResetTimeLine();
            Debug.Log($"Exited {this.GetType()}");
		}

		public void InitializeState(ExperienceStateManager context)
		{
            _shipParts = GetComponentsInChildren<ShipPart>();
            _context = context;
            gameObject.SetActive(false);
			Debug.Log($"Initialised {this.GetType()}");
		}

		public void UpdateState()
		{

		}

        public void DamangeTimelineEnded()
        {
            GameState.instance.DestroyPart(_partToDamange);
            _context.TransitionTo<FixingPanelState>();
        }

        [Serializable]
        public class TimelinePart
        {
            public ShipPart Part;
            public PlayableDirector Director;
        }
	}
}