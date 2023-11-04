using Game;

namespace Infrastructure
{
    public class Game
    {
        private Gun[] _guns;
        
        public Game(Gun[] guns) => 
            _guns = guns;

        public void Initialize()
        {
            foreach (var gun in _guns) 
                gun.Initialize();
        }

        public void Activate()
        {
            foreach (var gun in _guns) 
                gun.Activate();
        }

        public void Destruct()
        {
            foreach (var gun in _guns) 
                gun.Destruct();
        }
    }
}