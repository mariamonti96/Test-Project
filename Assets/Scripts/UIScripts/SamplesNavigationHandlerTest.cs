/*===============================================================================
Copyright (c) 2015-2017 PTC Inc. All Rights Reserved.
 
Copyright (c) 2015 Qualcomm Connected Experiences, Inc. All Rights Reserved.
 
Vuforia is a trademark of PTC Inc., registered in the United States and other 
countries.
===============================================================================*/
using UnityEngine;

public class SamplesNavigationHandlerTest : MonoBehaviour
{

    #region PRIVATE_MEMBERS

    string currentSceneName;

    #endregion // PRIVATE_MEMBERS

    #region PUBLIC_METHODS

    public void OnStartAR()
    {
        // called by OK button on About screen
        SamplesMainMenuTest.LoadScene(SamplesMainMenuTest.LoadingScene);
    }

    public void LoadMenuScene()
    {
        // called by "Vuforia Samples" button in AR scene UI menu
        // also called below in Update() if Android Back button pressed
        SamplesMainMenuTest.LoadScene(SamplesMainMenuTest.MenuScene);
    }

    #endregion // PUBLIC_METHODS

    #region MONOBEHAVIOUR_METHODS

    void Start()
    {
        currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
    }

    void Update()
    {
#if (UNITY_EDITOR || UNITY_ANDROID)

        if (Input.GetKeyUp(KeyCode.Escape))
        {

            Debug.Log("Esc/Back button pressed from " + currentSceneName);

            if (currentSceneName == SamplesMainMenuTest.MenuScene)
            {
                if (SamplesMainMenuTest.isAboutScreenVisible)
                {
                    gameObject.GetComponent<SamplesMainMenuTest>().BackToMenu();
                }
                else
                {
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_ANDROID
                    // On Android, the Back button is mapped to the Esc key
                    Application.Quit();
#endif
                }
            }
            else
            {
                LoadMenuScene();
            }
        }

        if (Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.JoystickButton0))
        {

            if (currentSceneName == SamplesMainMenuTest.MenuScene && SamplesMainMenuTest.isAboutScreenVisible)
            {
                // In Unity 'Return' key same as clicking OK button on About Screen
                // On ODG R7, JoystickButton0 is the Trackpad select button
                OnStartAR();
            }
        }

#endif // UNITY_EDITOR || UNITY_ANDROID
    }

    #endregion // MONOBEHAVIOUR_METHODS

}