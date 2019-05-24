using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenerator : MonoBehaviour
{
    public GameObject[] blocks;
    public int lines;

    // Start is called before the first frame update
    void Start()
    {
        CreateBlockGroup();
    }

    void CreateBlockGroup()
    {
        Bounds blockLimits = blocks[0].GetComponent<SpriteRenderer>().bounds;
        float blockWidth = blockLimits.size.x;
        float blockHeigth = blockLimits.size.y;
        float screenWidth, screenHeigth, widthMultiplier;
        int columns;
        CollectInfoBlock(blockWidth, out screenWidth, out screenHeigth, out columns, out widthMultiplier);
        for (int i = 0; i < lines; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                GameObject randomBlock = blocks[Random.Range(0, blocks.Length)];
                GameObject instanceBlock = (GameObject)Instantiate(randomBlock);
                instanceBlock.transform.position = new Vector3(-(screenWidth * 0.5f) + (j * blockWidth * widthMultiplier), (screenHeigth * 0.5f) - (i * blockHeigth), 0);
                Debug.Log($"pos: {instanceBlock.transform.position}");
                Debug.Log(screenWidth * 0.5f);
                float newWidthBlock = instanceBlock.transform.localScale.x * widthMultiplier;
                instanceBlock.transform.localScale = new Vector3(newWidthBlock, instanceBlock.transform.localScale.y, 1);
            }
        }
    }

    void CollectInfoBlock(float blockWidth, out float screenWidth, out float screenHeight, out int columns, out float widthMultiplier)
    {
        Camera c = Camera.main;
        screenWidth = (c.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)) - c.ScreenToWorldPoint(new Vector3(0, 0, 0))).x;
        Debug.Log($"screen width: {screenWidth}");
        screenHeight = (c.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)) - c.ScreenToWorldPoint(new Vector3(0, 0, 0))).y;
        Debug.Log($"screen heigth: {screenWidth}");
        columns = (int)(screenWidth / blockWidth); // or Mathf.Floor()
        Debug.Log($"columns: {columns}");
        // lengthMultiplier * columns * blockWidth = screenWidth
        // lengthMultiplier = screenWidth/( columns * blockWidth)
        widthMultiplier = screenWidth / (columns * blockWidth);
        Debug.Log($"widthMultiplier: {widthMultiplier}");
    }

}
