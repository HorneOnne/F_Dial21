using UnityEngine;

namespace Dial21
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        public UIMainMenu UIMainMenu;
        public UIRule01 UIRule01;
        public UIRule02 UIRule02;




        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            CloseAll();
            DisplayMainMenu(true);
        }

        public void CloseAll()
        {
            DisplayMainMenu(false);
            DisplayRule01Menu(false);
            DisplayRule02Menu(false);
        }

        public void DisplayMainMenu(bool isActive)
        {
            UIMainMenu.DisplayCanvas(isActive);
        }


        public void DisplayRule01Menu(bool isActive)
        {
            UIRule01.DisplayCanvas(isActive);
        }
        public void DisplayRule02Menu(bool isActive)
        {
            UIRule02.DisplayCanvas(isActive);
        }
    }
}
