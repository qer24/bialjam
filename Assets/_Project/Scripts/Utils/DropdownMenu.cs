using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DropdownMenu : MonoBehaviour
{
    [Serializable]
    public class MenuItem
    {
        public string label;
        public UnityEvent onPress;
        public UnityEvent<Button> onCreate;
    }

    public Transform content;
    public List<int> emptySpacesIndexes;
    public MenuItem[] menuItems;

    private Button template;
    private GameObject emptySpace;

    private bool opened = false;

    private List<GameObject> dropdownGameObjects;

    private void Awake()
    {
        if (content.childCount > 2) Debug.LogError("Dropdown Menu content shouldn't have more than 2 child at start!");
        template = content.GetChild(0).GetComponent<Button>();
        emptySpace = content.GetChild(1).gameObject;
        
        template.gameObject.SetActive(false);
        emptySpace.gameObject.SetActive(false);
        
        CreateDropdownButtons();
        
        content.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!opened) return;
        
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            if (!IsOverDropdown())
            {
                ToggleDropdown();
            }
        }
    }

    private bool IsOverDropdown()
    {
        var pointerData = new PointerEventData(EventSystem.current) {
            pointerId = -1,
            position = Mouse.current.position.ReadValue()
        };
            
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);
        
        return results.Any(result => dropdownGameObjects.Contains(result.gameObject));
    }

    private void CreateDropdownButtons()
    {
        dropdownGameObjects = new List<GameObject>
        {
            gameObject
        };

        for (var i = 0; i < menuItems.Length; i++)
        {
            var menuItem = menuItems[i];
            var menuButton = Instantiate(template, content);
            menuButton.GetComponentInChildren<TextMeshProUGUI>().text = menuItem.label;
            menuButton.onClick.AddListener(() => menuItem.onPress.Invoke());
            menuButton.onClick.AddListener(() => {
                if (opened) ToggleDropdown();
            });

            menuButton.gameObject.SetActive(true);
            
            menuItem.onCreate?.Invoke(menuButton);
            
            dropdownGameObjects.Add(menuButton.gameObject);

            if (emptySpacesIndexes.Contains(i))
            {
                var empty = Instantiate(emptySpace, content);
                empty.SetActive(true);
                dropdownGameObjects.Add(empty);
            }
        }
    }

    public void ToggleDropdown()
    {
        opened = !opened;
        content.gameObject.SetActive(opened);
    }
}