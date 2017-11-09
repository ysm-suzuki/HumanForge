using System.Collections.Generic;

using UnityMVC;

namespace GameMain
{
    public class Player
    {
        public delegate void UnitEventHandler(Unit unit);
        public event UnitEventHandler OnUnitPlaced;
        
        public void SetUp()
        {
            // kari
            const float PI = UnityEngine.Mathf.PI;
            var unit = new Unit(new UnitData
            {
                life = 20,
                attack = 2,
                defence = 5,
                moveSpeed = 10.0f * 10,
                sizeRadius = 10.0f,
                shapePoints = new List<Position>
                {
                    Position.Create(
                        UnityEngine.Mathf.Cos((22.5f + 45 * 0) * PI / 180),
                        UnityEngine.Mathf.Sin((22.5f + 45 * 0) * PI / 180)),
                    Position.Create(
                        UnityEngine.Mathf.Cos((22.5f + 45 * -1) * PI / 180),
                        UnityEngine.Mathf.Sin((22.5f + 45 * -1) * PI / 180)),
                    Position.Create(
                        UnityEngine.Mathf.Cos((22.5f + 45 * -2) * PI / 180),
                        UnityEngine.Mathf.Sin((22.5f + 45 * -2) * PI / 180)),
                    Position.Create(
                        UnityEngine.Mathf.Cos((22.5f + 45 * -3) * PI / 180),
                        UnityEngine.Mathf.Sin((22.5f + 45 * -3) * PI / 180)),
                    Position.Create(
                        UnityEngine.Mathf.Cos((22.5f + 45 * -4) * PI / 180),
                        UnityEngine.Mathf.Sin((22.5f + 45 * -4) * PI / 180)),
                    Position.Create(
                        UnityEngine.Mathf.Cos((22.5f + 45 * -5) * PI / 180),
                        UnityEngine.Mathf.Sin((22.5f + 45 * -5) * PI / 180)),
                    Position.Create(
                        UnityEngine.Mathf.Cos((22.5f + 45 * -6) * PI / 180),
                        UnityEngine.Mathf.Sin((22.5f + 45 * -6) * PI / 180)),
                    Position.Create(
                        UnityEngine.Mathf.Cos((22.5f + 45 * -7) * PI / 180),
                        UnityEngine.Mathf.Sin((22.5f + 45 * -7) * PI / 180)),
                },
                sightRange = 300,
                attackActions = new List<UnitAttackData>
                {
                    new UnitAttackData
                    {
                        power = 1,
                        range = 50,
                        warmUpSeconds = 0.1f,
                        coolDownSeconds = 0.5f
                    }
                }
            });
            unit.position = Position.Create(150, 0);
            unit.isPlayerUnit = true;

            PlaceUnit(unit);
        }

        public void PlaceUnit(Unit unit)
        {
            unit.isOwnedUnit = true;
            unit.isPlayerUnit = true;

            if (OnUnitPlaced != null)
                OnUnitPlaced(unit);
        }
    }
}