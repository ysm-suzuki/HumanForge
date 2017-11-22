using UnityMVC;

public class OneShotEffectView : View
{
    protected System.Action _finishCallback = null;
    
    void Start()
    {
        PlayAnimation(_animation.clip.name, () => 
        {
            if (_finishCallback != null)
                _finishCallback();
        });
    }
}
