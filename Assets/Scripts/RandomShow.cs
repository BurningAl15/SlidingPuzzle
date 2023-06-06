using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class RandomShow : MonoBehaviour
{
    [SerializeField] List<Image> images = new List<Image>();
    [SerializeField] List<Sprite> sprites = new List<Sprite>();

    void Start()
    {
        Shuffle();
        for(int i=0;i<images.Count;i++){
          images[i].sprite=sprites[i];
        }
    }

    void Shuffle(){
      for (int i = 0; i < sprites.Count; i++) {
         Sprite temp = sprites[i];
         int randomIndex = UnityEngine.Random.Range(i, sprites.Count);
         sprites[i] = sprites[randomIndex];
         sprites[randomIndex] = temp;
      }
    }
}
