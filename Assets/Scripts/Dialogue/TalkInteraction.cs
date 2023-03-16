using Ink.Runtime;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TalkInteraction : InteractionHandler
{
    // public Action OnDialogueStart;
    // public Action OnDialogueStop;

    [SerializeField] private TextAsset _inkJsonAsset;
    [SerializeField] private TMP_Text _textField;
    [SerializeField] private GameObject _dialogue;
    [SerializeField] private VerticalLayoutGroup _choiceButtonContainerLeft;
    [SerializeField] private VerticalLayoutGroup _choiceButtonContainerRight;
    [SerializeField] private Button _choiceBtnPref;

    [SerializeField] private Color _normalTextColor;
    [SerializeField] private Color _thoughtTextColor;

    private Story _story;


    override public void Interaction()
    {
        if (!_story)
            _story = new Story(_inkJsonAsset.text);

        GameStateController.Instance.ChangeGameState(GameStateController.GameState.Dialogue);

        _dialogue.gameObject.SetActive(true);
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
            ApplyStyling();
        }
        else if (_story.currentChoices.Count > 0)
        {
            DisplayChoices();
        }
    }

    private void DisplayChoices()
    {
        // checks if choices are already being displaye
       // if (_choiceButtonContainerLeft.GetComponentsInChildren<Button>().Length > 0 && _choiceButtonContainerRight.GetComponentsInChildren<Button>().Length > 0) return;

        Button[] ButtonLeftBlock = _choiceButtonContainerLeft.GetComponentsInChildren<Button>();
        Button[] ButtonRightBlock = _choiceButtonContainerRight.GetComponentsInChildren<Button>();

        if (ButtonLeftBlock.Length > 0 && ButtonRightBlock.Length > 0) return;

        for (int i = 0; i < _story.currentChoices.Count; i++) // iterates through all choices
        {

            var choice = _story.currentChoices[i];
            if (_story.currentTags.Contains("tactful"))
            {
                ButtonRightBlock[0].gameObject.SetActive(true);
                ButtonRightBlock[0].GetComponentInChildren<TMP_Text>().text = choice.text;
                ButtonRightBlock[0].onClick.AddListener(() => OnClickChoiceButton(choice));
            } 
            else if (_story.currentTags.Contains("sarcasm") || _story.currentTags.Contains("trick"))
            {
                ButtonRightBlock[1].gameObject.SetActive(true);
                ButtonRightBlock[1].GetComponentInChildren<TMP_Text>().text = choice.text;
                ButtonRightBlock[1].onClick.AddListener(() => OnClickChoiceButton(choice));
            }
            else if (_story.currentTags.Contains("straightforwardness"))
            {
                ButtonRightBlock[2].gameObject.SetActive(true);
                ButtonRightBlock[2].GetComponentInChildren<TMP_Text>().text = choice.text;
                ButtonRightBlock[2].onClick.AddListener(() => OnClickChoiceButton(choice));
            }
            else if (_story.currentTags.Contains("insight") || _story.currentTags.Contains("knowledge") || _story.currentTags.Contains("satellite") || _story.currentTags.Contains("flirting"))
            {
                ButtonLeftBlock[0].gameObject.SetActive(true);
                ButtonLeftBlock[0].GetComponentInChildren<TMP_Text>().text = choice.text;
                ButtonLeftBlock[0].onClick.AddListener(() => OnClickChoiceButton(choice));
            }
            else if (_story.currentTags.Contains("straightforwardness"))
            {
                ButtonLeftBlock[0].gameObject.SetActive(true);
                ButtonLeftBlock[0].GetComponentInChildren<TMP_Text>().text = choice.text;
                ButtonLeftBlock[0].onClick.AddListener(() => OnClickChoiceButton(choice));
            }


            //           var button = CreateChoiceButton(choice.text); // creates a choice button

                //  button.onClick.AddListener(() => OnClickChoiceButton(choice));
        }
    }

 /*   Button CreateChoiceButton(string text)
    {
        // creates the button from a prefab
        var choiceButton = Instantiate(_choiceBtnPref);
        choiceButton.transform.SetParent(_choiceButtonContainer.transform, false);

        // sets text on the button
        var buttonText = choiceButton.GetComponentInChildren<TMP_Text>();
        buttonText.text = text;

        return choiceButton;
    }*/
    void OnClickChoiceButton(Choice choice)
    {
        _story.ChooseChoiceIndex(choice.index); // tells ink which choice was selected
        RefreshChoiceView(); // removes choices from the screen
        DisplayNextLine();
    }
    void RefreshChoiceView()
    {
        
        foreach (var button in _choiceButtonContainerLeft.GetComponentsInChildren<Button>())
         {
            button.gameObject.SetActive(false);
         }

        foreach (var button in _choiceButtonContainerRight.GetComponentsInChildren<Button>())
        {
            button.gameObject.SetActive(false);
        }
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
