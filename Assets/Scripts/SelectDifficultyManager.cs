using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;

[Serializable]
public class DifficultyOption{
  public float notSelectedSize, selectedSize;
  public Sprite notSelected, selected;
  public Image image;

  public void SetNormal(){
    SetAppeareance(false);
  }

  public void SetSelected(){
    SetAppeareance(true);
  }

  void SetAppeareance(bool isSelected){
    RectTransform tempRectTransform = image.GetComponent<RectTransform>();
    tempRectTransform.sizeDelta =  Vector2.one * (isSelected ? selectedSize : notSelectedSize);
    image.sprite= isSelected ? selected : notSelected;
  }
}

public class SelectDifficultyManager : MonoBehaviour
{
    public static SelectDifficultyManager _instance;
    [SerializeField] List<DifficultyOption> difficultyOptions = new List<DifficultyOption>();

    void Awake()
    {
        _instance=this;
    }
   
    public void SetCurrentSelected(int index){
      for(int i=0;i<difficultyOptions.Count;i++){
        difficultyOptions[i].SetNormal();
      }
      difficultyOptions[index].SetSelected();
    }
}
