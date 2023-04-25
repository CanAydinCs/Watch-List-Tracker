using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UIElements.Button;
using Toggle = UnityEngine.UIElements.Toggle;

public class Lister : MonoBehaviour
{
    private VisualElement root;

    private void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
    }

    private VisualElement MakeSet(string _name, string _lastSeason, string _lastEpisode, string _lastWatchedName, bool _finished)
    {
        VisualElement set = new VisualElement();

        set.Add(MakeLabel(_name));
        set.Add(MakeLabel(_lastSeason));
        set.Add(MakeLabel(_lastEpisode));
        set.Add(MakeLabel(_lastWatchedName));
        set.Add(MakeLabel(_finished));



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

    private Toggle MakeToggle(bool _text)
    {
        Toggle toggle = new Toggle();

        toggle.style.fontSize = 25;
        toggle.style.alignContent = Align.Center;
        toggle.style.alignItems = Align.Stretch;
        toggle.style.flexDirection = FlexDirection.Column;

        

        return toggle;
    }

    private void Button_clicked()
    {
        throw new System.NotImplementedException();
    }
}
