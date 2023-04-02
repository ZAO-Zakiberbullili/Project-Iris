using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;

public class TemporaryDialogue : InteractionHandler
{
    [SerializeField] private TextAsset _inkJsonAsset;
    [SerializeField] private TMP_Text _textField;
    [SerializeField] private GameObject _dialogue;

    [SerializeField] private int _showTime;

    private Story _story;
    private Coroutine _ieFadeout = null;


    override public void Interaction()
    {
        StartDialogue();
    }


    public void StartDialogue()
    {
        _story = new Story(_inkJsonAsset.text);
        _ieFadeout = StartCoroutine(IEFadeout());
        DisplayNextLine();
    }

    private void DisplayNextLine()
    {
        if (!_story.canContinue)
            return;

        string text = _story.Continue(); // gets next line
        text = text?.Trim(); // removes white space from text
        _textField.text = text; // displays new text
    }

    private IEnumerator IEFadeout()
    {
        _dialogue.gameObject.SetActive(true);
        yield return new WaitForSeconds(_showTime);
        _dialogue.gameObject.SetActive(false);
        _ieFadeout = null;
    }

    private void OnDestroy()
    {
        if (_ieFadeout != null)
            StopCoroutine(_ieFadeout);
    }

}

