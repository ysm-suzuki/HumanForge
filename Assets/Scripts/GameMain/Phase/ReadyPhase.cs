
namespace GameMain
{
    public class ReadyPhase : Phase
    {
        public static string Tag = "ReadyPhase";

        override public void Initialize()
        {

        }

        override public void Tick(float delta)
        {
            End();
        }

        override protected void End()
        {
            base.End(MainPhase.Tag);
        }
    }
}

