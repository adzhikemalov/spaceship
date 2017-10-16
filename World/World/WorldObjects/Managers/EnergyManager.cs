using System.Security.Cryptography.X509Certificates;
using Assets.World.Areas;

namespace World.World.WorldObjects.Managers
{
    public class EnergyManager
    {
        private int _maxEnergy;
        private int _energy;
        private int _availableEnergy;

        public void Init(int energy, int maxEnergy)
        {
            _energy = energy;
            _maxEnergy = maxEnergy;
            _availableEnergy = _energy;
        }

        public void RequestEnergy(AreaBase area)
        {
            if (_availableEnergy > 0 && area.CanAddEnergy())
            {
                _availableEnergy--;
                area.AddEnergy();
            }
        }

        public void GrabEnergy(AreaBase area)
        {
            if (area.Energy > 0)
            {
                _availableEnergy++;
                area.RemoveEnergy();
            }
        }
    }
}