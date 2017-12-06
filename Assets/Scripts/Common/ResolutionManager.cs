using System.Collections.Generic;

using UnityEngine;

public class ResolutionManager
{
    public static ResolutionManager _instance = null;
    public static ResolutionManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new ResolutionManager();

            return _instance;
        }
    }
    private ResolutionManager() { }
    public void TearDown()
    {
        _instance = null;
    }


    private const float DefaultWidth = 640;
    private const float DefaultHeight = 1136;


    private RectTransform _rootRectTransform;
    private float _screenWidth;
    private float _screenHeight;
    private Vector2 _defaultSizeDelta;

    private float _scaleResolution = 1.0f;

    public ResolutionManager SetRootRectTransform(RectTransform rectTransform)
    {
        _rootRectTransform = rectTransform;
        _defaultSizeDelta = new Vector2(rectTransform.sizeDelta.x, rectTransform.sizeDelta.y);

        SetUp();

        return this;
    }

    public void Tick()
    {
        if (_screenWidth != Screen.width
            || _screenHeight != Screen.height)
        {
            SetUp();
        }
    }


    public float scale
    {
        get { return _scaleResolution; }
    }

    public float width
    {
        get { return _screenWidth; }
    }
    public float height
    {
        get { return _screenHeight; }
    }


    private void SetUp()
    {
        var rectTransform = _rootRectTransform;
        _screenWidth = Screen.width;
        _screenHeight = Screen.height;

        _scaleResolution = 1.0f * Screen.width / DefaultWidth;

        rectTransform.localScale = Vector3.one * _scaleResolution;

        float diff = DefaultWidth - _screenWidth;
        rectTransform.sizeDelta = new Vector2(
            _defaultSizeDelta.x + diff,
            diff * _screenHeight / _screenWidth);
    }
}
