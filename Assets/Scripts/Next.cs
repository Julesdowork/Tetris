/*
 * Copyright (c) Julian McNeill
 * http://julianmcneill.com
*/
using UnityEngine;

public class Next : MonoBehaviour
{
    public Sprite[] sprites;
    public int indexToSpawn;

    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        GetNextGroup();
    }

    public void GetNextGroup()
    {
        indexToSpawn = Random.Range(0, sprites.Length);

        spriteRenderer.sprite = sprites[indexToSpawn];
    }
}