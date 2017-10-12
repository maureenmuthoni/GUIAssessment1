using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GUIAssessment1
{
    public class GUIManager : MonoBehaviour
    {
        [Header("Bools")]
        public bool showOptions;
        public bool fullScreenToggle;
        public bool pause;

        [Header("Resolutions")]
        public int index;
        public int[] resX, resY;
        public Dropdown resolutionDropdown;

        [Header("Keys")]
        public KeyCode forward;
        public KeyCode backward;
        public KeyCode left;
        public KeyCode right;
        public KeyCode jump;
        public KeyCode crouch;
        public KeyCode interact;
        public KeyCode sprint;
        public KeyCode holdingKey;

        [Header("References")]
        public GameObject menu, options;
        public AudioSource mainMusic;
        public Slider volumeSlider, brightnessSlider;
        public Light brightness;
        public Text forwardText, backwardText, leftText, rightText, jumpText, crouchText, interactText, sprintText;
        public Toggle fullScreen;

        // Use this for initialization
        void Start()
        {
            // If there is a music file and a volume slider
            if (mainMusic != null && volumeSlider != null)
            {
                // If we have a volume setting
                if (PlayerPrefs.HasKey("Volume"))
                {
                    // Load saved settings
                    Load();
                }
                // Set volumeSlider's value equal to mainMusic's volume
                volumeSlider.value = mainMusic.volume;
            }
            if (brightness != null && brightnessSlider != null)
            {
                brightnessSlider.value = brightness.intensity;
            }
            if (fullScreen)
            {
                Screen.SetResolution(640, 480, true);
            }
            else if (!fullScreen)
            {
                Screen.SetResolution(640, 480, false);
            }
            Camera.main.orthographic = true;

            #region Key Set Up
            forward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Forward", "W"));
            backward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Backward", "S"));
            left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "A"));
            right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "D"));
            jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Jump", "Space"));
            crouch = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Crouch", "LeftControl"));
            sprint = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Sprint", "LeftShift"));
            interact = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Interact", "E"));

            forwardText.text = forward.ToString();
            backwardText.text = backward.ToString();
            leftText.text = left.ToString();
            rightText.text = right.ToString();
            jumpText.text = jump.ToString();
            crouchText.text = crouch.ToString();
            sprintText.text = sprint.ToString();
            interactText.text = interact.ToString();
            #endregion

            #region Resolution
            index = PlayerPrefs.GetInt("Res", 3);
            int fullWindow = PlayerPrefs.GetInt("FullWindow", 1);
            if (fullWindow == 0)
            {
                fullScreen.isOn = false;
                fullScreenToggle = false;
            }
            else
            {
                fullScreen.isOn = false;
                
                fullScreenToggle = true;
            }
            resolutionDropdown.value = index;
            Screen.SetResolution(resX[index], resY[index], fullScreen);
            #endregion
        }

        // Update is called once per frame
        void Update()
        {
            if (mainMusic != null && volumeSlider != null)
            {
                if (volumeSlider.value != mainMusic.volume)
                {
                    mainMusic.volume = volumeSlider.value;
                }
            }

            if (brightness != null && brightnessSlider != null)
            {
                if (brightnessSlider.value != brightness.intensity)
                {
                    brightness.intensity = brightnessSlider.value;
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Pause();
            }
        }

        void OnGUI()
        {
            #region Set New Key or Set Key Back
            Event e = Event.current;
            if (forward == KeyCode.None)
            {
                // if an event is triggered by a key press
                if (e.isKey)
                {
                    Debug.Log("Key Code: " + e.keyCode);
                    // if this key is not the same as the other keys
                    if (!(e.keyCode == backward || e.keyCode == left || e.keyCode == right || e.keyCode == jump || e.keyCode == sprint || e.keyCode == crouch || e.keyCode == interact))
                    {
                        // set forward to the new key
                        forward = e.keyCode;
                        // set holding key to none
                        holdingKey = KeyCode.None;
                        // set to new key
                        forwardText.text = forward.ToString();
                    }
                    else
                    {
                        // set forward back to what the holding key is
                        forward = holdingKey;
                        // set holding key to none
                        holdingKey = KeyCode.None;
                    }
                }
            }
            if (backward == KeyCode.None)
            {
                // if an event is triggered by a key press
                if (e.isKey)
                {
                    Debug.Log("Key Code: " + e.keyCode);
                    // if this key is not the same as the other keys
                    if (!(e.keyCode == forward || e.keyCode == left || e.keyCode == right || e.keyCode == jump || e.keyCode == sprint || e.keyCode == crouch || e.keyCode == interact))
                    {
                        // set backward to the new key
                        backward = e.keyCode;
                        // set holding key to none
                        holdingKey = KeyCode.None;
                        // set to new key
                        backwardText.text = backward.ToString();
                    }
                    else
                    {
                        // set backward back to what the holding key is
                        backward = holdingKey;
                        // set holding key to none
                        holdingKey = KeyCode.None;
                    }
                }
            }
            if (left == KeyCode.None)
            {
                // if an event is triggered by a key press
                if (e.isKey)
                {
                    Debug.Log("Key Code: " + e.keyCode);
                    // if this key is not the same as the other keys
                    if (!(e.keyCode == forward || e.keyCode == backward || e.keyCode == right || e.keyCode == jump || e.keyCode == sprint || e.keyCode == crouch || e.keyCode == interact))
                    {
                        // set left to the new key
                        left = e.keyCode;
                        // set holding key to none
                        holdingKey = KeyCode.None;
                        // set to new key
                        leftText.text = left.ToString();
                    }
                    else
                    {
                        // set left back to what the holding key is
                        left = holdingKey;
                        // set holding key to none
                        holdingKey = KeyCode.None;
                    }
                }
            }
            if (right == KeyCode.None)
            {
                // if an event is triggered by a key press
                if (e.isKey)
                {
                    Debug.Log("Key Code: " + e.keyCode);
                    // if this key is not the same as the other keys
                    if (!(e.keyCode == forward || e.keyCode == backward || e.keyCode == left || e.keyCode == jump || e.keyCode == sprint || e.keyCode == crouch || e.keyCode == interact))
                    {
                        // set right to the new key
                        right = e.keyCode;
                        // set holding key to none
                        holdingKey = KeyCode.None;
                        // set to new key
                        rightText.text = right.ToString();
                    }
                    else
                    {
                        // set right back to what the holding key is
                        right = holdingKey;
                        // set holding key to none
                        holdingKey = KeyCode.None;
                    }
                }
            }
            if (sprint == KeyCode.None)
            {
                // if an event is triggered by a key press
                if (e.isKey)
                {
                    Debug.Log("Key Code: " + e.keyCode);
                    // if this key is not the same as the other keys
                    if (!(e.keyCode == forward || e.keyCode == backward || e.keyCode == left || e.keyCode == jump || e.keyCode == right || e.keyCode == crouch || e.keyCode == interact))
                    {
                        // set sprint to the new key
                        sprint = e.keyCode;
                        // set holding key to none
                        holdingKey = KeyCode.None;
                        // set to new key
                        sprintText.text = sprint.ToString();
                    }
                    else
                    {
                        // set sprint back to what the holding key is
                        sprint = holdingKey;
                        // set holding key to none
                        holdingKey = KeyCode.None;
                    }
                }
            }
            if (interact == KeyCode.None)
            {
                // if an event is triggered by a key press
                if (e.isKey)
                {
                    Debug.Log("Key Code: " + e.keyCode);
                    // if this key is not the same as the other keys
                    if (!(e.keyCode == forward || e.keyCode == backward || e.keyCode == left || e.keyCode == jump || e.keyCode == right || e.keyCode == crouch || e.keyCode == sprint))
                    {
                        // set interact to the new key
                        interact = e.keyCode;
                        // set holding key to none
                        holdingKey = KeyCode.None;
                        // set to new key
                        interactText.text = interact.ToString();
                    }
                    else
                    {
                        // set interact back to what the holding key is
                        interact = holdingKey;
                        // set holding key to none
                        holdingKey = KeyCode.None;
                    }
                }
            }
            if (crouch == KeyCode.None)
            {
                // if an event is triggered by a key press
                if (e.isKey)
                {
                    Debug.Log("Key Code: " + e.keyCode);
                    // if this key is not the same as the other keys
                    if (!(e.keyCode == forward || e.keyCode == backward || e.keyCode == left || e.keyCode == jump || e.keyCode == right || e.keyCode == interact || e.keyCode == sprint))
                    {
                        // set crouch to the new key
                        crouch = e.keyCode;
                        // set holding key to none
                        holdingKey = KeyCode.None;
                        // set to new key
                        crouchText.text = crouch.ToString();
                    }
                    else
                    {
                        // set crouch back to what the holding key is
                        crouch = holdingKey;
                        // set holding key to none
                        holdingKey = KeyCode.None;
                    }
                }
            }
            if (jump == KeyCode.None)
            {
                // if an event is triggered by a key press
                if (e.isKey)
                {
                    Debug.Log("Key Code: " + e.keyCode);
                    // if this key is not the same as the other keys
                    if (!(e.keyCode == forward || e.keyCode == backward || e.keyCode == left || e.keyCode == crouch || e.keyCode == right || e.keyCode == interact || e.keyCode == sprint))
                    {
                        // set jump to the new key
                        jump = e.keyCode;
                        // set holding key to none
                        holdingKey = KeyCode.None;
                        // set to new key
                        jumpText.text = jump.ToString();
                    }
                    else
                    {
                        // set jump back to what the holding key is
                        jump = holdingKey;
                        // set holding key to none
                        holdingKey = KeyCode.None;
                    }
                }
            }
            #endregion
        }

        public void Play()
        {
            SceneManager.LoadScene(1);
        }

        public void Exit()
        {
            Application.Quit();

        }

        public void ShowOptions()
        {
            ToggleOptions();
        }

        public bool ToggleOptions()
        {
            if (showOptions)
            {
                menu.SetActive(true);
                options.SetActive(false);
                showOptions = false;
                return false;
            }
            else
            {
                menu.SetActive(false);
                options.SetActive(true);
                showOptions = true;
                return true;
            }
        }

        public void Save()
        {
            PlayerPrefs.SetFloat("Volume", mainMusic.volume);
            PlayerPrefs.SetFloat("Brightness", brightness.intensity);

            PlayerPrefs.SetString("Forward", forward.ToString());
            PlayerPrefs.SetString("Backward", backward.ToString());
            PlayerPrefs.SetString("Left", left.ToString());
            PlayerPrefs.SetString("Right", right.ToString());
            PlayerPrefs.SetString("Jump", jump.ToString());
            PlayerPrefs.SetString("Crouch", forward.ToString());
            PlayerPrefs.SetString("Sprint", sprint.ToString());
            PlayerPrefs.SetString("Interact", interact.ToString());

        }

        public void Load()
        {
            mainMusic.volume = PlayerPrefs.GetFloat("Volume");
            brightness.intensity = PlayerPrefs.GetFloat("Brightness");

            forwardText.text = PlayerPrefs.GetString("Forward");
            backwardText.text = PlayerPrefs.GetString("Backward");
            leftText.text = PlayerPrefs.GetString("Left");
            rightText.text = PlayerPrefs.GetString("Right");
            jumpText.text = PlayerPrefs.GetString("Jump");
            crouchText.text = PlayerPrefs.GetString("Crouch");
            sprintText.text = PlayerPrefs.GetString("Sprint");
            interactText.text = PlayerPrefs.GetString("Interact");
        }

        public void FullScreen()
        {
            Screen.fullScreen = !Screen.fullScreen;
            fullScreenToggle = !fullScreenToggle;
        }

        public bool FullScreenToggle()
        {
            if (fullScreen)
            {
                fullScreenToggle = false;
                fullScreen.isOn = false;
                Screen.fullScreen = false;
                return false;
            }
            else
            {
                fullScreenToggle = true;
                fullScreen.isOn = true;
                Screen.fullScreen = true;
                return true;
            }
        }

        public void Mute()
        {
            mainMusic.mute = !mainMusic.mute;
        }

        public void ChangeResolution()
        {
            index = resolutionDropdown.value;
            Screen.SetResolution(resX[index], resY[index], fullScreenToggle);
        }

        public void Pause()
        {
            TogglePause();
        }

        public bool TogglePause()
        {
            if (pause)
            {
                if (!showOptions)
                {
                    Time.timeScale = 1;
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    menu.SetActive(false);
                    pause = false;
                    //HUD.enabled = true;
                }
                else
                {
                    showOptions = false;
                    options.SetActive(false);
                    menu.SetActive(true);
                }
                return false;
            }
            else
            {
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                pause = true;
                menu.SetActive(true);
                //HUD.enabled = false;

                return true;
            }

        }

        public void MainMenu()
        {
            SceneManager.LoadScene(0);
        }

        #region Controls

        public void Forward()
        {
            if (!(backward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || jump == KeyCode.None || crouch == KeyCode.None || sprint == KeyCode.None || interact == KeyCode.None))
            {
                //set our holding key to the key of this button
                holdingKey = forward;
                //set this buttin to none allowing us to edit only this button
                forward = KeyCode.None;
                //set the GUI to blank
                forwardText.text = forward.ToString();

            }
        }

        public void Backwards()
        {
            if (!(forward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || jump == KeyCode.None || crouch == KeyCode.None || sprint == KeyCode.None || interact == KeyCode.None))
            {
                //set our holding key to the key of this button
                holdingKey = backward;
                //set this buttin to none allowing us to edit only this button
                backward = KeyCode.None;
                //set the GUI to blank
                backwardText.text = backward.ToString();

            }
        }

        public void Left()
        {
            if (!(backward == KeyCode.None || forward == KeyCode.None || right == KeyCode.None || jump == KeyCode.None || crouch == KeyCode.None || sprint == KeyCode.None || interact == KeyCode.None))
            {
                //set our holding key to the key of this button
                holdingKey = left;
                //set this buttin to none allowing us to edit only this button
                left = KeyCode.None;
                //set the GUI to blank
                leftText.text = left.ToString();

            }
        }

        public void Right()
        {
            if (!(backward == KeyCode.None || left == KeyCode.None || forward == KeyCode.None || jump == KeyCode.None || crouch == KeyCode.None || sprint == KeyCode.None || interact == KeyCode.None))
            {
                //set our holding key to the key of this button
                holdingKey = right;
                //set this buttin to none allowing us to edit only this button
                right = KeyCode.None;
                //set the GUI to blank
                rightText.text = right.ToString();

            }
        }

        public void Jump()
        {
            if (!(backward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || forward == KeyCode.None || crouch == KeyCode.None || sprint == KeyCode.None || interact == KeyCode.None))
            {
                //set our holding key to the key of this button
                holdingKey = jump;
                //set this buttin to none allowing us to edit only this button
                jump = KeyCode.None;
                //set the GUI to blank
                jumpText.text = jump.ToString();

            }
        }

        public void Crouch()
        {
            if (!(backward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || jump == KeyCode.None || forward == KeyCode.None || sprint == KeyCode.None || interact == KeyCode.None))
            {
                //set our holding key to the key of this button
                holdingKey = crouch;
                //set this buttin to none allowing us to edit only this button
                crouch = KeyCode.None;
                //set the GUI to blank
                crouchText.text = crouch.ToString();

            }
        }

        public void Sprint()
        {
            if (!(backward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || jump == KeyCode.None || crouch == KeyCode.None || forward == KeyCode.None || interact == KeyCode.None))
            {
                //set our holding key to the key of this button
                sprint = forward;
                //set this buttin to none allowing us to edit only this button
                sprint = KeyCode.None;
                //set the GUI to blank
                sprintText.text = sprint.ToString();

            }
        }

        public void Interact()
        {
            if (!(backward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || jump == KeyCode.None || crouch == KeyCode.None || sprint == KeyCode.None || forward == KeyCode.None))
            {
                //set our holding key to the key of this button
                holdingKey = interact;
                //set this buttin to none allowing us to edit only this button
                interact = KeyCode.None;
                //set the GUI to blank
                interactText.text = interact.ToString();

            }
        }
        #endregion
    }
}
