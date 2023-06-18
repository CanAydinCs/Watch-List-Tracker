using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UIElements.Button;
using Label = UnityEngine.UIElements.Label;
using Toggle = UnityEngine.UIElements.Toggle;

public class Lister : MonoBehaviour
{
    private PublicDriver driver;

    private VisualElement listBg, addBg;
    private Button btnPre, btnAdd, btnNext;

    public List<VisualElement[]> pages;

    public VisualElement titlePrefab;

    [SerializeField]
    private int softLimit = 15;

    private int cPage = 0;
    private int lastIndex = 0;

    private void Start()
    {
        Invoke("StarterFunction", 2f);
    }

    private void StarterFunction()
    {
        driver = GetComponent<PublicDriver>();

        listBg = driver.listBg;
        addBg = driver.addBg;

        btnPre = driver.btnPre;
        btnAdd = driver.btnAdd;
        btnNext = driver.btnNext;

        btnPre.clicked += BtnBack_clicked;
        btnAdd.clicked += BtnAdd_clicked;
        btnNext.clicked += BtnNext_clicked;

        foreach (VisualElement set in driver.mySets)
        {
            AddToPages(set);
        }

        titlePrefab = driver.CreateTitle();

        if (pages == null)
        {
            pages = new List<VisualElement[]>();
            return;
        }

        PageButtons(0);
    }

    private void AddToPages(VisualElement element)
    {
        if (lastIndex == softLimit)
        {
            pages.Add(new VisualElement[softLimit]);
            pages[pages.Count - 1][0] = titlePrefab;
            pages[pages.Count - 1][1] = element;

            lastIndex = 2;
        }
        else
        {
            pages[pages.Count - 1][lastIndex] = element;

            lastIndex += 1;
        }
    }

    private void BtnBack_clicked()
    {
        PageButtons(-1);
    }
    private void BtnAdd_clicked()
    {
        Adder adder = GetComponent<Adder>();
        adder.baseShow = null;

        listBg.style.visibility = Visibility.Hidden;
        addBg.style.visibility = Visibility.Visible;
    }

    private void BtnNext_clicked()
    {
        PageButtons(1);
    }
    private void PageButtons(int jumpAmount)
    {
        cPage += jumpAmount;
        listBg.Clear();

        foreach (var item in pages[cPage])
        {
            listBg.Add(item);
        }

        btnPre.SetEnabled(cPage != 0);
        btnNext.SetEnabled(cPage != pages.Count - 1);
    }
}