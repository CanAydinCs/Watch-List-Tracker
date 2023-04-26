using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UIElements.Button;
using Label = UnityEngine.UIElements.Label;
using Toggle = UnityEngine.UIElements.Toggle;

public class Lister : MonoBehaviour
{
    private VisualElement root, bg;
    private Button btnBack, btnNext;

    private List<VisualElement[]> pages;

    [SerializeField]
    private int softLimit = 15;

    private int cPage = 0;

    private void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        bg = root.Q<VisualElement>("background");
        btnBack = root.Q<Button>("btnBack");
        btnNext = root.Q<Button>("btnNext");

        btnBack.clicked += BtnBack_clicked;
        btnNext.clicked += BtnNext_clicked;

        pages = new List<VisualElement[]>();

        for (int i = 0; i < 20; i++)
        {
            AddToPages(MakeSet("Name" + i, "Season", "Episode", "Last Name", _finished: true, _tFinished: "Finished"));
        }
    }

    private void AddToPages(VisualElement element)
    {
        if(pages.Count == 0)
        {
            pages.Add(new VisualElement[softLimit]);
            pages[0][0] = MakeSet("Name", "Season", "Episode", "Last Name", _finished: true, _tFinished: "Finished");
            pages[0][1] = element;
        }
        else if (pages[pages.Count - 1].Length == softLimit)
        {
            pages.Add(new VisualElement[softLimit]);
            pages[pages.Count - 1][0] = MakeSet("Name", "Season", "Episode", "Last Name", _finished: true, _tFinished: "Finished");
            pages[pages.Count - 1][1] = element;
        }
        else
        {
            pages[pages.Count - 1][pages[pages.Count - 1].Length - 1] = element;
        }
    }

    private VisualElement MakeSet(string _name, string _lastSeason, string _lastEpisode, string _lastWatchedName, bool _finished = true, string _tFinished = "")
    {
        VisualElement set = new VisualElement();

        set.style.flexDirection = FlexDirection.Row;
        set.style.alignContent = Align.Center;
        set.style.alignItems = Align.Stretch;

        set.Add(MakeButton(_name));
        set.Add(MakeLabel(_lastSeason));
        set.Add(MakeLabel(_lastEpisode));
        set.Add(MakeLabel(_lastWatchedName));
        set.Add(MakeToggle(_finished, _tFinished));

        return set;
    }

    private Label MakeLabel(string _text)
    {
        Label label = new Label();

        label.text = _text;
        label.style.fontSize = 25;
        label.style.alignContent = Align.Center;
        label.style.alignItems = Align.Stretch;
        label.style.flexDirection = FlexDirection.Column;

        return label;
    }

    private Button MakeButton(string _text)
    {
        Button button = new Button();

        button.text = _text;
        button.style.fontSize = 25;
        button.style.alignContent = Align.Center;
        button.style.alignItems = Align.Stretch;
        button.style.flexDirection = FlexDirection.Column;

        button.clicked += Button_clicked;

        return button;
    }

    private Toggle MakeToggle(bool _finished, string _text = "")
    {
        Toggle toggle = new Toggle();

        toggle.text = _text;
        toggle.style.fontSize = 25;
        toggle.style.alignContent = Align.Center;
        toggle.style.alignItems = Align.Stretch;
        toggle.style.flexDirection = FlexDirection.Column;

        toggle.SetEnabled(false);

        toggle.value = _finished;

        return toggle;
    }

    private void Button_clicked()
    {
        print("need to implement");
    }

    private void BtnBack_clicked()
    {
        PageButtons(-1);
    }

    private void BtnNext_clicked()
    {
        PageButtons(1);
    }
    private void PageButtons(int jumpAmount)
    {
        cPage += jumpAmount;
        bg.Clear();

        foreach (var item in pages[cPage])
        {
            bg.Add(item);
        }

        btnBack.SetEnabled(cPage != 0);
        btnNext.SetEnabled(cPage != pages.Count - 1);
    }
}
