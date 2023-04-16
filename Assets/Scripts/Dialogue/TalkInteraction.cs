using Ink.Runtime;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TalkInteraction : InteractionHandler
{
    [SerializeField] private TextAsset _inkJsonAsset;
    [SerializeField] private TMP_Text _textField;
    [SerializeField] private GameObject _dialogue;
    [SerializeField] private GameObject _choiceButtonContainerLeft;
    [SerializeField] private GameObject _choiceButtonContainerRight;

    [SerializeField] private GameObject _leftSelectContainer;
    [SerializeField] private GameObject _rightSelectContainer;

    [SerializeField] private GameObject _leftSelectHoverContainer;
    [SerializeField] private GameObject _rightSelectHoverContainer;


    [SerializeField] private Color _normalTextColor;
    [SerializeField] private Color _thoughtTextColor;

    private Image[] selectLeftBlock;
    private Image[] selectRightBlock;
    private Image[] selectHoverLeftBlock;
    private Image[] selectHoverRightBlock;

    private Button[] ButtonLeftBlock;
    private Button[] ButtonRightBlock;
    const int BLOCK_LENGTH = 3;

    private Story _story;


    public void Start()
    {
        selectLeftBlock = _leftSelectContainer.GetComponentsInChildren<Image>(true);
        selectRightBlock = _rightSelectContainer.GetComponentsInChildren<Image>(true);

        selectHoverLeftBlock = _leftSelectHoverContainer.GetComponentsInChildren<Image>(true);
        selectHoverRightBlock = _rightSelectHoverContainer.GetComponentsInChildren<Image>(true);

        ButtonLeftBlock = _choiceButtonContainerLeft.GetComponentsInChildren<Button>(true);
        ButtonRightBlock = _choiceButtonContainerRight.GetComponentsInChildren<Button>(true);
    }

    override public void Interaction()
    {
      
        if (!_story)
            _story = new Story(_inkJsonAsset.text);
        
        // GameStateController.Instance.ChangeGameState(GameStateController.GameState.Dialogue);
        DisplayNextLine();
    }

    private void FixedUpdate()
    {
        if (_story && _story.currentChoices.Count > 0)
            DisplayNextLine();
    }


    private void DisplayNextLine()
    {
        
        if (!_story.canContinue && _story.currentChoices.Count == 0)
        {
            _dialogue.gameObject.SetActive(false);
            GameStateController.Instance.ChangeGameState(GameStateController.GameState.Normal);
            _story = null;
            return;
        }

        if (_story.canContinue)
        {

            string text = _story.Continue(); // gets next line
            text = text?.Trim(); // removes white space from text
            _textField.text = text; // displays new text

            if (text.Equals(""))
            {
                _dialogue.gameObject.SetActive(false); 
            } else
            {
                _dialogue.gameObject.SetActive(true);
                ApplyStyling();
            }
            
        }
        else if (_story.currentChoices.Count > 0)
        {
            DisplayChoices();
        }
    }

    private void DisplayChoices()
    {
       for (int i = 0; i < BLOCK_LENGTH; i++)
        {
            ButtonRightBlock[i].GetComponent<HoverChecker>().OnButtonHover -= HoverHandler;
            ButtonLeftBlock[i].GetComponent<HoverChecker>().OnButtonHover -= HoverHandler;

            ButtonRightBlock[i].GetComponent<HoverChecker>().OnButtonHover -= HoverEndHandler;
            ButtonLeftBlock[i].GetComponent<HoverChecker>().OnButtonHover -= HoverEndHandler;

            ButtonRightBlock[i].onClick.RemoveAllListeners();
            ButtonLeftBlock[i].onClick.RemoveAllListeners();
        } 


        int questionCounter = 0;
        int satteliteCounter = 0;
        print(_story.currentChoices.Count);
        for (int i = 0; i < _story.currentChoices.Count; i++) 
        {
            
            Choice choice = _story.currentChoices[i];

            if (choice.tags.Contains("tactic") || choice.tags.Contains("sattellite") && satteliteCounter == 1)
            {
                ButtonRightBlock[0].GetComponent<HoverChecker>().OnButtonHover += HoverHandler;
                ButtonRightBlock[0].GetComponent<HoverChecker>().OnButtonHoverEnd += HoverEndHandler;

                selectRightBlock[0].gameObject.SetActive(true);

                ButtonRightBlock[0].gameObject.SetActive(true);
                ButtonRightBlock[0].GetComponentInChildren<TMP_Text>().text = choice.text;
                ButtonRightBlock[0].onClick.AddListener(() => OnClickChoiceButton(choice));
            } 
            else if (choice.tags.Contains("sarcastic") || choice.tags.Contains("back"))
            {
                ButtonRightBlock[1].GetComponent<HoverChecker>().OnButtonHover += HoverHandler;
                ButtonRightBlock[1].GetComponent<HoverChecker>().OnButtonHoverEnd += HoverEndHandler;

                selectRightBlock[1].gameObject.SetActive(true);

                ButtonRightBlock[1].gameObject.SetActive(true);
                ButtonRightBlock[1].GetComponentInChildren<TMP_Text>().text = choice.text;
                ButtonRightBlock[1].onClick.AddListener(() => OnClickChoiceButton(choice));
            }
            else if (choice.tags.Contains("direct"))
            {
                ButtonRightBlock[2].GetComponent<HoverChecker>().OnButtonHover += HoverHandler;
                ButtonRightBlock[2].GetComponent<HoverChecker>().OnButtonHoverEnd += HoverEndHandler;

                selectRightBlock[2].gameObject.SetActive(true);

                ButtonRightBlock[2].gameObject.SetActive(true);
                ButtonRightBlock[2].GetComponentInChildren<TMP_Text>().text = choice.text;
                ButtonRightBlock[2].onClick.AddListener(() => OnClickChoiceButton(choice));
            }
            else if (choice.tags.Contains("insight") || choice.tags.Contains("another") || choice.tags.Contains("question") && questionCounter == 0)
            {
                ButtonLeftBlock[0].GetComponent<HoverChecker>().OnButtonHover += HoverHandler;
                ButtonLeftBlock[0].GetComponent<HoverChecker>().OnButtonHoverEnd += HoverEndHandler;


                selectLeftBlock[0].gameObject.SetActive(true);

                ButtonLeftBlock[0].gameObject.SetActive(true);
                ButtonLeftBlock[0].GetComponentInChildren<TMP_Text>().text = choice.text;
                ButtonLeftBlock[0].onClick.AddListener(() => OnClickChoiceButton(choice));
            }
            else if (choice.tags.Contains("sattellite") && satteliteCounter == 0 || choice.tags.Contains("asking") || choice.tags.Contains("question") && questionCounter == 1)
            {
                ButtonLeftBlock[1].GetComponent<HoverChecker>().OnButtonHover += HoverHandler;
                ButtonLeftBlock[1].GetComponent<HoverChecker>().OnButtonHoverEnd += HoverEndHandler;

                selectLeftBlock[1].gameObject.SetActive(true);

                ButtonLeftBlock[1].gameObject.SetActive(true);
                ButtonLeftBlock[1].GetComponentInChildren<TMP_Text>().text = choice.text;
                ButtonLeftBlock[1].onClick.AddListener(() => OnClickChoiceButton(choice));
            }
            else if (choice.tags.Contains("flirt") || choice.tags.Contains("question") && questionCounter == 2)
            {
                ButtonLeftBlock[2].GetComponent<HoverChecker>().OnButtonHover += HoverHandler;
                ButtonLeftBlock[2].GetComponent<HoverChecker>().OnButtonHoverEnd += HoverEndHandler;

                selectLeftBlock[2].gameObject.SetActive(true);

                ButtonLeftBlock[2].gameObject.SetActive(true);
                ButtonLeftBlock[2].GetComponentInChildren<TMP_Text>().text = choice.text;
                ButtonLeftBlock[2].onClick.AddListener(() => OnClickChoiceButton(choice));
            }

            if (choice.tags.Contains("question"))
                questionCounter++;
            if (choice.tags.Contains("tactic"))
                satteliteCounter++;
        }
    }

    void OnClickChoiceButton(Choice choice)
    {
        _story.ChooseChoiceIndex(choice.index); // tells ink which choice was selected
        RefreshChoiceView(); // removes choices from the screen
        DisplayNextLine();
    }
    void RefreshChoiceView()
    {
        for (int i = 0; i < BLOCK_LENGTH; i++)
        {
            selectLeftBlock[i].gameObject.SetActive(false);
            selectRightBlock[i].gameObject.SetActive(false);

            ButtonRightBlock[i].gameObject.SetActive(false);
            ButtonLeftBlock[i].gameObject.SetActive(false);
        }
    }

    private void HoverHandler(int buttonNumber, int side)
    {
        Image[] buttonContainer;
        Image[] buttonHoverContainer;

        switch (side)
        {
            case 0:
            {
                buttonContainer = selectLeftBlock;
                buttonHoverContainer = selectHoverLeftBlock;
                break;
            }
            case 1:
            {
                buttonContainer = selectRightBlock;
                buttonHoverContainer = selectHoverRightBlock;
                break;
            }
            default:
                return;
        }

        buttonContainer[buttonNumber].gameObject.SetActive(false);
        buttonHoverContainer[buttonNumber].gameObject.SetActive(true);
    }

    private void HoverEndHandler(int buttonNumber, int side)
    {
        Image[] buttonContainer;
        Image[] buttonHoverContainer;

        switch (side)
        {
            case 0:
                {
                    buttonContainer = selectLeftBlock;
                    buttonHoverContainer = selectHoverLeftBlock;
                    break;
                }
            case 1:
                {
                    buttonContainer = selectRightBlock;
                    buttonHoverContainer = selectHoverRightBlock;
                    break;
                }
            default:
                return;
        }

        buttonContainer[buttonNumber].gameObject.SetActive(true);
        buttonHoverContainer[buttonNumber].gameObject.SetActive(false);
    }

    private void ApplyStyling()
    {
        if (_story.currentTags.Contains("thought"))
        {
            _textField.color = _thoughtTextColor;
            _textField.fontStyle = FontStyles.Italic;
        }
        else
        {
            _textField.color = _normalTextColor;
            _textField.fontStyle = FontStyles.Normal;
        }
    }
}
