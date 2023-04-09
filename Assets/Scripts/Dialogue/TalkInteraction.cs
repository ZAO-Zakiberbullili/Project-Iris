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
   // [SerializeField] private Button _choiceBtnPref;

    [SerializeField] private Color _normalTextColor;
    [SerializeField] private Color _thoughtTextColor;

    private Story _story;
    

    override public void Interaction()
    {
        //print("try");
        if (!_story)
            _story = new Story(_inkJsonAsset.text);
        
        // GameStateController.Instance.ChangeGameState(GameStateController.GameState.Dialogue);

        _dialogue.gameObject.SetActive(true);
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
           // print("StopStory");
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
            //print("text");
            ApplyStyling();
        }
        else if (_story.currentChoices.Count > 0)
        {
           // print("choice");
            DisplayChoices();
        }
    }

    private void DisplayChoices()
    {
        // checks if choices are already being displaye
        // if (_choiceButtonContainerLeft.GetComponentsInChildren<Button>().Length > 0 && _choiceButtonContainerRight.GetComponentsInChildren<Button>().Length > 0) return;



        Button[] ButtonLeftBlock = _choiceButtonContainerLeft.GetComponentsInChildren<Button>(true);
        Button[] ButtonRightBlock = _choiceButtonContainerRight.GetComponentsInChildren<Button>(true);
        //print(ButtonLeftBlock.Length);

        //if (ButtonLeftBlock.Length > 0 && ButtonRightBlock.Length > 0) return;
        for (int i = 0; i < 3; i++)
        {
            ButtonRightBlock[i].onClick.RemoveAllListeners();
            ButtonLeftBlock[i].onClick.RemoveAllListeners();
        }


        int questionCounter = 0;
        int satteliteCounter = 0;
        print(_story.currentChoices.Count);
        for (int i = 0; i < _story.currentChoices.Count; i++) // iterates through all choices
        {
            
            Choice choice = _story.currentChoices[i];


           // print("" + choice.tags);

           // print(choice.tags);


            if (choice.tags.Contains("tactic") || choice.tags.Contains("sattellite") && satteliteCounter == 1)
            {
                //print("ButtonRightBlock[0]");
                ButtonRightBlock[0].gameObject.SetActive(true);
                ButtonRightBlock[0].GetComponentInChildren<TMP_Text>().text = choice.text;
                ButtonRightBlock[0].onClick.AddListener(() => OnClickChoiceButton(choice));
            } 
            else if (choice.tags.Contains("sarcastic") || choice.tags.Contains("back"))
            {
                //print("ButtonRightBlock[1]");
                ButtonRightBlock[1].gameObject.SetActive(true);
                ButtonRightBlock[1].GetComponentInChildren<TMP_Text>().text = choice.text;
                ButtonRightBlock[1].onClick.AddListener(() => OnClickChoiceButton(choice));
            }
            else if (choice.tags.Contains("direct"))
            {
                //print("ButtonRightBlock[2]");
                ButtonRightBlock[2].gameObject.SetActive(true);
                ButtonRightBlock[2].GetComponentInChildren<TMP_Text>().text = choice.text;
                ButtonRightBlock[2].onClick.AddListener(() => OnClickChoiceButton(choice));
            }
            else if (choice.tags.Contains("insight") || choice.tags.Contains("another") || choice.tags.Contains("question") && questionCounter == 0)
            {
                print("ButtonLeftBlock[0]");
                ButtonLeftBlock[0].gameObject.SetActive(true);
                ButtonLeftBlock[0].GetComponentInChildren<TMP_Text>().text = choice.text;
                ButtonLeftBlock[0].onClick.AddListener(() => OnClickChoiceButton(choice));
            }
            else if (choice.tags.Contains("sattellite") && satteliteCounter == 0 || choice.tags.Contains("asking") || choice.tags.Contains("question") && questionCounter == 1)
            {
                //print("ButtonLeftBlock[1]");
                ButtonLeftBlock[1].gameObject.SetActive(true);
                ButtonLeftBlock[1].GetComponentInChildren<TMP_Text>().text = choice.text;
                ButtonLeftBlock[1].onClick.AddListener(() => OnClickChoiceButton(choice));
            }
            else if (choice.tags.Contains("flirt") || choice.tags.Contains("question") && questionCounter == 2)
            {
                //print("ButtonLeftBlock[2]");
                ButtonLeftBlock[2].gameObject.SetActive(true);
                ButtonLeftBlock[2].GetComponentInChildren<TMP_Text>().text = choice.text;
                ButtonLeftBlock[2].onClick.AddListener(() => OnClickChoiceButton(choice));
            }

            if (choice.tags.Contains("question"))
                questionCounter++;
            if (choice.tags.Contains("tactic"))
                satteliteCounter++;

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
