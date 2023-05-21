using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PublicDriver : MonoBehaviour
{
    [Header("General")]
    public VisualElement root, listBg, addBg;
    public List<MShows> myShows;
    //storage name for all shows
    public string STORAGE_NAME = "storage";
    public int lastID;

    [Space]
    [Header("Lister")]
    public Button btnPre, btnAdd, btnNext;
    public List<VisualElement> mySets;

    [Space]
    [Header("Adder")]
    public Button btnMod, btnBack, btnDelete;

    private void Awake()
    {
        //General
        root = GetComponent<UIDocument>().rootVisualElement;
        listBg = root.Q<VisualElement>("listBg");
        addBg = root.Q<VisualElement>("addBg");

        //Lister
        btnBack = root.Q<Button>("btnPre");
        btnAdd = root.Q<Button>("btnAdd");
        btnNext = root.Q<Button>("btnNext");

        //Adder
        btnMod = root.Q<Button>("btnMod");
        btnBack = root.Q<Button>("btnBack");
        btnDelete = root.Q<Button>("btnDelete");
    }

    //// JSON verisini PlayerData sýnýfýna dönüþtürme
    //PlayerData playerData = JsonUtility.FromJson<PlayerData>(json);

    //// PlayerData nesnesini JSON formatýna dönüþtürme
    //string newJson = JsonUtility.ToJson(playerData);

    private void Start()
    {
        string showJson = PlayerPrefs.GetString(STORAGE_NAME, "");
        myShows = JsonUtility.FromJson<List<MShows>>(showJson);

        lastID = myShows[myShows.Count - 1].id;

        foreach (MShows show in myShows)
        {
            mySets.Add(MakeSet(show));
        }
    }

    public void Add(MShows show)
    {
        myShows.Add(show);
        Save();
    }

    public void Delete(MShows show)
    {
        myShows.Remove(show);
        Save();
    }

    public void Save()
    {
        string showJson = JsonUtility.ToJson(myShows);
        PlayerPrefs.SetString(STORAGE_NAME, showJson);
    }

    private VisualElement MakeSet(MShows _show)
    {
        VisualElement set = new VisualElement();

        set.style.flexDirection = FlexDirection.Row;
        set.style.alignContent = Align.Center;
        set.style.alignItems = Align.Stretch;

        int indexLastSeason = _show.seasons.Count - 1;
        int indexLastEp = _show.seasons[indexLastSeason].episodes.Count - 1;

        set.Add(MakeButton(_show.id, _show.name));
        set.Add(MakeLabel(_show.seasons[indexLastSeason].name));
        set.Add(MakeLabel(_show.seasons[indexLastSeason].episodes[indexLastEp]));
        set.Add(MakeToggle(_show.isFinished));

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

    private Button MakeButton(int _id, string _text)
    {
        Button button = new Button();

        button.text = _text;
        button.style.fontSize = 25;
        button.style.alignContent = Align.Center;
        button.style.alignItems = Align.Stretch;
        button.style.flexDirection = FlexDirection.Column;

        Label lblId = new Label();
        Color color = new Color(0f, 0f, 0f, 0f);
        lblId.style.color = color;

        lblId.text = _id.ToString();

        button.Add(lblId);

        button.clicked += Button_clicked;

        return button;
    }

    private Toggle MakeToggle(bool _finished)
    {
        Toggle toggle = new Toggle();

        toggle.text = "Finished";
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

    public VisualElement CreateTitle() => MakeSet(new MShows(31, "Name", new List<MSeasons> { new MSeasons("Season", new List<string> { "Last Episode" }, false) }, true));
}
