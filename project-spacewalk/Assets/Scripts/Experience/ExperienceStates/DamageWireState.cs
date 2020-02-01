using SpaceWalk.GameLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Experience.ExperienceState
{
    [RequireComponent(typeof(PlayableDirector))]
	public class DamageWireState : MonoBehaviour, IExperienceState
	{
        [SerializeField] private Camera _camera;
        [SerializeField] private List<TimelinePart> _damageTimeLines;

        private PlayableDirector _playableDirector;
        private DamagablePart _partToDamange;
		public void DisposeState()
		{
			Debug.Log($"Disposed {this.GetType()}");
		}

		public void EnterState()
		{
            gameObject.SetActive(true);
            _partToDamange = GameState.instance.GetAHealthyPart();
            var pair = _damageTimeLines.FirstOrDefault(item => item.Part == _partToDamange);
            if(pair == null)
            {
                Debug.LogError($"Could not find a timeline of type {_partToDamange}");
            }
            _playableDirector.playableAsset = pair.Timeline;
            _playableDirector.Play();
            Debug.Log($"Entered {this.GetType()}");
		}

		public void ExitState()
		{
			Debug.Log($"Exited {this.GetType()}");
		}

		public void InitializeState(ExperienceStateManager context)
		{
            _playableDirector = GetComponent<PlayableDirector>();
            gameObject.SetActive(false);
			Debug.Log($"Initialised {this.GetType()}");
		}

		public void UpdateState()
		{

		}

        public void DamangeTimelineEnded()
        {
            GameState.instance.DestroyPart(_partToDamange);
        }

        [Serializable]
        public class TimelinePart
        {
            public DamagablePart Part;
            public TimelineAsset Timeline;
        }
	}
}