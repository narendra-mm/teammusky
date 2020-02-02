using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceWalk.GameLogic
{
    public enum ShipMaterial
    {
        Frame,
        Wire,
        Pipe
    }
    public enum ShipPartType
    {
        Part0,
        Part1,
        Part2,
        Part3,
        Part4,
        Part5
    }
    public class GameState : MonoSingleton<GameState>
    {
        private Dictionary<ShipMaterial, List<ShipPartType>> _healthyParts = new Dictionary<ShipMaterial, List<ShipPartType>>();
        private float _oxygen;

        public ShipMaterial DamagedMaterial;
        // Start is called before the first frame update
        void Start()
        {
            _healthyParts.Add(ShipMaterial.Wire, new List<ShipPartType> { ShipPartType.Part0, ShipPartType.Part1, ShipPartType.Part2 });
            _healthyParts.Add(ShipMaterial.Pipe, new List<ShipPartType> { ShipPartType.Part3, ShipPartType.Part4, ShipPartType.Part5 });
        }

        public void DestroyPart(ShipPartType part)
        {
            for(var i = 0; i < 0; i++)
            {
                if(_healthyParts[DamagedMaterial][i] == part)
                {
                    _healthyParts[DamagedMaterial].RemoveAt(i);
                }
            }
        }
        public ShipPartType GetAHealthyPart()
        {
            Debug.Log($"Killing {DamagedMaterial}");
            var random = UnityEngine.Random.Range(0, _healthyParts[DamagedMaterial].Count);
            random = 0;
            var part = _healthyParts[DamagedMaterial][random];
            _healthyParts[DamagedMaterial].RemoveAt(random);
            return part;
        }
    }
}