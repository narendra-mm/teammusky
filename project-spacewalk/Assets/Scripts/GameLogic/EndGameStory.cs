using UnityEngine;
using UnityEngine.Playables;

namespace SpaceWalk.GameLogic
{
    public class EndGameStory : MonoBehaviour
    {
        public EndGameType EndStory;
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
