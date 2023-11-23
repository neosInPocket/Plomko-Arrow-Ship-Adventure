using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScreenBounds : MonoBehaviour
{
    [SerializeField] private PlayerSavesLoad playerSavesLoad;
    
    [SerializeField] private SpriteRenderer leftBorderRenderer;
    [SerializeField] private SpriteRenderer rightBorderRenderer;
    [SerializeField] private BoxCollider2D leftBorder;
    [SerializeField] private BoxCollider2D rightBorder;
    
    [SerializeField] private BoxCollider2D bottomBorder;

    private void Start()
    {
        var screenSize = playerSavesLoad.Data.screenSize;

        float borderHeight = 2 * screenSize.y;

        float leftBorderPosition = -screenSize.x - leftBorderRenderer.size.x / 2;
        float rightBorderPosition = screenSize.x + rightBorderRenderer.size.x / 2;

        leftBorderRenderer.size = new Vector2(leftBorderRenderer.size.x, borderHeight);
        rightBorderRenderer.size = new Vector2(rightBorderRenderer.size.x, borderHeight);

        leftBorder.transform.position = new Vector2(leftBorderPosition, 0);
        rightBorder.transform.position = new Vector2(rightBorderPosition, 0);
        
        leftBorder.size = leftBorderRenderer.size;
        rightBorder.size = rightBorderRenderer.size;

        var bottomSize = bottomBorder.size;
        bottomSize.x = screenSize.x * 2;
        bottomBorder.size = bottomSize;

        bottomBorder.transform.position = new Vector2(0, -screenSize.y - bottomBorder.size.y / 2);
    }
}
