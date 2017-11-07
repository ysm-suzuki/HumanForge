namespace GameMain
{
    public class DefaultUIMode : UIMode
    {
        override public void ClickUnit(Unit unit)
        {
            Change(new OwnedUnitUIMode()
                        .SetUnit(unit));
        }
    }
}