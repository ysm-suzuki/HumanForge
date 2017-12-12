using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using UnityMVC;

namespace GameMain
{
    public class PlayerManaConditionView : View
    {
        [SerializeField]
        private Text _redManaText;
        [SerializeField]
        private Text _greenManaText;
        [SerializeField]
        private Text _blueManaText;

        private static string PrefabPath = "Prefabs/GameMain/Board/UI/PlayerManaCondition";

        new private Player _model;

        public static PlayerManaConditionView Attach(GameObject parent)
        {
            var view = View.Attach<PlayerManaConditionView>(PrefabPath);
            view.SetParent(parent);
            return view;
        }

        public PlayerManaConditionView SetModel(Player model)
        {
            _model = model;

            _model.OnManaUpdated += mana => 
            {
                UpdateManaStatus(mana);
            };

            foreach (var mana in _model.allMana)
                UpdateManaStatus(mana);
            
            return this;
        }


        private void UpdateManaStatus(Mana mana)
        {
            var textUiMap = new Dictionary<ManaData.Type, Text>
            {
                { ManaData.Type.Red, _redManaText},
                { ManaData.Type.Green, _greenManaText},
                { ManaData.Type.Blue, _blueManaText},
            };

            textUiMap[mana.type].text = "[ " + mana.amount + " / " + mana.max + " ]";
        }
    }
}