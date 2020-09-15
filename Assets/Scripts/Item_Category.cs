using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CommonShop
{
    public class CategoryData
    {
        public List<ItemData> dataList;
    }

    public enum ShopCategory
    {
        AAA = 0,
        BBB,
        CCC,
        DDD
    }

    public class Item_Category : MonoBehaviour
    {
        private ShopCategory m_Category;
        private Button m_Button;
        private Text m_Text;

        public Button CategoryButton
        {
            get { return m_Button; }
        }

        private void Awake()
        {
            m_Button = transform.GetComponent<Button>();
            m_Text = transform.Find("Text").GetComponent<Text>();
        }

        public void Setup(ShopCategory category)
        {
            m_Category = category;

            string content = string.Empty;
            switch (m_Category)
            {
                case ShopCategory.AAA:
                    content = "AAA";
                    break;
                case ShopCategory.BBB:
                    content = "BBB";
                    break;
                case ShopCategory.CCC:
                    content = "CCC";
                    break;
                case ShopCategory.DDD:
                    content = "DDD";
                    break;
                default:
                    break;
            }
            m_Text.text = content;
        }

    }
}