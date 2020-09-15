using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CommonShop
{
    public class Panel_Shop : MonoBehaviour
    {
        private Text m_TextTitle;
        private Button m_ButtonClose;
        [SerializeField] private GameObject m_ShopItemPrefab;
        [SerializeField] private Transform m_TransformParent;

        private List<CategoryData> m_CategoryDataList = new List<CategoryData>();
        private List<Transform> m_TransformContent = new List<Transform>();
        
        private void Awake()
        {
            m_TextTitle = transform.Find("Text_Title").GetComponent<Text>();
            m_ButtonClose = transform.Find("Button_Close").GetComponent<Button>();

            m_TextTitle.text = "Welcome to SHOP";
            m_ButtonClose.onClick.AddListener(OnClickClose);
        }

        private void Start()
        {
            //InitData();

            TestCreate();
        }

        private void InitData()
        {
            CategoryData AAA = new CategoryData();
            AAA.dataList = new List<ItemData>();

            List<ItemData> shopItemData = ResourcesLoader.Instance.m_ShopData.m_ItemDataList;
            //List<Sprite> shopSprites = ResourcesLoader.Instance.m_ShopSpritesList;

            foreach (ItemData data in shopItemData)
            {
                switch (data.category)
                {
                    case ShopCategory.AAA:
                        m_CategoryDataList[0].dataList.Add(data);
                        break;
                    case ShopCategory.BBB:
                        m_CategoryDataList[1].dataList.Add(data);
                        break;
                    case ShopCategory.CCC:
                        m_CategoryDataList[2].dataList.Add(data);
                        break;
                    case ShopCategory.DDD:
                        m_CategoryDataList[3].dataList.Add(data);
                        break;
                    default:
                        break;
                }
            }
        }

        private void CreateItem()
        {
            foreach (ItemData data in m_CategoryDataList[0].dataList)
            {
                ShopItem item = InstantiateItem(m_TransformContent[0]);
                item.SetData(data);
                //item.SetSprite(sprite);
            }
        }

        private void TestCreate()
        {
            for (int i = 0; i < ResourcesLoader.Instance.m_ShopSpritesList.Count; i++)
            {
                ShopItem item = InstantiateItem(m_TransformParent);

                ItemData data = ResourcesLoader.Instance.m_ShopData.m_ItemDataList[i];
                Sprite sprite = ResourcesLoader.Instance.m_ShopSpritesList[i];
                item.SetData(data);
                item.SetSprite(sprite);
            }
        }

        private void OnClickClose()
        {
            //Destroy(this.gameObject);
            PopupManager.Instance.CloseShop();
        }

        /// <summary>
        /// 상점 아이템을 instantiate해서 새롭게 생성하고 ShopItem을 반환한다.
        /// </summary>
        /// <returns></returns>
        private ShopItem InstantiateItem(Transform parent)
        {
            GameObject newObject = Instantiate(m_ShopItemPrefab) as GameObject;
            newObject.transform.SetParent(parent);
            ShopItem item = newObject.GetComponent<ShopItem>();
            return item;
        }
    }
}