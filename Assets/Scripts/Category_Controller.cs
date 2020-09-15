using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommonShop
{
    public class Category_Controller : MonoBehaviour
    {
        [SerializeField] private List<Item_Category> m_CategoryList = new List<Item_Category>();

        private void Awake()
        {
            m_CategoryList[0].Setup(ShopCategory.AAA);
            m_CategoryList[1].Setup(ShopCategory.BBB);
            m_CategoryList[2].Setup(ShopCategory.CCC);
            m_CategoryList[3].Setup(ShopCategory.DDD);

            m_CategoryList[0].CategoryButton.onClick.AddListener(OnClickAAA);
        }

        private void OnClickAAA()
        {
            
        }
    }
}