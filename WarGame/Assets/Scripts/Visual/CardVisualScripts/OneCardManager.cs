using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// holds the refs to all the Text, Images on the card
public class OneCardManager : MonoBehaviour {

    public CardAsset cardAsset;
    public OneCardManager PreviewManager;
    [Header("Text Component References")]
    public Text NameText;
    public Text DescriptionText;
    public Text FactionText;
    [Header("Image References")]
    public Image CardTopRibbonImage;
    public Image CardLowRibbonImage;
    public Image CardGraphicImage;
    public Image CardBodyImage;
    public Image CardFaceFrameImage;
    public Image CardFaceGlowImage;
    public Image CardBackGlowImage;

    void Awake()
    {
        if (cardAsset != null)
            ReadCardFromAsset();
    }

    //private bool canBePlayedNow = false;
    //public bool CanBePlayedNow
    //{
    //    get
    //    {
    //        return canBePlayedNow;
    //    }

    //    set
    //    {
    //        canBePlayedNow = value;

    //        CardFaceGlowImage.enabled = value;
    //    }
    //}

    public void ReadCardFromAsset()
    {
        // universal actions for any Card
        // 1) apply tint
        if (cardAsset.FactionAsset != null)
        {
            // if the card belongs to a certain character class
            CardBodyImage.color = cardAsset.FactionAsset.FactionCardTint;
            CardFaceFrameImage.color = cardAsset.FactionAsset.FactionCardTint;
            CardTopRibbonImage.color = cardAsset.FactionAsset.FactionRibbonsTint;
            CardLowRibbonImage.color = cardAsset.FactionAsset.FactionRibbonsTint;

            FactionText.color = cardAsset.FactionAsset.FactionTextTint;
            NameText.color = cardAsset.FactionAsset.FactionTextTint;
            DescriptionText.color = cardAsset.FactionAsset.FactionTextTint;

            FactionText.text = cardAsset.FactionAsset.FactionName;
        }
        else
        {
            //CardBodyImage.color = GlobalSettings.Instance.CardBodyStandardColor;
            CardFaceFrameImage.color = Color.white;
            //CardTopRibbonImage.color = GlobalSettings.Instance.CardRibbonsStandardColor;
            //CardLowRibbonImage.color = GlobalSettings.Instance.CardRibbonsStandardColor;

            FactionText.color = Color.black;
            NameText.color = Color.black;
            DescriptionText.color = Color.black;

            //CardLowRibbonImage.gameObject.SetActive(false);
            FactionText.text = "Special";
        }

        // 2) add card name
        NameText.text = cardAsset.name;
        // 4) add description
        DescriptionText.text = cardAsset.Value.ToString();
        // 5) Change the card graphic sprite
        CardGraphicImage.sprite = cardAsset.CardImage;

        if (PreviewManager != null)
        {
            // this is a card and not a preview
            // Preview GameObject will have OneCardManager as well, but PreviewManager should be null there
            PreviewManager.cardAsset = cardAsset;
            PreviewManager.ReadCardFromAsset();
        }
    }
}
