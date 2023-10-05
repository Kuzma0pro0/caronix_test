namespace Test.States
{
    public abstract class BaseState 
    {
        protected readonly IStationStateSwither _stateSwitcher;

        protected BaseState(IStationStateSwither stateSwitcher)
        {
            _stateSwitcher = stateSwitcher;
        }

        public abstract void Start();

        public abstract void Stop();

    }

}
