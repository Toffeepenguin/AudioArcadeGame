using UnityEngine;
using UnityEngine.SceneManagement;

public class AAS_LevelComplete : MonoBehaviour
{
    // Array of levels in sequence (names of the scenes)
    private string[] levels = { "Level01", "Level02", "Level03", "Credits" };
    private static int active = 0;  // Index of the current level

    // Start is called before the first frame update
    void Start()
    {
        // If the current scene is the first level, reset the active index
        if (SceneManager.GetActiveScene().name == levels[0])
        {
            active = 0;
        }
    }

    // Function to load the current level based on the index
    void Update()
    {
        if (active < levels.Length)
        {
            // Move to the next level after loading
            active += 1;

            // Load the next scene in the sequence
            SceneManager.LoadScene(levels[active]);

        }
        else
        {
            // If all levels are completed, load the credits
            Debug.Log("Game complete! Displaying credits.");
            SceneManager.LoadScene("Credits");  // Load credits scene
        }
    }
}

