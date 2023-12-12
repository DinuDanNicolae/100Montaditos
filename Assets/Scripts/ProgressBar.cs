using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Slider slider;

    // Set this to the maximum score you want to achieve
    public int maxScore = 20;

    // Start is called before the first frame update
    void Start()
    {
        // Assuming the slider is attached to the same GameObject as this script
        slider = GetComponent<Slider>();
        UpdateSliderValue(0);
    }

    public void UpdateSliderValue(int score)
    {
        // Ensure the score doesn't exceed the maximum
        // score = Mathf.Min(score, maxScore);

        // Calculate the percentage based on the current score and the maximum score
        float percentage = (float)score % maxScore;

        // Update the slider value
        slider.value = percentage;
    }
}
