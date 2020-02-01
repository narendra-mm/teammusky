namespace GoogleRCS.Experience.ExperienceState
{
    public interface IExperienceState 
    {
        void InitializeState(ExperienceStateManager context);
        void EnterState();
        void ExitState();
        void UpdateState();
        void DisposeState();
    }
}