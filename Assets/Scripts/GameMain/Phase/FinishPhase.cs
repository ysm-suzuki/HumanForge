
namespace GameMain
{
    public class FinishPhase : Phase
    {
        public static string Tag = "FinishPhase";

        override public void Initialize()
        {

        }

        override public void Tick(float delta)
        {
            End();
        }

        override protected void End()
        {
            base.End();
        }
    }
}

