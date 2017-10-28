using UnityMVC;

namespace GameMain
{
    public class MainPhase : Phase
    {
        public static string Tag = "MainPhase";

        private Board _board = new Board();

        override public void Initialize()
        {

            // kari
            var unit = new Unit(new UnitData
            {
                life = 10,
                attack = 5,
                attackSpeed = 1.0f,
                defence = 5,
                moveSpeed = 10.0f
            });

            UnitView
                .Attach(ViewManager.Instance.GetRoot(GameMainKicker.BoardRootTag))
                .SetModel(unit);

            _board.AddUnit(unit);
            unit.MoveTo(Position.Create(100,0));
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
