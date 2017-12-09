using System.Collections.Generic;
using System.Collections.ObjectModel;

using UnityMVC;

namespace GameMain
{
    public class Player
    {
        public delegate void EventHandler();
        public event EventHandler OnFacesUpdated;
        public event EventHandler OnLifeUpdated;
        public event EventHandler OnAuraUpdated;

        public delegate void UnitEventHandler(Unit unit);
        public event UnitEventHandler OnUnitPlaced;
        public event UnitEventHandler OnUnitDead;

        public delegate void ManaEventHandler(Mana mana);
        public event ManaEventHandler OnManaUpdated;

        private Unit _playerUnit = null;
        private List<Face> _faces = new List<Face>();
        private Dictionary<ManaData.Type, Mana> _manas = new Dictionary<ManaData.Type, Mana>();

        private RepeatTimer _diceTimer = null;


        private const int TeamId = 1; // kari


        public void SetUp(int level)
        {
            var levelData = LevelMasterData.loader.Get(level);

            _playerUnit = levelData.playerUnit;
            UnityEngine.Debug.Assert(_playerUnit != null, "The player unit not set at level " + level);
            PlaceUnit(_playerUnit);

            _playerUnit.OnLifeUpdated += () => 
            {
                if (OnLifeUpdated != null)
                    OnLifeUpdated();
            };


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


            _diceTimer = new RepeatTimer(diceIntervalSeconds, 
                () => 
                {
                    int index = Random.Range(0, faceCount - 1);
                    var targetFace = _faces[index];
                    targetFace.Activate(this);
                });

            
            float InitialMaxMana = 20; // kari
            _manas[ManaData.Type.Red] = new Mana(new ManaData
            {
                type = ManaData.Type.Red,
                max = InitialMaxMana,
            });
            _manas[ManaData.Type.Green] = new Mana(new ManaData
            {
                type = ManaData.Type.Green,
                max = InitialMaxMana,
            });
            _manas[ManaData.Type.Blue] = new Mana(new ManaData
            {
                type = ManaData.Type.Blue,
                max = InitialMaxMana,
            });
            _manas[ManaData.Type.Red].OnAmountUpdated += () => 
            {
                if (OnManaUpdated != null)
                    OnManaUpdated(_manas[ManaData.Type.Red]);
            };
            _manas[ManaData.Type.Green].OnAmountUpdated += () => 
            {
                if (OnManaUpdated != null)
                    OnManaUpdated(_manas[ManaData.Type.Green]);
            };
            _manas[ManaData.Type.Blue].OnAmountUpdated += () => 
            {
                if (OnManaUpdated != null)
                    OnManaUpdated(_manas[ManaData.Type.Blue]);
            };
        }


        public void Tick(float delta)
        {
            _diceTimer.Tick(delta);
        }


        public void PlaceUnit(Unit unit)
        {
            unit.isOwnedUnit = true;
            unit.teamId = TeamId;

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

            if (OnFacesUpdated != null)
                OnFacesUpdated();
        }
        public void ReplaceFace(Face face, int index)
        {
            var replacedFace = _faces[index];
            _faces[index] = face;

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

        public void ExpandMana(ManaData.Type type, float amount)
        {
            _manas[type].max += amount;

            if (OnManaUpdated != null)
                OnManaUpdated(_manas[type]);
        }


        public void MoveTo(Position to)
        {
            _playerUnit.MoveTo(to);
        }


        public void PowerUp(Unit.Aura aura)
        {
            _playerUnit.PowerUp(aura);

            if (OnAuraUpdated != null)
                OnAuraUpdated();
        }


        public int faceCount
        {
            get
            {
                return _faces.Count;
            }
        }

        public float diceIntervalSeconds
        {
            get
            {
                float diceInterval = 5.0f; // kari
                return diceInterval;
            }
        }

        public Position position
        {
            get { return _playerUnit.position; }
        }
        public float life
        {
            get { return _playerUnit.life; }
            set { _playerUnit.life = value; }
        }
        public float maxLife
        {
            get { return _playerUnit.maxLife; }
        }
        public float attack
        {
            get { return _playerUnit.attack; }
        }
        public float defense
        {
            get { return _playerUnit.defense; }
        }

        public ReadOnlyCollection<Mana> allMana
        {
            get
            {
                var manaList = new List<Mana>();
                foreach (var mana in _manas.Values)
                    manaList.Add(mana);
                return manaList.AsReadOnly();
            }
        }
    }
}