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
        driver = GetComponent<PublicDriver>();

        listBg = driver.listBg;
        addBg = driver.addBg;

        btnMod = driver.btnMod;
        btnBack = driver.btnBack;
        btnDelete = driver.btnDelete;

        btnMod.text = baseShow == null ? "Add" : "Edit";
        btnDelete.style.visibility = baseShow == null ? Visibility.Hidden : Visibility.Visible;

        btnMod.clicked += BtnMod_clicked;
        btnBack.clicked += BtnBack_clicked;
        btnDelete.clicked += BtnDelete_clicked;
    }

    private void BtnMod_clicked()
    {
        throw new System.NotImplementedException();
    }

    private void BtnBack_clicked()
    {
        addBg.style.visibility = Visibility.Hidden;
        listBg.style.visibility = Visibility.Visible;
    }

    private void BtnDelete_clicked()
    {
        throw new System.NotImplementedException();
    }
}
