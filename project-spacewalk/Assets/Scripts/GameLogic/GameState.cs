using System.Collections.Generic;
using UnityEngine;

namespace SpaceWalk.GameLogic
{
    public enum DamagablePart
    {
        Wire0,
        Wire1,
        Wire2,
        Pipe0,
        Pipe1,
        Pipe2
    }
    public class GameState : MonoSingleton<GameState>
    {
        private List<DamagablePart> _healthyParts = new List<DamagablePart>();
        private float _oxygen;

        // Start is called before the first frame update
        void Start()
        {
            foreach (DamagablePart pieceType in DamagablePart.GetValues(typeof(DamagablePart)))
            {
                _healthyParts.Add(pieceType);
            }
        }

        public void DestroyPart(DamagablePart part)
        {
            for(var i = 0; i < 0; i++)
            {
                if(_healthyParts[i] == part)
                {
                    _healthyParts.RemoveAt(i);
                }
            }
        }
        public DamagablePart GetAHealthyPart()
        {
            var random = Random.Range(0, _healthyParts.Count);
            var part = _healthyParts[random];
            _healthyParts.RemoveAt(random);
            return part;
        }
    }
}