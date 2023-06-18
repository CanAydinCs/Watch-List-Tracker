using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UIElements.Button;
using Label = UnityEngine.UIElements.Label;
using Toggle = UnityEngine.UIElements.Toggle;

public class Adder : MonoBehaviour
{
    private PublicDriver driver;

    private VisualElement listBg, addBg;
    private Button btnMod, btnBack, btnDelete;

    public MShows baseShow;

    private void Start()
    {
        Invoke("StarterFunction", 2);
    }

    private void StarterFunction()
    {
        driver = GetComponent<PublicDriver>();

        listBg = driver.listBg;
        addBg = driver.addBg;

        btnMod = driver.btnMod;
        btnBack = driver.btnBack;
        btnDelete = driver.btnDelete;

        btnMod.clicked += BtnMod_clicked;
        btnBack.clicked += BtnBack_clicked;
        btnDelete.clicked += BtnDelete_clicked;
    }

    public void Switched()
    {
        btnMod.text = baseShow == null ? "Add" : "Edit";
        btnDelete.style.visibility = baseShow == null ? Visibility.Hidden : Visibility.Visible;
    }

    private void BtnMod_clicked()
    {
        if(baseShow == null)
        {
            baseShow = new MShows(driver.lastID,"isim deneme", new List<MSeasons>(), false);
        }
    }

    private void BtnBack_clicked()
    {
        addBg.style.visibility = Visibility.Hidden;
        listBg.style.visibility = Visibility.Visible;
    }

    private void BtnDelete_clicked()
    {
        driver.Delete(baseShow);
    }
}
