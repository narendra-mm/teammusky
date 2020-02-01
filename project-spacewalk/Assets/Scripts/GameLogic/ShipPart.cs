using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace SpaceWalk.GameLogic
{
    [RequireComponent(typeof(PlayableDirector))]
    public class ShipPart : MonoBehaviour
    {
        public ShipPartType ShipPartType;

        private PlayableDirector _director;
        // Start is called before the first frame update
        void Start()
        {
            _director = GetComponent<PlayableDirector>();
        }

        public void PlayTimeline()
        {
            _director.Play();
        }
        public void ResetTimeLine()
        {
            _director.Stop();
        }
    }
}