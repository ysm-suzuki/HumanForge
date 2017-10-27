
namespace GameMain
{
    public class MainPhase : Phase
    {
        public static string Tag = "MainPhase";

        override public void Initialize()
        {

        }

        override public void Tick(float delta)
        {

        }

        override protected void End()
        {
            base.End(ReadyPhase.Tag);
        }
    }
}
