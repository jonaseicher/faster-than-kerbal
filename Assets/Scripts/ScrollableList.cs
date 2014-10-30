using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ScrollableList : MonoBehaviour
{
    public GameObject itemPrefab;
    int columnCount = 1;
    GameWidgetManager gameWidget;

    void Awake()
    {
        gameWidget = itemPrefab.GetComponent<GameWidgetManager>();
        
    }


    public void ShowGames(RoomInfo[] rooms)
    {
        int itemCount = rooms.Length;
        RectTransform rowRectTransform = itemPrefab.GetComponent<RectTransform>();
        RectTransform containerRectTransform = gameObject.GetComponent<RectTransform>();

        //calculate the width and height of each child item.
        float width = containerRectTransform.rect.width / columnCount;
        float ratio = width / rowRectTransform.rect.width;
        float height = rowRectTransform.rect.height * ratio;
        int rowCount = itemCount / columnCount;
        if (itemCount % rowCount > 0)
            rowCount++;

        //adjust the height of the container so that it will just barely fit all its children
        float scrollHeight = height * rowCount;
        containerRectTransform.offsetMin = new Vector2(containerRectTransform.offsetMin.x, -scrollHeight / 2);
        containerRectTransform.offsetMax = new Vector2(containerRectTransform.offsetMax.x, scrollHeight / 2);
        int i = 0;
        foreach (RoomInfo roomInfo in rooms)
        {
            i++;
            //create a new item, name it, and set the parent
            GameObject newItem = Instantiate(itemPrefab) as GameObject;
            newItem.name = gameObject.name + " item at (" + i + ")";
            newItem.transform.parent = gameObject.transform;

            //move and size the new item
            RectTransform rectTransform = newItem.GetComponent<RectTransform>();

            float x = -containerRectTransform.rect.width / 2 + width * (i % columnCount);
            float y = containerRectTransform.rect.height / 2;
            rectTransform.offsetMin = new Vector2(x, y);

            x = rectTransform.offsetMin.x + width;
            y = rectTransform.offsetMin.y + height;
            rectTransform.offsetMax = new Vector2(x, y);

            // add data to the room widget
            gameWidget.MaxPlayers.text = roomInfo.maxPlayers.ToString();
            gameWidget.GameName.text = roomInfo.name;
            gameWidget.CurrentPlayers.text = roomInfo.playerCount.ToString();
            gameWidget.JoinButton.enabled = roomInfo.playerCount < roomInfo.maxPlayers;
  
        }


    }

}
