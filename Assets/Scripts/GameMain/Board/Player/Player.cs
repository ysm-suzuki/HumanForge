using System.Collections.Generic;

using UnityMVC;

namespace GameMain
{
    public class Player
    {
        public delegate void EventHandler();
        public event EventHandler OnFacesUpdated;

        public delegate void UnitEventHandler(Unit unit);
        public event UnitEventHandler OnUnitPlaced;
        public event UnitEventHandler OnUnitDead;


        private Unit _playerUnit = null;
        private List<Face> _faces = new List<Face>();


        public void SetUp(int level)
        {
            var levelData = LevelMasterData.loader.Get(level);

            _playerUnit = levelData.playerUnit;
            UnityEngine.Debug.Assert(_playerUnit != null, "The player unit not set at level " + level);
            PlaceUnit(_playerUnit);
            
            foreach (var unit in levelData.ownedUnits)
                PlaceUnit(unit);
        }


        public void Tick(float delta)
        {

        }


        public void PlaceUnit(Unit unit)
        {
            unit.isOwnedUnit = true;

            unit.OnDead += () => 
            {
                if (OnUnitDead != null)
                    OnUnitDead(unit);
            };

            if (OnUnitPlaced != null)
                OnUnitPlaced(unit);
        }


        public void AddFace(FaceData data)
        {
            _faces.Add(new Face(data));

            if (OnFacesUpdated != null)
                OnFacesUpdated();
        }
        public void ReplaceFace(FaceData data, int index)
        {
            _faces[index] = new Face(data);

            if (OnFacesUpdated != null)
                OnFacesUpdated();
        }
    }
}