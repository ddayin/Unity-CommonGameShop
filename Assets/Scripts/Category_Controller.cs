using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommonShop
{
    public class Category_Controller : MonoBehaviour
    {
        [SerializeField] private List<Item_Category> m_CategoryList = new List<Item_Category>();
        [SerializeField] private List<RectTransform> m_ScrollList = new List<RectTransform>();

        private void Awake()
        {
            m_CategoryList[0].Setup(ShopCategory.AAA);
            m_CategoryList[1].Setup(ShopCategory.BBB);
            m_CategoryList[2].Setup(ShopCategory.CCC);
            m_CategoryList[3].Setup(ShopCategory.DDD);

            m_CategoryList[0].CategoryButton.onClick.AddListener(OnClickAAA);
            m_CategoryList[1].CategoryButton.onClick.AddListener(OnClickBBB);
            m_CategoryList[2].CategoryButton.onClick.AddListener(OnClickCCC);
            m_CategoryList[3].CategoryButton.onClick.AddListener(OnClickDDD);
        }

        private void OnClickAAA()
        {
            
        }

        private void OnClickBBB()
        {

        }

        private void OnClickCCC()
        {

        }

        private void OnClickDDD()
        {

        }
    }
}