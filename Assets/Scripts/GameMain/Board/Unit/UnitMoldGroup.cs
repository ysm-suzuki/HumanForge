using System.Collections.Generic;

using UnityMVC;

namespace GameMain
{
    public class UnitMoldGroup
    {
        private List<UnitMold> _molds = new List<UnitMold>();

        public UnitMoldGroup()
        {
            foreach (var masterData in UnitMasterData.loader.GetAll())
                if (masterData.isBuildable)
                    _molds.Add(masterData.ToUnitMold());
        }
        
        public UnitMoldGroup Filter(System.Func<UnitMold, bool> Match)
        {
            var newList = new List<UnitMold>();
            foreach (var mold in _molds)
                if (Match(mold))
                    newList.Add(mold);
            _molds = newList;

            return this;
        }
        
        public List<UnitMold> molds
        {
            get
            {
                return _molds;
            }
        }
    }
}