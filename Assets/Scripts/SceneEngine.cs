using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Unity.UI;
using UnityEngine.SceneManagement;

public class SceneEngine: MonoBehaviour
{
    public GameObject enterButton;
    // Start is called before the first frame update
    void Start()
    {
        if (enterButton != null)
        {
            enterButton.SetActive(false);
            //SceneManager.LoadScene(0);
            StartCoroutine(EnterCoroutine());
        }
    }

    private IEnumerator EnterCoroutine()
    {
        yield return new WaitForSeconds(33);
        enterButton.SetActive(true);
    }

    public void OnClick()
    {
        Debug.Log("Clicked");
        SceneManager.LoadSceneAsync(1);
    }
}
