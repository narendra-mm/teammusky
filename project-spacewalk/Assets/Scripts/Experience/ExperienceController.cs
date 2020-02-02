using Experience.ExperienceState;
using SpaceWalk.GameLogic;
using System.Collections.Generic;
using UnityEngine;

namespace Experience
{
    [RequireComponent(typeof(ExperienceStateManager))]
    public class ExperienceController : MonoBehaviour
    {
        public enum States
        {
            SplashScreen,
            Intro,
            Fixing,
            DamageShip,
            End
        }

        public States DefaultState = States.Intro;

        private ExperienceStateManager _experienceStateManager;

        void Start()
        {
            GameState.instance.Setup();
            _experienceStateManager = GetComponent<ExperienceStateManager>();
            var states = transform.GetComponentsInChildren<IExperienceState>();
            _experienceStateManager.Setup(states);

            switch (DefaultState)
            {
                case States.SplashScreen:
                    _experienceStateManager.TransitionTo<SplashScreenState>();
                    break;
                case States.Intro:
                    _experienceStateManager.TransitionTo<IntroState>();
                    break;
                case States.Fixing:
                    _experienceStateManager.TransitionTo<FixingPanelState>();
                    break;
                case States.DamageShip:
                    _experienceStateManager.TransitionTo<DamageShipState>();
                    break;
                case States.End:
                    _experienceStateManager.TransitionTo<EndState>();
                    break;
            }



            Debug.Log($"Current state is {_experienceStateManager.CurrentState.GetType()}");
        }
    }
}