using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommonShop
{
    [Serializable]
    public class ItemData
    {
        public int id;
        public ShopCategory category;        
        public string name_en;
        public string sprite_name;
        public double coin_buy;
        public bool is_locked;
    }

    [CreateAssetMenu(fileName = "ShopData", menuName = "Scriptable Object Asset/ShopData")]
    public class ShopData : ScriptableObject
    {
        public List<ItemData> m_ItemDataList = new List<ItemData>();
    }
}