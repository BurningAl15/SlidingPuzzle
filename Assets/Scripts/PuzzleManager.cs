using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


[Serializable]
public class DifficultyPuzzleManager
{
    public GameObject difficulty;
    public List<PositionSetter> positions = new List<PositionSetter>();
    public List<TileInteraction> tileInteractions = new List<TileInteraction>();
}

public class PuzzleManager : MonoBehaviour
{
    private int[] answerMatrix_2x2 = new int[]
    {
        0,1,2,3
    };

    private int[] answerMatrix_3x3 = new int[]
    {
        0, 1, 2, 3, 4, 5, 6, 7, 8
    };
    
    private int[] answerMatrix_4x4 = new int[]
    {
        0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10,11,12,13,14,15
    };

    private int[] tempMatrix;

    [SerializeField] private List<PositionSetter> positions = new List<PositionSetter>();
    [SerializeField] private List<TileInteraction> _tileInteractions = new List<TileInteraction>();
    private int counter = 0;

    private int movementsPerformed = 0;

    private GameObject turnOffTile;

    [SerializeField] private Character testingCharacter;

    [SerializeField] private int blocks;

    [SerializeField] private List<DifficultyPuzzleManager> difficulty = new List<DifficultyPuzzleManager>();

    void TurnOffList(int index)
    {
        for(int i=0;i<difficulty.Count;i++)
            difficulty[i].difficulty.SetActive(false);
        difficulty[index].difficulty.SetActive(true);
    }
    
    public IEnumerator Initialize()
    {
        int tempIndex = 0;

        blocks = CharacterSelected._instance.Difficulty;
        
        int shuffleIndex = blocks * blocks;
        switch (blocks)
        {
            case 0:
            case 1:
            case 2:
                tempIndex = 0;
                TurnOffList(tempIndex);
                tempMatrix = answerMatrix_2x2;
                break;
            case 3:
                tempIndex = 1;
                TurnOffList(tempIndex);
                tempMatrix = answerMatrix_3x3;
                break;
            case 4:
                tempIndex = 2;
                TurnOffList(tempIndex);
                tempMatrix = answerMatrix_4x4;
                break;
        }

        positions = difficulty[tempIndex].positions;
        _tileInteractions = difficulty[tempIndex].tileInteractions;
        
        //All should be Turned On
        for (int i = 0; i < positions.Count; i++)
        {
            positions[i].TurnOffCollider();
            _tileInteractions[i].TurnOffCollider();
        }

        yield return null;
        
        Character temp = CharacterSelected._instance == null
            ? testingCharacter : CharacterSelected._instance.CurrentCharacter;

        if(CharacterSelected._instance == null)
            print("Entering in test mode");
        
        for(int i = 0;i<_tileInteractions.Count;i++){
            // _tileInteractions[i].InitSprite(CharacterSpriteManager._instance.GetSprites(temp)[i]);
            _tileInteractions[i].InitTexture(CharacterSpriteManager._instance.GetSpritesFromTexture_3D(temp,blocks)[i]);
        }
        
        for (int i = 0; i < _tileInteractions.Count; i++)
            _tileInteractions[i].OnCheckAnswer += CheckAnswer;

        UIManager._instance.UpdatePerformedMoves(movementsPerformed);

        //Shuffle the tiles
        List<int> randomPositions = new List<int>();
        int number;
        int test = 0;
        for (int i = 0; i < shuffleIndex; i++)
        {
            do {
                number = Random.Range(0,shuffleIndex);
            } while (randomPositions.Contains(number));
            randomPositions.Add(number);
            test++;
            print(test + " - Test");
        }
        
        for (int i = 0; i < shuffleIndex; i++)
        {
            int randomValue = randomPositions[i];
            _tileInteractions[i].transform.position = positions[randomValue].transform.position;
        }
        
        //Randomly Turn off one of the tiles
        turnOffTile = _tileInteractions[Random.Range(0, _tileInteractions.Count)].gameObject;
        turnOffTile.SetActive(false);

        for (int i = 0; i < positions.Count; i++)
        {
            positions[i].TurnOnCollider();
            print(i + " - b");            
        }
        
        for (int i = 0; i < _tileInteractions.Count; i++)
        {
            if (_tileInteractions[i].gameObject.activeInHierarchy)
            {
                _tileInteractions[i].TurnOnCollider();
                print(i);                
            }
        }
    }
    
    void CheckAnswer()
    {
        counter = 0;
        
        for (int i = 0; i < _tileInteractions.Count; i++)
        {
            if (_tileInteractions[i].CurrentPosition == tempMatrix[i])
                counter++;
        }

        MovementsPerformed();
        
        //End Game
        if (counter >= _tileInteractions.Count - 1)
        {
            GameState._instance.GameState_End();
            for (int i = 0; i < positions.Count; i++)
            {
                PositionSetter temp = positions[i];
                turnOffTile.SetActive(true);
                if (!temp.isFull)
                    turnOffTile.transform.position = temp.transform.position;
            }
        }
        
        print("Current counter: " + counter);
    }

    void MovementsPerformed()
    {
        movementsPerformed++;
        UIManager._instance.UpdatePerformedMoves(movementsPerformed);
    }
}
