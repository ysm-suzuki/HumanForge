using System.Collections.Generic;

using UnityEngine;

using UnityMVC;
using GameMain;

public class GameMainKicker : MonoBehaviour
{
    [SerializeField]
    private RectTransform _rootRectTransform = null;

    [SerializeField]
    private GameObject _root = null;
    [SerializeField]
    private GameObject _boardRoot = null;
    [SerializeField]
    private GameObject _uiRoot = null;
    [SerializeField]
    private GameObject _informationRoot = null;
    [SerializeField]
    private GameObject _overlayRoot = null;
    [SerializeField]
    private GameObject _systemFrontRoot = null;


    public static string BoardRootTag = "BoardRootTag";
    public static string UIRootTag = "UIRootTag";
    public static string InformationRootTag = "InformationRootTag";
    public static string OverlayRootTag = "OverlayRootTag";
    public static string SystemFrontRootTag = "SystemFrontRootTag";


    private Dictionary<string, Phase> _allPhases = new Dictionary<string, Phase>();
    private Phase _currentPhase = null;


    private InputPorter _inputPorter = null;

    // kari 
    private bool _initialized = false;

    void Start()
    {
        GameMain.Random.Initialize();
        FileManager.Instance.Initialize();

        new SerialProcess()
            .Add(finish =>
            {
                GameMain.SQLite.Instance.Initialize(finish);
            }).Add(finish =>
            {
                ResolutionManager.Instance.SetRootRectTransform(_rootRectTransform);

                ViewManager.Instance.RegisterRoot(_root);
                ViewManager.Instance.RegisterRoot(_boardRoot, BoardRootTag);
                ViewManager.Instance.RegisterRoot(_uiRoot, UIRootTag);
                ViewManager.Instance.RegisterRoot(_informationRoot, InformationRootTag);
                ViewManager.Instance.RegisterRoot(_overlayRoot, OverlayRootTag);
                ViewManager.Instance.RegisterRoot(_systemFrontRoot, SystemFrontRootTag);

                SetupInput();
                SetupPhases();

                _initialized = true;
            }).Flush();
    }

    void Update()
    {

        float delta = Time.deltaTime;
        
        GameMain.SQLite.Instance.Tick();
        FileManager.Instance.Tick();


        if (!_initialized)
            return;
        if (_inputPorter != null)
            _inputPorter.Tick(delta);
        if (_currentPhase != null)
            _currentPhase.Tick(delta);
    }

    void OnDestroy()
    {
        TearDown();
    }

    private void SetupInput()
    {
        if (Input.touchSupported)
            _inputPorter = new TouchInputPorter();
        else
            _inputPorter = new MouseInputPorter();
    }

    private void SetupPhases()
    {
        _currentPhase = _allPhases[ReadyPhase.Tag] = new ReadyPhase();
        _allPhases[MainPhase.Tag] = new MainPhase();
        _allPhases[FinishPhase.Tag] = new FinishPhase();
        

        foreach (var phase in _allPhases.Values)
        {
            phase.RegisterOnFinishedCallback(tag=> 
            {
                if (!_allPhases.ContainsKey(tag))
                {
                    EndScene();
                    return;
                }

                _currentPhase = _allPhases[tag];
                _currentPhase.Initialize();
            });
        }

        _currentPhase.Initialize();
    }


    private void EndScene()
    {
        TearDown();
    }

    private void TearDown()
    {
        GameMain.SQLite.Instance.TearDown();
    }
}
