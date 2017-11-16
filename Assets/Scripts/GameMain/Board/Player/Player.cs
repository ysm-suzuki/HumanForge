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


            var initialFaceMolds = new FaceMoldGroup()
            .Filter(
                mold =>
                {
                    return mold.type == FaceData.Type.Initial;
                })
            .molds;
            foreach (var mold in initialFaceMolds)
                AddFace(mold.Pick());



            // kari
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
            _manas[ManaData.Type.Red].OnAmountUpdated += () => { UnityEngine.Debug.Log("red mana : " + _manas[ManaData.Type.Red].amount); };
            _manas[ManaData.Type.Green].OnAmountUpdated += () => { UnityEngine.Debug.Log("green mana : " + _manas[ManaData.Type.Green].amount); };
            _manas[ManaData.Type.Blue].OnAmountUpdated += () => { UnityEngine.Debug.Log("blue mana : " + _manas[ManaData.Type.Blue].amount); };
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


        public void AddFace(Face face)
        {
            _faces.Add(face);

            foreach(var buff in face.buffs)
                _playerUnit.AddBuff(buff);

            if (OnFacesUpdated != null)
                OnFacesUpdated();
        }
        public void ReplaceFace(Face face, int index)
        {
            var replacedFace = _faces[index];
            foreach (var buff in replacedFace.buffs)
                buff.duration.End();

            _faces[index] = face;

            foreach (var buff in face.buffs)
                _playerUnit.AddBuff(buff);

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

        public bool HasEnoughMana(Dictionary<ManaData.Type, float> requiringManas)
        {
            foreach (var pair in requiringManas)
                if (_manas[pair.Key].amount < pair.Value)
                    return false;

            return true;
        }


        public int faceCount
        {
            get
            {
                return _faces.Count;
            }
        }
    }
}