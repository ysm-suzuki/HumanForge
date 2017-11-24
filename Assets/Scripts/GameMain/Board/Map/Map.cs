using System.Collections.Generic;

using UnityMVC;

namespace GameMain
{
    public class Map : FieldObject
    {
        public event EventHandler OnUnitDead;

        public delegate void UnitEventHandler(Unit unit);
        public event UnitEventHandler OnUnitAdded;

        public delegate void WallEventHandler(Wall wall);
        public event WallEventHandler OnWallAdded;

        public delegate void BarricadeEventHandler(Barricade barricade);
        public event BarricadeEventHandler OnBarricadeAdded;


        private MapMasterData _data;


        private List<Wall> _walls = new List<Wall>();
        private List<Unit> _units = new List<Unit>();
        private List<Barricade> _barricades = new List<Barricade>();

        private List<Wall> _removingWalls = new List<Wall>();
        private List<Unit> _removingUnits = new List<Unit>();
        private List<Barricade> _removingBarricades = new List<Barricade>();

        private List<Wall> _addingWalls = new List<Wall>();
        private List<Unit> _addingUnits = new List<Unit>();
        private List<Barricade> _addingBarricades = new List<Barricade>();


        public void SetUp(int level)
        {
            var levelData = LevelMasterData.loader.Get(level);

            _data = MapMasterData.loader.Get(levelData.mapId);

            foreach (var wall in _data.walls)
                AddWall(wall);
            foreach (var barricade in _data.barricades)
                AddBarricade(barricade);
        }


        override public void Tick(float delta)
        {
            base.Tick(delta);

            UpdateRecoginitoins();

            foreach (var wall in _walls)
                wall.Tick(delta);
            foreach (var unit in _units)
                unit.Tick(delta);
            foreach (var barricade in _barricades)
                barricade.Tick(delta);
            
            SolveMoving();
            RemoveFieldObjects();
            AddFieldObjects();
        }



        public void AddUnit(Unit unit)
        {
            if (_units.Contains(unit))
                return;
            foreach (var aUnit in _units)
                if (unit.IsSame(aUnit))
                    return;

            unit.OnRemoved += () => 
            {
                RemoveUnit(unit);
            };
            unit.OnDead += () => 
            {
                if (OnUnitDead != null)
                    OnUnitDead();
            };

            _addingUnits.Add(unit);
        }

        public void RemoveUnit(Unit unit)
        {
            if (!_units.Contains(unit))
                return;

            _removingUnits.Add(unit);
        }

        public void AddWall(Wall wall)
        {
            if (_walls.Contains(wall))
                return;
            foreach (var aWall in _walls)
                if (wall == aWall)
                    return;

            wall.OnRemoved += () =>
            {
                RemoveWall(wall);
            };

            _addingWalls.Add(wall);
        }

        public void RemoveWall(Wall wall)
        {
            if (!_walls.Contains(wall))
                return;

            _removingWalls.Add(wall);
        }

        public void AddBarricade(Barricade barricade)
        {
            if (_barricades.Contains(barricade))
                return;

            barricade.OnRemoved += () =>
            {
                RemoveBarricade(barricade);
            };

            _addingBarricades.Add(barricade);
        }

        public void RemoveBarricade(Barricade barricade)
        {
            if (!_barricades.Contains(barricade))
                return;

            _removingBarricades.Add(barricade);
        }

        
        // ======================================== private functions
        private void UpdateRecoginitoins()
        {
            foreach (var unit in _units)
            {
                var recognition = unit.recognition;
                
                var recognizedUnits = new List<Unit>();
                foreach (var targetUnit in _units)
                    if (unit.IsInSight(targetUnit)
                        && !unit.IsSame(targetUnit))
                        recognizedUnits.Add(targetUnit);
                recognition.UpdateUnits(recognizedUnits);
                
                var recognizedWalls = new List<Wall>();
                foreach (var wall in _walls)
                    if (unit.IsInSight(wall))
                        recognizedWalls.Add(wall);
                recognition.UpdateWalls(recognizedWalls);
            }
        }


