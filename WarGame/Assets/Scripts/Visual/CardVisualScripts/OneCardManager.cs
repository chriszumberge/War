using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// holds the refs to all the Text, Images on the card
public class OneCardManager : MonoBehaviour {

    public CardAsset cardAsset;
    public OneCardManager PreviewManager;
    [Header("Text Component References")]
    public Text NameText;
    public Text ValueText;
    public Text FactionText;
    public Text SubtitleText;
    public Text DescriptionText;
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
            if (CardBodyImage != null)
                CardBodyImage.color = cardAsset.FactionAsset.FactionCardTint;
            if (CardFaceFrameImage != null)
                CardFaceFrameImage.color = cardAsset.FactionAsset.FactionCardTint;
            if (CardTopRibbonImage != null)
                CardTopRibbonImage.color = cardAsset.FactionAsset.FactionRibbonsTint;
            if (CardLowRibbonImage != null)
                CardLowRibbonImage.color = cardAsset.FactionAsset.FactionRibbonsTint;

            if (FactionText != null)
            {
                FactionText.color = cardAsset.FactionAsset.FactionTextTint;
                FactionText.text = cardAsset.FactionAsset.FactionName;
            }
            
            if (NameText != null)
                NameText.color = cardAsset.FactionAsset.FactionTextTint;
            if (ValueText != null)
                ValueText.color = cardAsset.FactionAsset.FactionTextTint;
            if (SubtitleText != null)
                SubtitleText.color = cardAsset.FactionAsset.FactionTextTint;
            if (DescriptionText != null)
                DescriptionText.color = cardAsset.FactionAsset.FactionTextTint;
        }
        else
        {
            //CardBodyImage.color = GlobalSettings.Instance.CardBodyStandardColor; // #EEE8D0
            if (CardFaceFrameImage != null)
                CardFaceFrameImage.color = Color.white;
            //CardTopRibbonImage.color = GlobalSettings.Instance.CardRibbonsStandardColor; // #A3A3A3
            //CardLowRibbonImage.color = GlobalSettings.Instance.CardRibbonsStandardColor;

            if (FactionText != null)
            {
                FactionText.color = Color.black;
                FactionText.text = "Special";
            }

            if (NameText != null)
                NameText.color = Color.black;
            if (ValueText != null)
                ValueText.color = Color.black;
            if (SubtitleText != null)
                SubtitleText.color = Color.black;
            if (DescriptionText != null)
                DescriptionText.color = Color.black;

            //CardLowRibbonImage.gameObject.SetActive(false);
        }

        // 2) add card name
        if (NameText != null)
            NameText.text = cardAsset.name;
        // 4) add description
        if (ValueText != null)
            ValueText.text = cardAsset.Value.ToString();
        if (SubtitleText != null)
            SubtitleText.text = cardAsset.Subtitle;
        if (DescriptionText != null)
            DescriptionText.text = cardAsset.Description;

        // 5) Change the card graphic sprite
        if (CardGraphicImage != null)
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
