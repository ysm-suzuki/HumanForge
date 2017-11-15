using UnityMVC;

namespace GameMain
{
    public class FaceForgeUIMode : UIMode
    {
        public FaceForgeUIMode()
        {
            UnityEngine.Debug.Log("FaceForgeUIMode");
        }

        public override void ClickMap(Position position)
        {
            Change(new DefaultUIMode());
        }
    }
}