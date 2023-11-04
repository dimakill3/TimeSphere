using System;
using Game;
using UnityEngine;

namespace Infrastructure
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private Gun[] guns;
        
        private Game _game;

        private void Start()
        {
            Initialize();
            Activate();
        }

        private void OnDestroy() => 
            _game.Destruct();

        private void Initialize()
        {
            _game = new Game(guns);
            _game.Initialize();
        }

        private void Activate() => 
            _game.Activate();
    }
}