namespace Test.States
{
    public interface IStationStateSwither 
    {
        void SwitchState<T>() where T : BaseState;
    }

}
