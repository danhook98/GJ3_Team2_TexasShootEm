namespace TexasShootEm.EventSystem
{
    public class VoidEventTrigger : AbstractEventTrigger<Empty>
    {
        public void Trigger() => eventToTrigger.Invoke(new Empty());
    }
}   