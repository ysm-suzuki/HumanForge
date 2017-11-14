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
        private Dictionary<ManaData.Type, Mana> _manas = new Dictionary<ManaData.Type, Mana>();


        public void SetUp(int level)
        {
            var levelData = LevelMasterData.loader.Get(level);

            _playerUnit = levelData.playerUnit;
            UnityEngine.Debug.Assert(_playerUnit != null, "The player unit not set at level " + level);
            PlaceUnit(_playerUnit);
            
            foreach (var unit in levelData.ownedUnits)
                PlaceUnit(unit);

            _manas[ManaData.Type.Red] = new Mana(new ManaData
            {
                type = ManaData.Type.Red,
                max = 10,
            });
            _manas[ManaData.Type.Green] = new Mana(new ManaData
            {
                type = ManaData.Type.Green,
                max = 10,
            });
            _manas[ManaData.Type.Blue] = new Mana(new ManaData
            {
                type = ManaData.Type.Blue,
                max = 10,
            });


            // kari
            AddFace(new FaceData
            {
                manaGenerators = new Dictionary<ManaData.Type, float>
                {
                    { ManaData.Type.Red, 0.5f}
                }
            });
            _manas[ManaData.Type.Red].OnAmountUpdated += () => { UnityEngine.Debug.Log("red mana : " + _manas[ManaData.Type.Red].amount); };
        }


        public void Tick(float delta)
        {
            foreach(var face in _faces)
            {
                foreach (var generator in face.manaGenerators)
                    GainMana(generator.Key, generator.Value * delta);
            }
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


        public void GainMana(ManaData.Type type, float amount)
        {
            _manas[type].amount += amount;
        }
        public void LoseMana(ManaData.Type type, float amount)
        {
            _manas[type].amount -= amount;
        }
    }
}