using System.Collections.Generic;
using World.Map;

namespace Assets.World.Areas
{
    public class AreaBase
    {
        private int _energy;
        private int _maxEnergy;
        private int _energyNeeded;
        private int _hp;
        private bool _isWorking;

        public int Energy
        {
            get { return _energy; }
        }
        public int MaxEnergy
        {
            get { return _maxEnergy; }
        }
        public int EnergyNeeded
        {
            get { return _energyNeeded; }
        }
        public int Hp
        {
            get { return _hp; }
        }
        public bool IsWorking
        {
            get { return _isWorking; }
        }

        private List<CellModel> _areaCells; 

        public virtual void Update()
        {
            
        }

        public void AddEnergy()
        {
            if (_energy < _maxEnergy)
                _energy++;
        }

        public void RemoveEnergy()
        {
            if (_energy > 0)
                _energy--;
        }

        public bool CanAddEnergy()
        {
            return _energy < _maxEnergy;
        }
    }
}