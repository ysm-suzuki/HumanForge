using System.Collections.Generic;

using UnityEngine;

public class FileManager
{
    private static FileManager _instance = null;
    public static FileManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new FileManager();

            return _instance;
        }
    }
    private FileManager() { }

    public FileManager Initialize()
    {
        DataPath = Application.dataPath;
        StreamingAssetsPath = Application.streamingAssetsPath;
        PersistentDataPath = Application.persistentDataPath;
        return this;
    }
    
    private string DataPath;
    private string StreamingAssetsPath;
    private string PersistentDataPath;
    

    class Filer
    {
        public string path;
        public System.Action callback;
        public System.Func<bool> IsFinished;
    }
    private Queue<Filer> _loadingFileQueue = new Queue<Filer>();

    public void Tick()
    {
        if (_loadingFileQueue.Count > 0)
        {
            if (_loadingFileQueue.Peek().IsFinished())
            {
                _loadingFileQueue.Dequeue().callback();
            }
        }
    }


    public void LoadTextFromStreamingAssets(string path, System.Action<string> callback)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        var url = "jar:file://" + DataPath + "!/assets/" + path;
#elif UNITY_IOS && !UNITY_EDITOR
        var url = DataPath + "/Raw/" + path;
#else
        var url = "file://" + StreamingAssetsPath + "/" + path;
#endif
        WWW www = new WWW(url);

        _loadingFileQueue.Enqueue(new Filer
        {
            path = path,
            callback = () =>
            {
                SaveText(path, www.text);
                LoadText(path, callback);
            },
            IsFinished = () =>
            {
                return www.isDone;
            }
        });
    }



    public void LoadText(string path, System.Action<string> callback)
    {
        var absolutePath = PersistentDataPath + "/" + path;

        if (!System.IO.File.Exists(absolutePath))
        {
            LoadTextFromStreamingAssets(path, callback);
            return;
        }

        var reader = new System.IO.StreamReader(absolutePath);
        var text = reader.ReadToEnd();
        reader.Close();

        callback(text);
    }



    public void SaveText(string path, string text)
    {
        var absolutePath = PersistentDataPath + "/" + path;

        if (!System.IO.File.Exists(absolutePath))
        {
            System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(absolutePath));
        }

        var writer = new System.IO.StreamWriter(absolutePath);
        writer.Write(text);
        writer.Close();

        return;
    }

    public void LoadSQlite3FromStreamingAssets(string path, System.Action<byte[]> callback)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        var url = "jar:file://" + DataPath + "!/assets/" + path;
#elif UNITY_IOS && !UNITY_EDITOR
        var url = DataPath + "/Raw/" + path;
#else
        var url = "file://" + StreamingAssetsPath + "/" + path;
#endif
        WWW www = new WWW(url);

        _loadingFileQueue.Enqueue(new Filer
        {
            path = path,
            callback = () =>
            {
                callback(www.bytes);
            },
            IsFinished = () =>
            {
                return www.isDone;
            }
        });
    }
}