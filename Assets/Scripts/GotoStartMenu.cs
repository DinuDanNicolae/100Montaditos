using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoStartMenu : MonoBehaviour
{
    private Vector3 originalScale;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    private void OnMouseEnter()
    {
        transform.localScale = originalScale * 1.1f; // Increase scale by 20%
    }

    private void OnMouseExit()
    {
        transform.localScale = originalScale; // Reset scale to original size
    }

    private void OnMouseDown()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
