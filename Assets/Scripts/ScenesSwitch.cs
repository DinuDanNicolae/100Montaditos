using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickableObject : MonoBehaviour
{
    // Name of the scene to load when clicked
    [SerializeField]
    public string sceneToLoad;

    // Update is called once per frame
    void Update()
    {
        // Check if the left mouse button is clicked
        if (Input.GetMouseButtonDown(0))
        {
            // Cast a ray from the mouse position into the scene
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            // Check if the ray hits this object
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                // Handle the click event (change the scene based on the object's sceneToLoad variable)
                ChangeScene();
            }
        }
    }

    // Change the scene based on the object's sceneToLoad variable
    void ChangeScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}