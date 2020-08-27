using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SPStudios.Tools;

namespace CommonShop
{
    public class PopupManager : MonoSingleton<PopupManager>
    {
        private Panel_Shop m_Shop;
        [SerializeField] private GameObject m_PrefabShop;
        [SerializeField] private Transform m_TransformParent;

        private void Awake()
        {
            
        }

        public Panel_Shop ShowShop()
        {
            GameObject newObject = Instantiate(m_PrefabShop) as GameObject;
            newObject.transform.SetParent(m_TransformParent);
            //newObject.transform.SetPositionAndRotation(Vector2.zero, Quaternion.identity);

            RectTransform rt = newObject.transform as RectTransform;
            rt.anchoredPosition = Vector2.zero;

            m_Shop = newObject.transform.GetComponent<Panel_Shop>();
            return m_Shop;
        }

        public void CloseShop()
        {
            Destroy(m_Shop.gameObject);
        }
    }
}