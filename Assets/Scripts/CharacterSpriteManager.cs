using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Character
{
    MrT,
    Blade,
    CoolGuy,
    Franky,
    Rocky,
    SamLJackson,
    Doc,
    None
}

[Serializable]
public class CharacterSpriteList
{
    public Character character;
    public List<Sprite> sprites = new List<Sprite>();
    public Texture2D texture;
}

public class CharacterSpriteManager : MonoBehaviour
{
    public static CharacterSpriteManager _instance;

    [SerializeField] private List<CharacterSpriteList> _spriteLists = new List<CharacterSpriteList>();

    private Dictionary<Character, List<Sprite>> _characterSpritesDictionary =
        new Dictionary<Character, List<Sprite>>();

    private Dictionary<Character, Texture2D> _characterSpriteDictionary =
        new Dictionary<Character, Texture2D>();

    private void Awake()
    {
        if (_instance == null)
            _instance = this;

        for (int i = 0; i < _spriteLists.Count; i++)
            _characterSpritesDictionary.Add((Character) i, _spriteLists[i].sprites);

        for (int i = 0; i < _spriteLists.Count; i++)
            _characterSpriteDictionary.Add((Character) i, _spriteLists[i].texture);
    }

    public List<Sprite> GetSprites(Character _character)
    {
        List<Sprite> temp = new List<Sprite>();
        if (_characterSpritesDictionary.TryGetValue(_character, out temp))
            return temp;

        print("Error In Color Manager - GetColorValue Method");
        return null;
    }

    public List<Sprite> GetSpritesFromTexture(Character _character)
    {
        Texture2D temp = null;
        List<Texture2D> tempList = new List<Texture2D>();
        Texture2D[,] imageSlices = new Texture2D[3, 3];
        List<Sprite> tempSprites = new List<Sprite>();
        if (_characterSpriteDictionary.TryGetValue(_character, out temp))
        {
            imageSlices = ImageSlicer.GetSlices(temp, 3);
        }

        if (temp != null)
        {
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                    tempList.Add(imageSlices[x, y]);
            }

            int size = 2;

            for (int i = 0; i < tempList.Count; i++)
            {
                tempList[i].Resize(size, size);
                tempList[i].Apply();
            }

            for (int i = 0; i < tempList.Count; i++)
            {
                Rect rec = new Rect(0, 0, size,size);
                print("Width and Height: " + tempList[i].width + " - "+ tempList[i].height);
                tempSprites.Add(Sprite.Create(tempList[i], rec, new Vector2(0, 0), 1));
            }

            return tempSprites;
        }

        print("Error In Color Manager - GetColorValue Method");
        return null;
    }
    
    public List<Texture2D> GetSpritesFromTexture_3D(Character _character,int blocks)
    {
        Texture2D temp = null;
        List<Texture2D> tempList = new List<Texture2D>();
        Texture2D[,] imageSlices = new Texture2D[blocks, blocks];
        if (_characterSpriteDictionary.TryGetValue(_character, out temp))
            imageSlices = ImageSlicer.GetSlices(temp, blocks);

        if (temp != null)
        {
            for (int y = 0; y < blocks; y++)
            {
                for (int x = 0; x < blocks; x++)
                    tempList.Add(imageSlices[x, y]);
            }

            return tempList;
        }

        print("Error In Color Manager - GetColorValue Method");
        return null;
    }

}
