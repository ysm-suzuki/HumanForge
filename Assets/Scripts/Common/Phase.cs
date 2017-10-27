using UnityEngine;

public class Phase
{
    private System.Action<string> _onFinished = null;

    public void RegisterOnFinishedCallback(System.Action<string> callback)
    {
        _onFinished = callback;
    }

    virtual public void Initialize()
    {

    }

    virtual public void Tick(float delta)
    {

    }

    virtual protected void End()
    {
        End("");
    }

    virtual protected void End(string next)
    {
        if (_onFinished != null)
            _onFinished(next);
    }
}
