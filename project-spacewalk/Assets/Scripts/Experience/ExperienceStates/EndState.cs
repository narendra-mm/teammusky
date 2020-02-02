using SpaceWalk.GameLogic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Experience.ExperienceState
{
    public class EndState : MonoBehaviour, IExperienceState
    {
        private ExperienceStateManager _context;
        private EndGameStory[] _endStories;
        public void InitializeState(ExperienceStateManager context)
        {
            gameObject.SetActive(false);
            _context = context;
            _endStories = GetComponentsInChildren<EndGameStory>();
            Debug.Log($"Initialised {this.GetType()}");
        }

        public void EnterState()
        {
            gameObject.SetActive(true);
            var endGame = GameState.instance.EndStory;
            var pair = _endStories.FirstOrDefault(item => item.EndStory == endGame);
            if (pair == null)
            {
                Debug.LogError($"Could not find a timeline of type {endGame}");
            }
            pair.PlayTimeline();
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
            SceneManager.LoadScene("Main", LoadSceneMode.Single);
        }
    }
}