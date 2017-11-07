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

        override public void Tick(float delta)
        {
            base.Tick(delta);


            foreach (var wall in _walls)
                wall.Tick(delta);
            foreach (var unit in _units)
                unit.Tick(delta);

            SolveMoving();

            foreach (var removingWall in _removingWalls)
                _walls.Remove(removingWall);
            _removingWalls.Clear();

            foreach (var removnigUnit in _removingUnits)
                _units.Remove(removnigUnit);
            _removingUnits.Clear();
        }



        public void AddUnit(Unit unit)
        {
            if (_units.Contains(unit))
                return;
            foreach (var aUnit in _units)
                if (unit.IsSame(aUnit))
                    return;

            _units.Add(unit);

            if (OnUnitAdded != null)
                OnUnitAdded(unit);
        }

        public void RemoveUnit(Unit unit)
        {
            _removingUnits.Add(unit);
        }

        public void AddWall(Wall wall)
        {
            if (_walls.Contains(wall))
                return;
            foreach (var aWall in _walls)
                if (wall == aWall)
                    return;

            _walls.Add(wall);

            if (OnWallAdded != null)
                OnWallAdded(wall);
        }

        public void RemoveWall(Wall wall)
        {
            _removingWalls.Add(wall);
        }


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