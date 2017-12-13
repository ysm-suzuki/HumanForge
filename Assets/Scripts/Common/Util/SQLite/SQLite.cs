using System.Threading;
using System.IO;

using SqlCipher4Unity3D;

using UnityEngine;

namespace GameMain
{
    public class SQLite
    {
        private const string DataBasePath = "Data/MasterData/master.db";
        private const string DataBasePassword = "aaaaaaaa";


        private static SQLite _instance = null;
        public static SQLite Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SQLite();

                return _instance;
            }
        }
        private SQLite() { }
        public void TearDown()
        {
            if (_connection != null)
                _connection.Close();
        }


        private SQLiteConnection _connection;
        public SQLiteConnection connection
        {
            get { return _connection; }
        }
        public static SQLiteConnection Connection
        {
            get { return Instance._connection; }
        }



        private System.Action _initilizeCallback = null;

        public void Initialize(System.Action callback)
        {
            string DatabaseName = DataBasePath;
            var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);

            System.Action ConnectDataBase = () =>
            {
                new Thread(() =>
                {
                    _connection = new SQLiteConnection(filepath, DataBasePassword);

                    UnityEngine.Debug.Assert(_connection != null,
                        "Database(" + filepath + ") could not open.");
                    
                    _initilizeCallback = callback;
                }).Start();
            };

            // TODO : delete when the existing file is lower versoin 
            if (File.Exists(filepath))
                File.Delete(filepath);

            FileManager.Instance.LoadSQlite3FromStreamingAssets(DatabaseName, bytes =>
            {
                var lastIndex = filepath.LastIndexOf("/");
                string directory = filepath.Substring(0, lastIndex);
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                File.WriteAllBytes(filepath, bytes);

                ConnectDataBase();
            });
        }
        

        public void Tick()
        {
            if (_initilizeCallback != null)
            {
                _initilizeCallback();
                _initilizeCallback = null;
            }
        }
    }
}
