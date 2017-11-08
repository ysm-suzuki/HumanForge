using System.Collections.Generic;

using UnityMVC;

namespace GameMain
{
    public class Map : FieldObject
    {
        public delegate void UnitEventHandler(Unit unit);
        public event UnitEventHandler OnUnitAdded;

        public delegate void WallEventHandler(Wall wall);
        public event WallEventHandler OnWallAdded;

        public delegate void BarricadeEventHandler(Barricade barricade);
        public event BarricadeEventHandler OnBarricadeAdded;



        private List<Wall> _walls = new List<Wall>();
        private List<Unit> _units = new List<Unit>();

        private List<Wall> _removingWalls = new List<Wall>();
        private List<Unit> _removingUnits = new List<Unit>();

        private List<Wall> _addingWalls = new List<Wall>();
        private List<Unit> _addingUnits = new List<Unit>();


        public void SetUp()
        {
            // kari
            var wall = new Wall();
            wall.shapePoints = new List<Position>
            {
                Position.Create(-40, 50),
                Position.Create(0, 50),
                Position.Create(40, -50),
                Position.Create(0, -50),
            };
            wall.position = Position.Create(60, 0);
            AddWall(wall);
        }


        override public void Tick(float delta)
        {
            base.Tick(delta);

            UpdateRecoginitoins();

            foreach (var wall in _walls)
                wall.Tick(delta);
            foreach (var unit in _units)
                unit.Tick(delta);

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

            _addingWalls.Add(wall);
        }

        public void RemoveWall(Wall wall)
        {
            if (!_walls.Contains(wall))
                return;

            _removingWalls.Add(wall);
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
                        && unit.IsSame(targetUnit))
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

        // ======================================== adding or removing field objects
        private void RemoveFieldObjects()
        {
            foreach (var removingWall in _removingWalls)
                _walls.Remove(removingWall);
            _removingWalls.Clear();

            foreach (var removnigUnit in _removingUnits)
                _units.Remove(removnigUnit);
            _removingUnits.Clear();
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

            return fieldObjects;
        }
    }
}