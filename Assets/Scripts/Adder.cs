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

    private Button btnMod;

    public MShows baseShow;

    private void Start()
    {
        Invoke("StarterFunction", 2);
    }

    private void StarterFunction()
    {
        driver = GetComponent<PublicDriver>();

        btnMod = driver.btnMod;

        btnMod.clicked += BtnMod_clicked;
    }

    private void BtnMod_clicked()
    {
        if(baseShow == null)
        {
            baseShow = new MShows(driver.lastID,"isim deneme", new List<MSeasons>(), false);
        }
    }
}
