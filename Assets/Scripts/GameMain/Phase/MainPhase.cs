
namespace GameMain
{
    public class MainPhase : Phase
    {
        public static string Tag = "MainPhase";

        private Board _board = new Board();

        override public void Initialize()
        {

        }

        override public void Tick(float delta)
        {
            _board.Tick(delta);
        }

        override protected void End()
        {
            base.End(ReadyPhase.Tag);
        }
    }
}
