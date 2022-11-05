using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
    using UnityEditor;
#endif
using TMPro;

public class MenuUIHandler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI bestScoreText;
    int bestScore;
    string playerName;
    [SerializeField] TMP_InputField nameInputField;


    // Start is called before the first frame update
    void Start()
    {
        bestScore = MainManagerMenu.Instance.LoadBestScore();
        playerName = MainManagerMenu.Instance.LoadPlayerName();

        // initializing the best score text.
        bestScoreText.text = "Best Score: " + playerName + ": " + bestScore; 
    }

    public void StartBtnClicked()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitBtnClicked()
    { 
        // This is conditional compiling, instructs the compiler whether it is Unity Editor in which appln is running or some other platform.
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif

        // NOTE: the else code is all greyed out, thats because we are currently inside of Unity Editor, & compiler knows that, when the build is made of this appln, automatically
        //       the else's code will get executed.
    }

    // this function is called when the text is entered in the input field. This text is stored in the MainManagerMenu(our non destroyable class object)'s textInputEntered field.
    // this field is passed to the Main scene to display the name. Concept is called Data Persistence between Scenes.
    public void OnInputtextEnter()
    {
        MainManagerMenu.Instance.textInputEntered = nameInputField.text;
    }
}
