using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore;
using TMPro;

public class Timer : MonoBehaviour
{
    public float remainingTime = 5;

    [SerializeField]
    private TextMeshProUGUI txtMesh;
    [SerializeField]


    public GameObject startMenuButton;

    private int finalScore;

    // Start is called before the first frame update
    void Start()
    {
        startMenuButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            txtMesh.text = "Time: " + (int)remainingTime;
        }

        if (remainingTime < 0)
        {
            // finalScore = CalculateFinalScore();
            // txtMesh.text = "Final Score: " + finalScore;
            startMenuButton.gameObject.SetActive(true);
        }
    }
}