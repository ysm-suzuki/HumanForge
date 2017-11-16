using System.Collections.Generic;

using UnityMVC;

namespace GameMain
{
    public class FaceMoldGroup
    {
        private List<FaceMold> _molds = new List<FaceMold>();

        public FaceMoldGroup()
        {
            foreach (var masterData in FaceMasterData.loader.GetAll())
                _molds.Add(masterData.ToFaceMold());
        }
        
        public FaceMoldGroup Filter(System.Func<FaceMold, bool> Match)
        {
            var newList = new List<FaceMold>();
            foreach (var mold in _molds)
                if (Match(mold))
                    newList.Add(mold);
            _molds = newList;

            return this;
        }
        
        public List<FaceMold> molds
        {
            get
            {
                return _molds;
            }
        }
    }
}