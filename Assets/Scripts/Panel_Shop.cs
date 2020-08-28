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

        private void Awake()
        {
            m_TextTitle = transform.Find("Text_Title").GetComponent<Text>();
            m_ButtonClose = transform.Find("Button_Close").GetComponent<Button>();

            m_TextTitle.text = "Welcome to SHOP";
            m_ButtonClose.onClick.AddListener(OnClickClose);
        }

        private void Start()
        {
            TestCreate();
        }

        private void TestCreate()
        {
            for (int i = 0; i < 50; i++)
            {
                InstantiateItem();
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
        private ShopItem InstantiateItem()
        {
            GameObject newObject = Instantiate(m_ShopItemPrefab) as GameObject;
            newObject.transform.SetParent(m_TransformParent);
            ShopItem item = newObject.GetComponent<ShopItem>();
            return item;
        }
    }
}