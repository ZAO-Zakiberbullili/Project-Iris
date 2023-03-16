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
    [SerializeField] private VerticalLayoutGroup _choiceButtonContainer;
    [SerializeField] private Button _choiceBtnPref;

    [SerializeField] private Color _normalTextColor;
    [SerializeField] private Color _thoughtTextColor;

    private Story _story;


    override public void Interaction()
    {
        if (!_story)
            _story = new Story(_inkJsonAsset.text);

        GameStateController.Instance.ChangeGameState(GameStateController.GameState.Dialogue);

        //print("I TALK! I TAAALK!");
        _dialogue.gameObject.SetActive(true);
        DisplayNextLine();
        // OnDialogueStart?.Invoke();
    }

    private void DisplayNextLine()
    {
        if (!_story.canContinue && _story.currentChoices.Count == 0)
        {
            //  OnDialogueStop?.Invoke();
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
        if (_choiceButtonContainer.GetComponentsInChildren<Button>().Length > 0) return;

        for (int i = 0; i < _story.currentChoices.Count; i++) // iterates through all choices
        {

            var choice = _story.currentChoices[i];
            var button = CreateChoiceButton(choice.text); // creates a choice button

            button.onClick.AddListener(() => OnClickChoiceButton(choice));
        }
    }

    Button CreateChoiceButton(string text)
    {
        // creates the button from a prefab
        var choiceButton = Instantiate(_choiceBtnPref);
        choiceButton.transform.SetParent(_choiceButtonContainer.transform, false);

        // sets text on the button
        var buttonText = choiceButton.GetComponentInChildren<TMP_Text>();
        buttonText.text = text;

        return choiceButton;
    }
    void OnClickChoiceButton(Choice choice)
    {
        _story.ChooseChoiceIndex(choice.index); // tells ink which choice was selected
        RefreshChoiceView(); // removes choices from the screen
        DisplayNextLine();
    }
    void RefreshChoiceView()
    {
        if (_choiceButtonContainer != null)
        {
            foreach (var button in _choiceButtonContainer.GetComponentsInChildren<Button>())
            {
                Destroy(button.gameObject);
            }
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
