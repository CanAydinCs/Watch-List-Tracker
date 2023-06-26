using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.U2D.Path;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

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
    [SerializeField] private Lister lister;
    public Button btnPre, btnAdd, btnNext;
    public List<VisualElement> mySets;

    [Space]
    [Header("Adder")]
    [SerializeField] private Adder adder;
    public Button btnMod, btnBack, btnDelete;

    //// JSON verisini PlayerData sýnýfýna dönüþtürme
    //PlayerData playerData = JsonUtility.FromJson<PlayerData>(json);

    //// PlayerData nesnesini JSON formatýna dönüþtürme
    //string newJson = JsonUtility.ToJson(playerData);

    private void Awake()
    {
        //General
        root = GetComponent<UIDocument>().rootVisualElement;
        listBg = root.Q<VisualElement>("listBg");
        addBg = root.Q<VisualElement>("addBg");

        //Lister
        lister = GetComponent<Lister>();
        btnPre = root.Q<Button>("btnPre");
        btnAdd = root.Q<Button>("btnAdd");
        btnNext = root.Q<Button>("btnNext");

        //Adder
        adder = GetComponent<Adder>();
        btnMod = root.Q<Button>("btnMod");
        btnBack = root.Q<Button>("btnBack");
        btnDelete = root.Q<Button>("btnDelete");

        btnMod.clicked += BtnMod_clicked;
        btnBack.clicked += BtnBack_clicked;
        btnDelete.clicked += BtnDelete_clicked;

        //others
        string showJson = PlayerPrefs.GetString(STORAGE_NAME, "");

        var myObject = JsonUtility.FromJson<List<MShows>>(showJson);
        myShows = myObject != null ? myObject : new List<MShows>();

        if(myShows.Count == 0)
        {
            lastID = 1;

            mySets = new List<VisualElement>();

            return;
        }

        lastID = myShows[myShows.Count - 1].id;

        foreach (MShows show in myShows)
        {
            mySets.Add(MakeSet(show));
        }
    }

    private void BtnMod_clicked()
    {
        if (adder.baseShow == null)
        {
            adder.baseShow = new MShows(lastID, "isim deneme" + lastID, new List<MSeasons>(), false);
        }

        Add(adder.baseShow);
        adder.baseShow = null;
    }

    private void BtnBack_clicked()
    {
        STLister();
    }

    private void BtnDelete_clicked()
    {
        Delete(adder.baseShow);
    }

    public void Add(MShows show)
    {
        //eðer nesne yeni ise id default olarak 0 döner
        if (show.id == 0)
        {
            show.id = lastID;
            lastID++;
            myShows.Add(show);
        }
        else
        {
            MShows main = myShows.FirstOrDefault(x => x.id == show.id);
            main = show;
        }

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

    public void STAddSeason(int id)
    {
        MShows baseShow = myShows.FirstOrDefault(x => x.id == id);

        btnMod.text = baseShow == null ? "Add" : "Edit";
        btnDelete.style.visibility = baseShow == null ? Visibility.Hidden : Visibility.Visible;

        adder.baseShow = baseShow;

        listBg.style.visibility = Visibility.Hidden;
        addBg.style.visibility = Visibility.Visible;
    }

    public void STLister()
    {
        addBg.style.visibility = Visibility.Hidden;
        listBg.style.visibility = Visibility.Visible;
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

    public VisualElement CreateTitle() => MakeSet(new MShows(0, "Name", new List<MSeasons> { new MSeasons("Season", new List<string> { "Last Episode" }, false) }, true));
}
