using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeMenu : MonoBehaviour
{
    public Scrollbar scrollBar;
    private float scrollpos = 0;
    private float[] pos;

    public int index = 0;
    public int lastIndex = 0;
    private bool justDragged = false;

    [SerializeField] private AnimationCurve animationCurve;
    
    [SerializeField] List<Button> pages= new List<Button>();

    [SerializeField] private Coroutine currentCoroutine = null;

    private bool isCleared = false;
    
    private void Start()
    {
        // if (currentCoroutine == null)
        //     currentCoroutine = StartCoroutine(CallCoroutine(lastIndex));
    }

    private void Update()
    {
        if (!isCleared)
        {
            pos = new float[transform.childCount];
            var distance = 1f / (pos.Length - 1f);
            for (var i = 0; i < pos.Length; i++) pos[i] = distance * i;

            if (Input.GetMouseButton(0))
                scrollpos = scrollBar.value;
            else
                for (var i = 0; i < pos.Length; i++)
                    if (scrollpos < pos[i] + distance / 2 && scrollpos > pos[i] - distance / 2)
                        scrollBar.value =
                            Mathf.Lerp(scrollBar.value, pos[i], .1f);

            for (var i = 0; i < pos.Length; i++)
                if (scrollpos < pos[i] + distance / 2 && scrollpos > pos[i] - distance / 2)
                {
                    transform.GetChild(i).localScale =
                        Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1f, 1f), .1f);
            
                    for (var a = 0; a < pos.Length; a++)
                        if (a != i)
                            transform.GetChild(a).localScale = Vector2.Lerp(transform.GetChild(a).localScale,
                                new Vector2(.8f, .8f), .1f);
                }
        }
    }

    //Should be a coroutine, analyze it
    void Animate()
    {
        //Init
        pos = new float[transform.childCount];
        var distance = 1f / (pos.Length - 1f);
        for (var i = 0; i < pos.Length; i++) pos[i] = distance * i;
        
        //If is not pressing or dragging
        for (var i = 0; i < pos.Length; i++)
            if (scrollpos < pos[i] + distance / 2 && scrollpos > pos[i] - distance / 2)
                scrollBar.value =
                    Mathf.Lerp(scrollBar.value, pos[i], .1f);
        
        //Index stuff
        if (scrollBar.value >= 0 && scrollBar.value < .45f)
            index = 0;
        else if (scrollBar.value >= .45f && scrollBar.value < .95f)
            index = 1;
        else
            index = 2;
        
        if (lastIndex != index)
        {
            //Call something
            lastIndex = index;
            if (currentCoroutine == null)
                currentCoroutine = StartCoroutine(CallCoroutine(lastIndex));
        }
        
        
        //Lerping
        for (var i = 0; i < pos.Length; i++)
            if (scrollpos < pos[i] + distance / 2 && scrollpos > pos[i] - distance / 2)
            {
                transform.GetChild(i).localScale =
                    Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1f, 1f), .1f);
            
                for (var a = 0; a < pos.Length; a++)
                    if (a != i)
                        transform.GetChild(a).localScale = Vector2.Lerp(transform.GetChild(a).localScale,
                            new Vector2(.8f, .8f), .1f);
            }
    }

    IEnumerator CallCoroutine(int _index)
    {
        isCleared = true;
        scrollBar.interactable = false;
        
        pos = new float[transform.childCount];
        var distance = 1f / (pos.Length - 1f);
        for (var i = 0; i < pos.Length; i++) pos[i] = distance * i;

        //Animate hearts
        // yield return StartCoroutine(pages[_index].CallAnimation(animationCurve));
        
        // for (var i = 0; i < pos.Length; i++)
        //     if (scrollpos < pos[i] + distance / 2 && scrollpos > pos[i] - distance / 2)
        //     {
        //         transform.GetChild(i).localScale =
        //             Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1f, 1f), .1f);
        //
        //         for (var a = 0; a < pos.Length; a++)
        //             if (a != i)
        //                 transform.GetChild(a).localScale = Vector2.Lerp(transform.GetChild(a).localScale,
        //                     new Vector2(.8f, .8f), .1f);
        //     }
        yield return null;
        scrollBar.interactable = true;
        
        currentCoroutine = null;
        isCleared = false;
    }
}