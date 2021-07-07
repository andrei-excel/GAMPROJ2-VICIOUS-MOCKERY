using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class FlipPage : MonoBehaviour
{
    public enum ButtonType
    {
        NextButton,
        BackButton
    }

    [SerializeField] Button nextBtn;
    [SerializeField] Button backBtn;
    [SerializeField] Button closeBtn;


    [SerializeField] Text headerText1_1;
    [SerializeField] Text headerText1_2;
    [SerializeField] Text headerText2_1;
    [SerializeField] Text headerText2_2;

    [SerializeField] Text bodyText1_1;
    [SerializeField] Text bodyText1_2;
    [SerializeField] TextMeshProUGUI bodyText1_3;
    [SerializeField] Text bodyText2_1;
    [SerializeField] Text bodyText2_2;
    [SerializeField] Image img;

    [SerializeField] Text footerText1_1;
    [SerializeField] Text footerText1_2;
    [SerializeField] Text footerText2_1;
    [SerializeField] Text footerText2_2;

    private Vector3 rotationVector;
    private Vector3 startPosition;
    private Quaternion startRotation;

    private bool isClicked;

    private DateTime startTime;
    private DateTime endTime;

    // Start is called before the first frame update
    private void Start()
    {
        startRotation = transform.rotation;
        startPosition = transform.position;

        if (nextBtn != null)
            nextBtn.onClick.AddListener(() => turnOnePageBtn_Click(ButtonType.NextButton));

        if (backBtn != null)
            backBtn.onClick.AddListener(() => turnOnePageBtn_Click(ButtonType.BackButton));

        if (closeBtn != null)
            closeBtn.onClick.AddListener(() => closeBookBtn_Click());
    }


    private void Awake()
    {
        AppEvents.OpenBook += new EventHandler(openBookBtn_Click);
        //nextBtn.gameObject.SetActive(true);
    }

    // Update is called once per frame
    private void Update()
    {
        if (isClicked)
        {
            transform.Rotate(rotationVector * Time.deltaTime);

            endTime = DateTime.Now;
            if((endTime - startTime).TotalSeconds >= 1)
            {
                isClicked = false;
                transform.rotation = startRotation;
                transform.position = startPosition;

                SetVisibleText();
            }
        }
    }

    private void openBookBtn_Click(object sender, EventArgs e)
    {
        Page pge = Page.GetRandomPage();
        Page.CurrentPage1 = 0;
        Page.CurrentPage2 = 1;

        backBtn.gameObject.SetActive(false);
        nextBtn.gameObject.SetActive(false);

       if (pge.Pages.Count > 2)
        {
            nextBtn.gameObject.SetActive(true);
        }

        SetVisibleText();
    }

    private void SetVisibleText()
    {
        Page pge = Page.RandomPage;

        string footer1 = "";
        string footer2 = "";
        string body1 = "";
        string body2 = "";
        string header1 = "";
        string summary = "";
        string header2 = "";

        if (Page.CurrentPage1 < pge.Pages.Count)
        {
            footer1 = String.Format("Page {0} of {1}", Page.CurrentPage1 + 1, pge.Pages.Count);
            body1 = pge.Pages[Page.CurrentPage1];
            header1 = pge.Title;
            summary = pge.Summary;
        }

        if (Page.CurrentPage2 < pge.Pages.Count)
        {
            footer2 = String.Format("Page {0} of {1}", Page.CurrentPage2 + 1, pge.Pages.Count);
            body2 = pge.Pages[Page.CurrentPage2];
            header2 = pge.Title;
        }

        headerText1_1.text = header1;
        headerText2_1.text = summary;
        
        footerText1_1.text = footer1;
        footerText2_1.text = footer2;

        bodyText1_1.text = body1;
        bodyText1_2.text = body2;


    }

    private void SetFlipPageText(int leftPage, int rightPage)
    {
        Page pge = Page.RandomPage;

        string footerR = "";
        string footerL = "";
        string bodyR = "";
        string bodyL = "";
        string headerR = "";
        string summary = "";
        string headerL = "";

        if (rightPage < pge.Pages.Count)
        {
            footerR = String.Format("Page {0} of {1}", rightPage + 1, pge.Pages.Count);
            bodyR = pge.Pages[rightPage];
            headerR = pge.Title;
            summary = pge.Summary;
        }

        if (leftPage < pge.Pages.Count)
        {
            footerL = String.Format("Page {0} of {1}", leftPage + 1, pge.Pages.Count);
            bodyL = pge.Pages[leftPage];
            headerL = pge.Title;
        }

        headerText1_2.text = summary;
        headerText2_2.text = headerL;

        footerText1_2.text = footerL;
        footerText2_2.text = footerR;

        bodyText1_2.text = bodyL;
        bodyText2_2.text = bodyR;


    }

    private void ClearVisibleText()
    {
        Page pge = Page.RandomPage;

        string footer1 = "";
        string footer2 = "";
        string body1 = "";
        string body2 = "";
        string header1 = "";
        string summary = "";
        //string header2 = "";

       
        headerText1_1.text = header1;
        headerText2_1.text = summary;

        footerText1_1.text = footer1;
        footerText2_1.text = footer2;

        bodyText1_1.text = body1;
        bodyText1_2.text = body2;


    }

    public void turnOnePageBtn_Click(ButtonType type)
    {
        isClicked = true;
        startTime = DateTime.Now;
        nextBtn.gameObject.SetActive(true);
        backBtn.gameObject.SetActive(true);
        

        if (type == ButtonType.NextButton)
        {
            rotationVector = new Vector3(0, 180, 0);

            SetFlipPageText(Page.CurrentPage2, Page.CurrentPage2 + 1);

            Page.CurrentPage1 += 2;
            Page.CurrentPage2 += 2;
            Page pge = Page.RandomPage;

            //SetVisibleText();

            ClearVisibleText();

            if ((Page.CurrentPage2 >= pge.Pages.Count || (Page.CurrentPage1 >= pge.Pages.Count)))
            {
                nextBtn.gameObject.SetActive(false);
            }

        }
        else if (type == ButtonType.BackButton)
        {
            Vector3 newRotation = new Vector3(startRotation.x, 180, startRotation.z);
            transform.rotation = Quaternion.Euler(newRotation);

            rotationVector = new Vector3(0, -180, 0);

            SetFlipPageText(Page.CurrentPage1 - 1, Page.CurrentPage1);

            Page.CurrentPage1 -= 2;
            Page.CurrentPage2 -= 2;

            //SetVisibleText();

            ClearVisibleText();

            if ((Page.CurrentPage2 <= 0 || (Page.CurrentPage1 <= 0)))
            {
                backBtn.gameObject.SetActive(false);
            }
        }

        FindObjectOfType<AudioManager>().Play("PageFlip_SFX");
    }

    public void closeBookBtn_Click()
    {
        AppEvents.CloseBookFunction();
    }
}