        // ======================================== accessor
        public Unit playerUnit
        {
            get
            {
                foreach (var unit in _units)
                    if (unit.isPlayerUnit)
                        return unit;

                return null;
            }
        }

        public List<Unit> enemyUnits
        {
            get
            {
                var units = new List<Unit>();
                foreach (var unit in _units)
                {
                    if (!unit.isAlive)
                        continue;

                    if (unit.isOwnedUnit
                        || unit.isPlayerUnit)
                        continue;

                    units.Add(unit);
                }

                return units;
            }
        }

        public float width
        {
            get { return _data.width; }
        }

        public float height
        {
            get { return _data.height; }
        }

        // ======================================== adding or removing field objects
        private void RemoveFieldObjects()
        {
            foreach (var removingWall in _removingWalls)
                _walls.Remove(removingWall);
            _removingWalls.Clear();

            foreach (var removnigUnit in _removingUnits)
                _units.Remove(removnigUnit);
            _removingUnits.Clear();

            foreach (var removnigBarricade in _removingBarricades)
                _barricades.Remove(removnigBarricade);
            _removingBarricades.Clear();
        }
        private void AddFieldObjects()
        {
            foreach (var addingWall in _addingWalls)
            {
                _walls.Add(addingWall);
                
                if (OnWallAdded != null)
                    OnWallAdded(addingWall);
            }
            _addingWalls.Clear();

            foreach (var addingUnit in _addingUnits)
            {
                _units.Add(addingUnit);

                if (OnUnitAdded != null)
                    OnUnitAdded(addingUnit);
            }
            _addingUnits.Clear();

            foreach (var addingBarricade in _addingBarricades)
            {
                _barricades.Add(addingBarricade);

                if (OnBarricadeAdded != null)
                    OnBarricadeAdded(addingBarricade);
            }
            _addingBarricades.Clear();
        }

        // ======================================== moving field objects

        private void SolveMoving()
        {
            var vectorBurner = new VectorBurner();

            foreach (var unit in _units)
            {
                // -------------------------------
                var fieldObjects = GetFieldObjectsWithout(unit);

                var unitBoundaryLines = new List<Atagoal.Core.Point>();
                foreach (var point in unit.shapePoints)
                    unitBoundaryLines.Add(point);

                var obstacles = new List<VectorBurnerCalculation.Body>();
                foreach (var fieldObject in fieldObjects)
                {
                    var fieldObjectBoundaryLines = new List<Atagoal.Core.Point>();
                    foreach (var point in fieldObject.shapePoints)
                        fieldObjectBoundaryLines.Add(point);

                    obstacles.Add(new VectorBurnerCalculation.Body
                    {
                        point = fieldObject.position,
                        vertices = fieldObjectBoundaryLines,
                    });
                }

                var destination = vectorBurner
                    .SetTarget(new VectorBurnerCalculation.Body
                    {
                        point = unit.position,
                        radius = unit.sizeRadius
                    })
                    .SetBarricades(obstacles)
                    .SetBounce(1.0f)
                    .GetDestination(Atagoal.Core.Vector.Create(unit.velocity.x, unit.velocity.y));
                // -------------------------------

                unit.position = Position.Create(destination.x, destination.y);
                unit.velocity = Position.Create(0, 0);
            }
        }


        private List<FieldObject> GetFieldObjectsWithout(Unit unit)
        {
            var fieldObjects = new List<FieldObject>();

            foreach (var wall in _walls)
                fieldObjects.Add(wall);
            foreach (var fieldUnit in _units)
                if (!fieldUnit.IsSame(unit))
                    fieldObjects.Add(fieldUnit);
            foreach (var fieldBarricade in _barricades)
                    fieldObjects.Add(fieldBarricade);

            return fieldObjects;
        }
    }
}