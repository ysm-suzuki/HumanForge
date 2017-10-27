using UnityEngine;
using UnityMVC;

public class SimpleSample : MonoBehaviour
{
    private InputPorter _inputPorter = null;

    private SampleButton _button = null;

    void Start()
    {
        if (Input.touchSupported)
        {
            _inputPorter = new TouchInputPorter();
        }
        else
        {
            _inputPorter = new MouseInputPorter();
        }


        _button = new SampleButton();
        _button.position = Position.Create(100, 50);
        _button.OnClicked += ()=> 
        {
            Debug.Log("The button has clicked.");
            _button.position = Position.Create(100, 50);
        };

        SampleButtonView
            .Attach(gameObject)
            .SetModel(_button);
    }
    

    void Update()
    {
        var delta = Time.deltaTime;

        if (_button != null)
            _button.position += Position.Create(0, 0.1f);

        if (_inputPorter != null)
            _inputPorter.Tick(delta);
    }
}
