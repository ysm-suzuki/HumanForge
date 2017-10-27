using UnityMVC;

public class SampleButton : Model
{
    public event EventHandler OnClicked;
    
    public void Click()
    {
        if (OnClicked != null)
            OnClicked();
    }
}
