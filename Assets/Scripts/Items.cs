using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Items : MonoBehaviour
{
    [SerializeField] private float cooldownTime = 3f;
    [SerializeField] private float rotationSpeed = 25f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Image itemImageHolder;
    [SerializeField] private TMP_Text messageField;
    private List<Color> items = new List<Color>();
    private int activeItemIndex = -1;
    

    void Update()
    {
        CycleItems();
        UseItem();
    }

    IEnumerator ShowMessage(string message)
    {
        messageField.enabled = true;
        messageField.text = message;
        yield return new WaitForSeconds(3f);
        messageField.enabled = false;
    }

    private void OnTriggerEnter(Collider other) // deel van PickUp script
    {
        if (other.gameObject.CompareTag("Item"))
        {
            PickUpItem(other.gameObject);
        }
    }
    void PickUpItem(GameObject item)
    { // Picking up items, PickUp script

        Color color = item.gameObject.GetComponent<Renderer>().material.color;

        Destroy(item);

        items.Add(color);

        activeItemIndex = items.Count - 1;

        itemImageHolder.color = items[activeItemIndex];
        itemImageHolder.enabled = true;
    }

    void UseItem() // Use Item script
    {

        if (Input.GetKeyDown(KeyCode.E) && items.Count > 0 && activeItemIndex != -1)
        {

            if (items[activeItemIndex] == Color.blue)
            {
                StartCoroutine(ShowMessage(" +  Move Speed"));
                moveSpeed += 5;
            }
            else if (items[activeItemIndex] == Color.red)
            {
                StartCoroutine(ShowMessage(" + Fire Rate"));
                cooldownTime -= 0.1f;
            }
            else if (items[activeItemIndex] == Color.green)
            {
                StartCoroutine(ShowMessage(" + Rotation Speed"));
                rotationSpeed += 10;
            }
            items.RemoveAt(activeItemIndex);
            if (activeItemIndex > 0)
            {
                activeItemIndex--;
                itemImageHolder.color = items[activeItemIndex];
            }
            else if (items.Count == 0)
            {
                itemImageHolder.color = Color.white;
                activeItemIndex = -1;
                itemImageHolder.enabled = false;
            }

        }
    }

    void CycleItems()
    { // Cycling through items, ItemsScript
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (items.Count > 0)
            {
                if (activeItemIndex < items.Count - 1)
                {
                    activeItemIndex++;
                }
                else
                {
                    activeItemIndex = 0;
                }
                itemImageHolder.color = items[activeItemIndex];
            }
            else
            {
                itemImageHolder.color = Color.white;
                activeItemIndex = -1;
                itemImageHolder.enabled = false;
            }
        }
    }
}
